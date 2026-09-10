namespace TTMS_OOP.Models
{
    public class Subject : IEntity
    {
        public int SubjectId { get; set; }
        public string CourseCode { get; set; }
        public string Name { get; set; }
        public string Semester { get; set; }
        public bool IsLab { get; set; }
        public int TeacherId { get; set; }

        public string GetEntityName()
        {
            string code = !string.IsNullOrWhiteSpace(CourseCode) ? "[" + CourseCode.Trim() + "] " : "";
            return code + Name;
        }

        public override string ToString()
        {
            string code = !string.IsNullOrWhiteSpace(CourseCode) ? CourseCode.Trim() + " - " : "";
            return code + Name + (IsLab ? " (Lab)" : "");
        }
    }
}