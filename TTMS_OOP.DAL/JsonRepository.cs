using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using TTMS_OOP.Models;

namespace TTMS_OOP.DAL
{
    public static class JsonRepository
    {
        private static string teachersFile = "teachers.json";
        private static string subjectsFile = "subjects.json";
        private static string sectionsFile = "sections.json";
        private static string slotsFile = "timeslots.json";
        private static string entriesFile = "entries.json";
        private static string credsFile = "credentials.json";

        private static void Save<T>(List<T> data, string filename)
        {
            File.WriteAllText(filename, JsonConvert.SerializeObject(data, Formatting.Indented));
        }

        private static List<T> Load<T>(string filename)
        {
            if (!File.Exists(filename))
                return new List<T>();
            return JsonConvert.DeserializeObject<List<T>>(File.ReadAllText(filename));
        }

        public static List<Teacher> LoadTeachers() => Load<Teacher>(teachersFile);
        public static void SaveTeachers(List<Teacher> data) => Save(data, teachersFile);

        public static List<Subject> LoadSubjects() => Load<Subject>(subjectsFile);
        public static void SaveSubjects(List<Subject> data) => Save(data, subjectsFile);

        public static List<Section> LoadSections() => Load<Section>(sectionsFile);
        public static void SaveSections(List<Section> data) => Save(data, sectionsFile);

        public static List<TimeSlot> LoadSlots() => Load<TimeSlot>(slotsFile);
        public static void SaveSlots(List<TimeSlot> data) => Save(data, slotsFile);

        public static List<TimetableEntry> LoadEntries() => Load<TimetableEntry>(entriesFile);
        public static void SaveEntries(List<TimetableEntry> data) => Save(data, entriesFile);

        public static List<UserCredential> LoadCredentials() => Load<UserCredential>(credsFile);
        public static void SaveCredentials(List<UserCredential> data) => Save(data, credsFile);
    }
}
