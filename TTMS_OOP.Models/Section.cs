namespace TTMS_OOP.Models
{
    public class Section : IEntity
    {
        public int SectionId { get; set; }
        public string Name { get; set; }
        public string Semester { get; set; }
        public string Session { get; set; }

        public string GetEntityName()
        {
            return "Section " + Name;
        }

        public override string ToString()
        {
            if (string.IsNullOrWhiteSpace(Name)) return Semester;
            return "Section " + Name + " - " + Semester;
        }
    }
}