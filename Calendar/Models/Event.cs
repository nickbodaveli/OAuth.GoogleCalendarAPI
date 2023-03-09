namespace Calendar.Models
{
    public class Event
    {
        public string Summary { get; set; }
        public string Description { get; set; }
        public EventDateTime Start { get; set; }
        public EventDateTime End { get; set; }
    }

    public class EventDateTime
    {
        public string DateTime { get; set; }
    }
}
