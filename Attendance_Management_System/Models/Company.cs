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
        public string AdminUsername { get; set; }
        public string AdminPassword { get; set; }
        
        /*  //new Information Added
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }

        public byte[]? Logo { get; set; }*/
    }

}
