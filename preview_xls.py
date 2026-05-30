import sys
import pandas as pd
import json

sys.stdout.reconfigure(encoding='utf-8')

try:
    df = pd.read_excel('p.xls')
except:
    df = pd.read_excel('p.xls', engine='xlrd')

print("Columns:", df.columns.tolist())
df.to_json('p.json', orient='records', force_ascii=False)
print("Saved to p.json")
