namespace TTMS_OOP.Models
{
    public class TimeSlot
    {
        public int SlotId { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }

        public override string ToString()
        {
            return StartTime + " - " + EndTime;
        }
    }
}