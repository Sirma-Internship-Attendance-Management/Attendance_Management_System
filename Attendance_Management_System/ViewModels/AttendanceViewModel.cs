using Attendance_Management_System.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Attendance_Management_System.ViewModels
{
    public class AttendanceViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Attendance> Attendances { get; set; }

        public AttendanceViewModel()
        {
            Attendances = new ObservableCollection<Attendance>();
            // Populate the Attendances collection with data
            // You can fetch the data from a database or any other source

            // Example data
            Attendances.Add(new Attendance { AttendanceId = 1, EmployeeId = 1, EventId = 1, CheckInTime = DateTime.Now, CheckOutTime = DateTime.Now.AddHours(2) });
            Attendances.Add(new Attendance { AttendanceId = 2, EmployeeId = 2, EventId = 1, CheckInTime = DateTime.Now, CheckOutTime = DateTime.Now.AddHours(3) });
            Attendances.Add(new Attendance { AttendanceId = 3, EmployeeId = 1, EventId = 2, CheckInTime = DateTime.Now, CheckOutTime = DateTime.Now.AddHours(1) });
            Attendances.Add(new Attendance { AttendanceId = 4, EmployeeId = 3, EventId = 1, CheckInTime = DateTime.Now, CheckOutTime = DateTime.Now.AddHours(4) });
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
