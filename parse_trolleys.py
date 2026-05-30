import os
import json
from bs4 import BeautifulSoup

files = [
    ("100 Заречный .txt", "№100", "Заречный - Облбольница"),
    ("100 облбольницы.txt", "№100", "Облбольница - Заречный"),
    ("102 ДП Виктория.txt", "№102", "ДП Виктория - Свердлова"),
    ("102 Свердлова.txt", "№102", "Свердлова - ДП Виктория")
]

all_data = []
stop_id_counter = 500 # Just an arbitrary starting id

for filename, route_number, route_name in files:
    if not os.path.exists(filename):
        print(f"File not found: {filename}")
        continue
        
    with open(filename, 'r', encoding='utf-8') as f:
        html = f.read()
        
    soup = BeautifulSoup(html, 'html.parser')
    
    # Usually the stops and times are in two separate tables inside divs with overflow-x: scroll
    divs = soup.find_all('div', style=lambda value: value and 'overflow-x: scroll' in value)
    
    if len(divs) >= 2:
        stops_table = divs[0].find('table')
        times_table = divs[1].find('table')
        
        stops_rows = stops_table.find_all('tr')
        times_rows = times_table.find_all('tr')
        
        for i, stop_row in enumerate(stops_rows):
            if i >= len(times_rows): break
            
            stop_cell = stop_row.find(['th', 'td'])
            if not stop_cell: continue
            stop_name = stop_cell.get_text(strip=True)
            
            time_cells = times_rows[i].find_all(['td', 'th'])
            times = []
            for tc in time_cells:
                t = tc.get_text(strip=True).replace('.', ':')
                # Filter out valid times, like HH:MM
                if len(t) == 5 and ':' in t:
                    try:
                        h, m = t.split(':')
                        if h.isdigit() and m.isdigit():
                            times.append(f"{h.zfill(2)}:{m.zfill(2)}:00")
                    except:
                        pass
                        
            all_data.append({
                "route_number": route_number,
                "schedule_type": "Е",
                "route_name": route_name,
                "stop_name": stop_name,
                "stop_id": str(stop_id_counter),
                "times": times
            })
            stop_id_counter += 1
    else:
        print(f"Could not find required tables in {filename}")

with open('trolleybuses.json', 'w', encoding='utf-8') as f:
    json.dump(all_data, f, ensure_ascii=False, indent=2)

print(f"Parsed {len(all_data)} stops for trolleybuses.")
