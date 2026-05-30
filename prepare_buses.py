import json
from datetime import datetime, timedelta

def load_data(filename, vehicle_type):
    with open(filename, 'r', encoding='utf-8') as f:
        data = json.load(f)
        
    routes_map = {}
    for row in data:
        route_num = row["route_number"].replace("№", "").strip()
        route_name = row["route_name"].strip()
        sched_type = row["schedule_type"].strip()
        
        key = (route_num, route_name, sched_type, vehicle_type)
        if key not in routes_map:
            routes_map[key] = []
            
        routes_map[key].append({
            "name": row["stop_name"].strip(),
            "times": row["times"]
        })
    return routes_map

map_buses = load_data('buses.json', 'Bus')
map_trolleys = load_data('trolleybuses.json', 'Trolleybus')

all_maps = {**map_buses, **map_trolleys}

final_routes = []

for (num, name, sched, vtype), stops in all_maps.items():
    if not stops: continue
    
    day = sched
    if sched == "Р": day = "Рабочие дни"
    elif sched == "В": day = "Выходные дни"
    elif sched == "Е": day = "Все дни"
    
    base_times_str = stops[0]["times"]
    base_times = []
    for t_str in base_times_str:
        try:
            base_times.append(datetime.strptime(t_str, "%H:%M:%S"))
        except:
            pass
            
    final_stops = []
    prev_offset = 0
    for i, stop in enumerate(stops):
        offset = 0
        if i > 0 and len(base_times) > 0 and len(stop["times"]) > 0:
            try:
                t1 = base_times[len(base_times) // 2]
                stop_times = []
                for t_str in stop["times"]:
                    try: stop_times.append(datetime.strptime(t_str, "%H:%M:%S"))
                    except: pass
                
                if stop_times:
                    best_diff = None
                    for t2 in stop_times:
                        diff = (t2 - t1).total_seconds()
                        if diff < -12*3600: diff += 24*3600
                        if diff > 12*3600: diff -= 24*3600
                        
                        if diff >= prev_offset * 60:
                            if best_diff is None or diff < best_diff:
                                best_diff = diff
                    if best_diff is not None:
                        offset = int(best_diff / 60)
                    else:
                        offset = prev_offset # Fallback if we somehow can't find a next time
            except: pass
        else:
            offset = 0
            
        prev_offset = offset
        
        final_stops.append({
            "Name": stop["name"],
            "OffsetMinutes": offset,
            "Order": i + 1
        })
        
    schedules = sorted(list(set([t.strftime("%H:%M:%S") for t in base_times])))
    
    final_routes.append({
        "Number": num,
        "Name": name,
        "Type": vtype,
        "Day": day,
        "Stops": final_stops,
        "Schedules": schedules
    })

with open('clean_db.json', 'w', encoding='utf-8') as f:
    json.dump(final_routes, f, ensure_ascii=False, indent=2)

print(f"Prepared {len(final_routes)} unified routes.")
