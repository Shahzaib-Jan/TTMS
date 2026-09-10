namespace TTMS_OOP.Models
{
    public class TimetableEntry
    {
        public int EntryId { get; set; }
        public int SectionId { get; set; }
        public int SubjectId { get; set; }
        public int TeacherId { get; set; }
        public int SlotId { get; set; }
        public string Day { get; set; }
        public bool IsReserved { get; set; }
        public bool IsLocked { get; set; }
        public string CustomText { get; set; }
    }
}