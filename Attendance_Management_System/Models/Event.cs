using System;

namespace Attendance_Management_System.Models
{
    public class Event
    {
        public int EventId { get; set; }
        public string EventName { get; set; }
        public DateTime EventDate { get; set; }
        public string Venue { get; set; }
        public string AdditionalInformation { get; set; }
    }
}
