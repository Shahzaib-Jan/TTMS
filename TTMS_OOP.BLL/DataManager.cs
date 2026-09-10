using System.Collections.Generic;
using TTMS_OOP.Models;
using TTMS_OOP.DAL;

namespace TTMS_OOP.BLL
{
    public static class DataManager
    {
        public static List<Teacher> GetTeachers()
        {
            var list = JsonRepository.LoadTeachers();
            list.Sort((a, b) => string.Compare(a.Name ?? "", b.Name ?? "", System.StringComparison.OrdinalIgnoreCase));
            return list;
        }
        public static void SaveTeachers(List<Teacher> data) => JsonRepository.SaveTeachers(data);

        public static List<Subject> GetSubjects()
        {
            var list = JsonRepository.LoadSubjects();
            list.Sort((a, b) => {
                int semA = TimetableManager.GetSemesterNumber(a.Semester);
                int semB = TimetableManager.GetSemesterNumber(b.Semester);
                int cmp = semA.CompareTo(semB);
                if (cmp != 0) return cmp;
                string codeA = a.CourseCode ?? "";
                string codeB = b.CourseCode ?? "";
                cmp = string.Compare(codeA, codeB, System.StringComparison.OrdinalIgnoreCase);
                if (cmp != 0) return cmp;
                cmp = string.Compare(a.Name ?? "", b.Name ?? "", System.StringComparison.OrdinalIgnoreCase);
                if (cmp != 0) return cmp;
                return a.IsLab.CompareTo(b.IsLab);
            });
            return list;
        }
        public static void SaveSubjects(List<Subject> data) => JsonRepository.SaveSubjects(data);

        public static List<Section> GetSections()
        {
            var list = JsonRepository.LoadSections();
            list.Sort((a, b) => {
                int semA = TimetableManager.GetSemesterNumber(a.Semester);
                int semB = TimetableManager.GetSemesterNumber(b.Semester);
                int cmp = semA.CompareTo(semB);
                if (cmp != 0) return cmp;
                return string.Compare(a.Name ?? "", b.Name ?? "", System.StringComparison.OrdinalIgnoreCase);
            });
            return list;
        }
        public static void SaveSections(List<Section> data) => JsonRepository.SaveSections(data);

        public static List<TimeSlot> GetSlots()
        {
            var list = JsonRepository.LoadSlots();
            list.Sort((a, b) => ParseTime(a.StartTime).CompareTo(ParseTime(b.StartTime)));
            return list;
        }

        private static int ParseTime(string timeStr)
        {
            if (string.IsNullOrWhiteSpace(timeStr)) return 0;
            string[] parts = timeStr.Split(new char[] { ':', ' ', '-' }, System.StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 0) return 0;
            if (int.TryParse(parts[0], out int h))
            {
                int m = 0;
                if (parts.Length > 1 && int.TryParse(parts[1], out int mParsed)) m = mParsed;
                if (h >= 1 && h <= 7) h += 12; // Convert 1 PM - 7 PM to 24-hour format
                return h * 100 + m;
            }
            return 0;
        }
        public static void SaveSlots(List<TimeSlot> data) => JsonRepository.SaveSlots(data);

        public static List<TimetableEntry> GetEntries() => JsonRepository.LoadEntries();
        public static void SaveEntries(List<TimetableEntry> data) => JsonRepository.SaveEntries(data);
    }
}
