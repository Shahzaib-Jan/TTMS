using System.Collections.Generic;
using TTMS_OOP.Models;
using TTMS_OOP.DAL;

namespace TTMS_OOP.BLL
{
    public static class TimetableManager
    {
        public static int GetSemesterNumber(string sem)
        {
            if (string.IsNullOrWhiteSpace(sem)) return 0;
            string s = sem.Trim().ToLower();
            for (int i = 1; i <= 8; i++)
            {
                if (s.Contains(i.ToString())) return i;
            }
            if (s.Contains("first") || s.Contains("1st")) return 1;
            if (s.Contains("second") || s.Contains("2nd")) return 2;
            if (s.Contains("third") || s.Contains("3rd")) return 3;
            if (s.Contains("fourth") || s.Contains("4th")) return 4;
            if (s.Contains("fifth") || s.Contains("5th")) return 5;
            if (s.Contains("sixth") || s.Contains("6th")) return 6;
            if (s.Contains("seventh") || s.Contains("7th")) return 7;
            if (s.Contains("eighth") || s.Contains("8th")) return 8;
            return 0;
        }

        public static bool IsSameSemester(string sem1, string sem2)
        {
            if (string.IsNullOrWhiteSpace(sem1) && string.IsNullOrWhiteSpace(sem2)) return true;
            if (string.Equals(sem1?.Trim(), sem2?.Trim(), System.StringComparison.OrdinalIgnoreCase)) return true;
            int n1 = GetSemesterNumber(sem1);
            int n2 = GetSemesterNumber(sem2);
            if (n1 > 0 && n2 > 0) return n1 == n2;
            return false;
        }

        public static bool IsTeacherBusy(int teacherId, int slotId, string day, int excludeSectionId)
        {
            if (teacherId <= 0) return false;
            List<TimetableEntry> entries = JsonRepository.LoadEntries();
            List<Subject> subjects = JsonRepository.LoadSubjects();

            foreach (TimetableEntry entry in entries)
            {
                if (entry.IsReserved) continue;
                if (entry.SlotId != slotId) continue;
                if (entry.Day != day) continue;
                if (entry.SectionId == excludeSectionId) continue;

                int assignedTid = entry.TeacherId;
                if (assignedTid <= 0)
                {
                    Subject subj = subjects.Find(s => s.SubjectId == entry.SubjectId);
                    if (subj != null) assignedTid = subj.TeacherId;
                }

                if (assignedTid == teacherId)
                    return true;
            }
            return false;
        }

        public static int GetTotalConflicts()
        {
            List<TimetableEntry> entries = JsonRepository.LoadEntries();
            List<Subject> subjects = JsonRepository.LoadSubjects();
            int conflicts = 0;

            for (int i = 0; i < entries.Count; i++)
            {
                if (entries[i].IsReserved) continue;
                int t1 = entries[i].TeacherId;
                if (t1 <= 0)
                {
                    Subject s1 = subjects.Find(s => s.SubjectId == entries[i].SubjectId);
                    if (s1 != null) t1 = s1.TeacherId;
                }
                if (t1 <= 0) continue;

                for (int j = i + 1; j < entries.Count; j++)
                {
                    if (entries[j].IsReserved) continue;
                    if (entries[i].SlotId != entries[j].SlotId) continue;
                    if (entries[i].Day != entries[j].Day) continue;
                    if (entries[i].SectionId == entries[j].SectionId) continue;

                    int t2 = entries[j].TeacherId;
                    if (t2 <= 0)
                    {
                        Subject s2 = subjects.Find(s => s.SubjectId == entries[j].SubjectId);
                        if (s2 != null) t2 = s2.TeacherId;
                    }

                    if (t2 > 0 && t1 == t2)
                        conflicts++;
                }
            }
            return conflicts;
        }
    }
}
