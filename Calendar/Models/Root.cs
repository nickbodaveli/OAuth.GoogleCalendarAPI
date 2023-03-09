namespace Calendar.Models
{
    public class Root
    {
        public string kind { get; set; }
        public string etag { get; set; }
        public string summary { get; set; }
        public DateTime updated { get; set; }
        public string timeZone { get; set; }
        public string accessRole { get; set; }
        public string nextSyncToken { get; set; }
        public List<Item> items { get; set; }
    }

    public class Item
    {
        public string kind { get; set; }
        public string etag { get; set; }
        public string id { get; set; }
        public string status { get; set; }
        public string htmlLink { get; set; }
        public DateTime created { get; set; }
        public DateTime updated { get; set; }
        public string summary { get; set; }
        public string description { get; set; }
        public Start start { get; set; }
        public End end { get; set; }
        public string transparency { get; set; }
        public string visibility { get; set; }
        public string iCalUID { get; set; }
        public int sequence { get; set; }
        public bool guestsCanInviteOthers { get; set; }
        public bool privateCopy { get; set; }
        public string eventType { get; set; }
        public string location { get; set; }
        public string hangoutLink { get; set; }
        public List<string> recurrence { get; set; }
        public string recurringEventId { get; set; }
    }

    public class Start
    {
        public DateTime dateTime { get; set; }
        public string timeZone { get; set; }
    }

    public class End
    {
        public DateTime dateTime { get; set; }
        public string timeZone { get; set; }
    }
}
