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

        private ObservableCollection<Employee> _employees;
        private ObservableCollection<Attendance> _attendances;

        public ObservableCollection<Employee> Employees
        {
            get { return _employees; }
            set
            {
                _employees = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Employees)));
            }
        }
        public ObservableCollection<Attendance> Attendances
        {
            get { return _attendances; }
            set
            {
                _attendances = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Attendances)));
            }
        }

        public Company LoggedCompany { get; set; }

        public AttendanceViewModel(Company Loggedcompany)
        {
            LoggedCompany = Loggedcompany;
            LoadEmployees();
            //Employees = LoadEmployees(Employees, LoggedCompany);
            LoadAttendances();
            
        }

        private void LoadEmployees()
        {
            using (var dbContext = new MyDbContext())
            {
                var employees = dbContext.Employees.Where(e => e.CompanyId.Equals(LoggedCompany.CompanyId)).ToList();
                Employees = new ObservableCollection<Employee>(employees);
            }
        }

        /*private static ObservableCollection<Employee> LoadEmployees(ObservableCollection<Employee> EmployeesList, Company loggedcompany)
        {
            using (var dbContext = new MyDbContext())
            {
                var employees = dbContext.Employees.Where(e => e.CompanyId.Equals(loggedcompany.CompanyId)).ToList();
                EmployeesList = new ObservableCollection<Employee>(employees);
                return EmployeesList;
            }
        }*/

        private void LoadAttendances()
        {
            using (var dbContext = new MyDbContext())
            {
                //var employees = dbContext.Employees.Where(e => e.CompanyId.Equals(LoggedCompany.CompanyId)).ToList();
                //Employees = new ObservableCollection<Employee>(employees);                
                var employeeIds = Employees.Select(e => e.EmployeeId).ToList();
                var attendanceRecords = dbContext.AttendanceRecords.Where(a => employeeIds.Contains(a.EmployeeId)).ToList();
                Attendances = new ObservableCollection<Attendance>(attendanceRecords);
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
