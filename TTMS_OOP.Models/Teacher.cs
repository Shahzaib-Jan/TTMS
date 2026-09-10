namespace TTMS_OOP.Models
{
    public class Teacher : IEntity
    {
        public int TeacherId { get; set; }
        public string Name { get; set; }
        public string Designation { get; set; }

        public string FullName { get { return Designation + " " + Name; } }

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