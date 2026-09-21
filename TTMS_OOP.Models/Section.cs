namespace TTMS_OOP.Models
{
    public class Section : IEntity
    {
        public int SectionId { get; set; }
        public string Name { get; set; }
        public string Semester { get; set; }
        public string Session { get; set; }
        public string Department { get; set; }

        public string GetEntityName()
        {
            string dept = !string.IsNullOrWhiteSpace(Department) ? Department + " " : "";
            return dept + "Section " + Name;
        }

        public override string ToString()
        {
            string dept = !string.IsNullOrWhiteSpace(Department) ? Department + " - " : "";
            string name = !string.IsNullOrWhiteSpace(Name) ? "Section " + Name : "";
            string sem = !string.IsNullOrWhiteSpace(Semester) ? " (" + Semester + ")" : "";
            return dept + name + sem;
        }
    }
}