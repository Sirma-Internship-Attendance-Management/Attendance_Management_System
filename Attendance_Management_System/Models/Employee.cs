using System.Collections.Generic;

namespace Attendance_Management_System.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        
        public string ContactDetails { get; set; }
        public ICollection<Attendance> Attendances { get; set; }

        public bool IsCheckedIn
        {
            get
            {
                foreach (var attendance in Attendances)
                {
                    if (attendance.CheckOutTime == null)
                        return true;
                }
                return false;
            }
        }
    }
}
