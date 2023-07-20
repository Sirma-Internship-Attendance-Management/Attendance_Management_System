using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendance_Management_System.Models
{
    public class Company
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string? Type { get; set; }
        
        public string? Website { get; set; }

        public string? Address { get; set; }       

        public byte[]? Logo { get; set; }
    }

}
