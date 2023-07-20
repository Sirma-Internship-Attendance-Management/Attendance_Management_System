using System;

namespace Attendance_Management_System.Models
{
    public class Attendance
    {
        public int AttendanceId { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public DateTime CheckInTime { get; set; }
        public DateTime? CheckOutTime { get; set; }

        public Employee Employee { get; set; } // Add this property to establish the relationship

        // Add any additional properties or methods as needed
    }
}
