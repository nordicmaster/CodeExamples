from openpyxl import Workbook
from openpyxl import load_workbook

wb = load_workbook('D://work analysis.xlsx')
sheet = wb['Лист1']
keywords = {}
for x in range(2, 100):
    string = sheet['C'+str(x)].value
    if string is None:
        continue
    words = string.split(',')
    for word in words:
        n_word = word.lower().strip()
        if n_word in keywords:
            keywords[n_word] = keywords[n_word] + 1
        else:
            keywords[n_word] = 1
f0 = open("work_keywords.txt", "w", encoding='utf-8')
for key, value in sorted(keywords.items(), key=lambda kv: kv[1], reverse = True):
    f0.write(f"{key}  - {value}")
    f0.write('\n')
f0.close()