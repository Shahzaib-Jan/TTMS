namespace TTMS_OOP.Models
{
    public class Teacher : IEntity
    {
        public int TeacherId { get; set; }
        public string Name { get; set; }
        public string Designation { get; set; }

        public string FullName
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Designation)) return (Name ?? "").Trim();
                if (string.IsNullOrWhiteSpace(Name)) return (Designation ?? "").Trim();
                return Designation.Trim() + " " + Name.Trim();
            }
        }

        public string GetEntityName()
        {
            return FullName;
        }

        public override string ToString()
        {
            return FullName;
        }
    }
}