import json
from datetime import datetime

with open('p.json', 'r', encoding='utf-8') as f:
    data = json.load(f)

# Group by route number to unify buses
# Key: RouteNumber, Value: dict of branches
routes_by_num = {} 

for row in data:
    route_num = row.get("Маршрут")
    if not route_num:
        continue
        
    route_num = route_num.replace("№", "").strip()
    direction = row.get("Направление", "").strip()
    stop_name = row.get("Остановка", "").strip()
    
    if route_num not in routes_by_num:
        routes_by_num[route_num] = []
        
    # We will build branches temporarily. A branch is defined by (Direction, Day).
    # Since rows are ordered in the file, we can just append stops to the last branch 
    # if it matches direction. But it's safer to just group by (Direction, Day)
    day = row.get("День", "").strip()
    branch_key = f"{direction}_{day}"
    
    # find branch
    branch = next((b for b in routes_by_num[route_num] if b['key'] == branch_key), None)
    if not branch:
        branch = {
            'key': branch_key,
            'direction': direction,
            'stops': []
        }
        routes_by_num[route_num].append(branch)
        
    # Extract times as a dict { column_key: time_obj }
    times_dict = {}
    for k, v in row.items():
        if k.startswith("Unnamed:") and v and type(v) == str and ":" in v:
            try:
                t = datetime.strptime(v.strip(), "%H:%M:%S")
                # ignore small offsets that might be in Unnamed: 7 
                # actually, any valid time can be here, but we will just store it
                times_dict[k] = t
            except:
                pass
                
    branch['stops'].append({
        "Name": stop_name,
        "Times": times_dict
    })

final_routes = []
for num, branches in routes_by_num.items():
    # To unify, we pick the branch with the MOST stops (most complete).
    # Or we could just pick the very first one, which is usually the main direction.
    best_branch = max(branches, key=lambda b: len(b['stops']))
    
    first_stop = best_branch['stops'][0]
    
    # Extract schedules (just the values from first stop, excluding things that look like offsets like 00:02:00)
    # Actually, any time in first stop is a real schedule time.
    schedules = [t.strftime("%H:%M:%S") for k, t in first_stop["Times"].items()]
    
    final_stops = []
    for i, s in enumerate(best_branch['stops']):
        offset = 0
        if i > 0:
            # find first shared key
            shared_keys = set(first_stop["Times"].keys()).intersection(set(s["Times"].keys()))
            
            # exclude Unnamed: 7 if it's there because it's often an offset, not a time
            # actually if it's in BOTH, it means both have a time in that column.
            # but wait, first stop has Unnamed: 7 = null. So it won't be in shared_keys!
            
            if shared_keys:
                k = sorted(list(shared_keys))[0]
                t1 = first_stop["Times"][k]
                t2 = s["Times"][k]
                offset_sec = (t2 - t1).total_seconds()
                # handle crossing midnight if needed, but normally t2 >= t1
                if offset_sec < -12*3600:
                    offset_sec += 24*3600
                offset = int(offset_sec / 60)
                
        final_stops.append({
            "Name": s["Name"],
            "OffsetMinutes": offset,
            "Order": i + 1
        })
        
    final_routes.append({
        "Number": num,
        "Name": best_branch['direction'],
        "Type": "Bus",
        "Day": "Все дни",
        "Stops": final_stops,
        "Schedules": sorted(schedules)
    })

with open('clean_db.json', 'w', encoding='utf-8') as f:
    json.dump(final_routes, f, ensure_ascii=False, indent=2)

print(f"Prepared {len(final_routes)} unified routes.")
