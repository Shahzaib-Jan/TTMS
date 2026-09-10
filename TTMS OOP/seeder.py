import json
import os

entries_path = r"d:\C#\TTMS BY ZAIB\TTMS OOP\bin\Debug\entries.json"

with open(entries_path, 'r') as f:
    entries = json.load(f)

# Keep only existing entries for Sections 1,2,3
entries = [e for e in entries if e["SectionId"] in [1, 2, 3]]

def add_entry(sec, subj, slot, day, is_res=False):
    entries.append({
        "EntryId": 0,
        "SectionId": sec,
        "SubjectId": subj,
        "SlotId": slot,
        "Day": day,
        "IsReserved": is_res
    })

# Section 4 (8th Semester)
# Final Project - II (34), International Lang (35), QT-4 (36), Entrepreneurship (37), Islamic (M Aslam 38, Riaz 39), Leadership (40)
add_entry(4, 34, 1, "Monday")
add_entry(4, 37, 2, "Monday")
add_entry(4, 37, 3, "Monday")
add_entry(4, 38, 5, "Monday")
add_entry(4, 34, 6, "Monday")
add_entry(4, 34, 7, "Monday")

add_entry(4, 35, 1, "Tuesday")
add_entry(4, 36, 2, "Tuesday")
add_entry(4, 38, 3, "Tuesday")
add_entry(4, 40, 5, "Tuesday")
add_entry(4, 34, 7, "Tuesday")

add_entry(4, 34, 2, "Wednesday")
add_entry(4, 34, 3, "Wednesday")
add_entry(4, 34, 6, "Wednesday")

add_entry(4, 34, 2, "Thursday")
add_entry(4, 34, 3, "Thursday")
add_entry(4, 39, 5, "Thursday")
add_entry(4, 34, 6, "Thursday")
add_entry(4, 34, 7, "Thursday")

add_entry(4, 34, 1, "Friday")
add_entry(4, 37, 2, "Friday")
add_entry(4, 34, 3, "Friday")
add_entry(4, 40, 4, "Friday")

# Section 5 (4th Semester)
# Assembly (12), Assembly Lab (13), Info Sec (14), Info Sec Lab (15), Adv DBMS (16), Adv DBMS Lab (17)
# Quran (18), Automata (19), AI (20), AI Lab (21), DS (22)
add_entry(5, 13, 2, "Monday")
add_entry(5, 13, 3, "Monday")
add_entry(5, 22, 4, "Monday")
add_entry(5, 22, 5, "Monday")
add_entry(5, 15, 6, "Monday")
add_entry(5, 15, 7, "Monday")

add_entry(5, 12, 1, "Tuesday")
add_entry(5, 20, 3, "Tuesday")
add_entry(5, 12, 6, "Tuesday")

add_entry(5, 14, 1, "Wednesday")
add_entry(5, 18, 2, "Wednesday")
add_entry(5, 16, 3, "Wednesday")
add_entry(5, 19, 4, "Wednesday")
add_entry(5, 17, 6, "Wednesday")
add_entry(5, 17, 7, "Wednesday")

add_entry(5, 21, 2, "Thursday")
add_entry(5, 21, 3, "Thursday")
add_entry(5, 20, 4, "Thursday")
add_entry(5, 12, 6, "Thursday")

add_entry(5, 16, 1, "Friday")
add_entry(5, 19, 2, "Friday")
add_entry(5, 14, 3, "Friday")
add_entry(5, 22, 4, "Friday")
add_entry(5, 19, 5, "Friday")

# Section 6 (6th Semester)
# Graph (23), Psych (25), Compiler (26), Compiler Lab (27), Marketing (28), PDC (29), PDC Lab (30)
# Quran (31), SQE (32), Expository (33)
add_entry(6, 30, 2, "Monday")
add_entry(6, 30, 3, "Monday")
add_entry(6, 28, 4, "Monday")
add_entry(6, 25, 5, "Monday")
add_entry(6, 32, 6, "Monday")
add_entry(6, 23, 7, "Monday")

add_entry(6, 28, 1, "Tuesday")
add_entry(6, 32, 4, "Tuesday")
add_entry(6, 25, 7, "Tuesday")

add_entry(6, 23, 1, "Wednesday")
add_entry(6, 31, 3, "Wednesday")
add_entry(6, 33, 4, "Wednesday")
add_entry(6, 32, 5, "Wednesday")
add_entry(6, 29, 6, "Wednesday")

add_entry(6, 25, 1, "Thursday")
add_entry(6, 27, 3, "Thursday")
add_entry(6, 33, 4, "Thursday")
add_entry(6, 26, 7, "Thursday")

add_entry(6, 26, 1, "Friday")
add_entry(6, 29, 2, "Friday")
add_entry(6, 23, 3, "Friday")

# Section 7 (Cyber 1st Semester)
# Physics (41), Physics Lab (43), Discrete (44), PDS (45), PDS Lab (46), Calculus (47), Basic Math (49), ICT (50/51)
add_entry(7, 41, 1, "Monday")
add_entry(7, 47, 2, "Monday")
add_entry(7, 50, 4, "Monday")
add_entry(7, 45, 6, "Monday")

add_entry(7, 41, 1, "Tuesday")
add_entry(7, 43, 3, "Tuesday")
add_entry(7, 43, 4, "Tuesday")
add_entry(7, 47, 5, "Tuesday")

add_entry(7, 44, 1, "Wednesday")
add_entry(7, 46, 3, "Wednesday")
add_entry(7, 46, 4, "Wednesday")
add_entry(7, 47, 5, "Wednesday")

add_entry(7, 45, 1, "Thursday")
add_entry(7, 49, 2, "Thursday")
add_entry(7, 44, 4, "Thursday")
add_entry(7, 51, 6, "Thursday")

add_entry(7, 49, 2, "Friday")
add_entry(7, 50, 3, "Friday")
add_entry(7, 44, 4, "Friday")

# Section 8 (DS 1st Semester)
# Discrete (44), PF (52), PF Lab (53), Calculus (48), Basic Math (49), ICT (50), Physics (42), Physics Lab (43)
add_entry(8, 44, 1, "Monday")
add_entry(8, 48, 4, "Monday")
add_entry(8, 53, 6, "Monday")
add_entry(8, 53, 7, "Monday")

add_entry(8, 48, 1, "Tuesday")
add_entry(8, 44, 4, "Tuesday")
add_entry(8, 50, 6, "Tuesday")

add_entry(8, 43, 2, "Wednesday")
add_entry(8, 43, 3, "Wednesday")
add_entry(8, 42, 4, "Wednesday")

add_entry(8, 50, 1, "Thursday")
add_entry(8, 42, 4, "Thursday")
add_entry(8, 52, 5, "Thursday")

add_entry(8, 44, 1, "Friday")
add_entry(8, 49, 2, "Friday")
add_entry(8, 52, 3, "Friday")

with open(entries_path, 'w') as f:
    json.dump(entries, f, indent=2)

print("Entries successfully written!")
