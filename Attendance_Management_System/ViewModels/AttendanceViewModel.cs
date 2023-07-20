using Attendance_Management_System.DataAccess;
using Attendance_Management_System.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace Attendance_Management_System.ViewModels
{
    public class AttendanceViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<Attendance> _attendances;
        public ObservableCollection<Attendance> Attendances
        {
            get { return _attendances; }
            set
            {
                _attendances = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Attendances)));
            }
        }

        public AttendanceViewModel()
        {
            LoadAttendances();
        }

        private void LoadAttendances()
        {
            using (var dbContext = new MyDbContext())
            {
                var attendances = dbContext.AttendanceRecords.ToList();
                Attendances = new ObservableCollection<Attendance>(attendances);
            }

            // After loading the attendances, you can populate the EmployeeName property for each Attendance.
            using (var dbContext = new MyDbContext())
            {
                foreach (var attendance in Attendances)
                {
                    var employee = dbContext.Employees.FirstOrDefault(e => e.EmployeeId == attendance.EmployeeId);
                    if (employee != null)
                    {
                        attendance.EmployeeName = employee.Name;
                    }
                }
            }
        }
    }
}
