using Attendance_Management_System.Commands;
using Attendance_Management_System.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Attendance_Management_System.Views;
using Attendance_Management_System.DataAccess;

namespace Attendance_Management_System.ViewModels
{
    public class EmployeeManagementViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<Employee> _employees;
        private ObservableCollection<Attendance> _attendance;
        private string _connectionString = "DbConnect";

        public DatePicker StartDatePicker { get; set; }
        public DatePicker EndDatePicker { get; set; }
        public ListBox EmployeeListBox { get; set; }

        public ObservableCollection<Employee> Employees
        {
            get { return _employees; }
            set
            {
                _employees = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Employees)));
            }
        }

        public ObservableCollection<Attendance> Attendance
        {
            get { return _attendance; }
            set
            {
                _attendance = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Attendance)));
            }
        }

        public RelayCommand AddEmployeeCommand { get; }
        public RelayCommand EditEmployeeCommand { get; }
        public RelayCommand DeleteEmployeeCommand { get; }
        public RelayCommand CheckInCommand { get; }
        public RelayCommand CheckOutCommand { get; }
        public RelayCommand GenerateAttendanceReportCommand { get; }
        public RelayCommand SaveEmployeeCommand { get; }

        public EmployeeManagementViewModel()
        {
            AddEmployeeCommand = new RelayCommand(AddEmployee);
            EditEmployeeCommand = new RelayCommand(EditEmployee, CanEditOrDeleteEmployee);
            DeleteEmployeeCommand = new RelayCommand(DeleteEmployee, CanEditOrDeleteEmployee);
            CheckInCommand = new RelayCommand(CheckIn, CanCheckIn);
            CheckOutCommand = new RelayCommand(CheckOut, CanCheckOut);
            GenerateAttendanceReportCommand = new RelayCommand(GenerateAttendanceReport, CanGenerateAttendanceReport);
            SaveEmployeeCommand = new RelayCommand(SaveEmployee);

            Employees = new ObservableCollection<Employee>();
            Attendance = new ObservableCollection<Attendance>();

            LoadEmployees();
            LoadAttendance();
        }
        private void LoadEmployees()
        {
            using (var dbContext = new MyDbContext())
            {
                var employees = dbContext.Employees.ToList();
                Employees = new ObservableCollection<Employee>(employees);
            }
        }

        private void LoadAttendance()
        {
            using (var dbContext = new MyDbContext())
            {
                var attendanceRecords = dbContext.AttendanceRecords.ToList();
                Attendance = new ObservableCollection<Attendance>(attendanceRecords);
            }
        }


        private void AddEmployee(object parameter)
        {
            Employee newEmployee = new Employee();
            Employees.Add(newEmployee);
        }

        private void EditEmployee(object parameter)
        {
            Employee selectedEmployee = parameter as Employee;
            if (selectedEmployee != null)
            {
                // Update selectedEmployee in the database
            }
        }

        private void DeleteEmployee(object parameter)
        {
            Employee selectedEmployee = parameter as Employee;
            if (selectedEmployee != null)
            {
                MessageBoxResult result = MessageBox.Show($"Are you sure you want to delete the employee {selectedEmployee.Name}?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    Employees.Remove(selectedEmployee);
                }
            }
        }

        private bool CanEditOrDeleteEmployee(object parameter)
        {
            return true;
        }

        private void CheckIn(object parameter)
        {
            Employee selectedEmployee = parameter as Employee;

            if (selectedEmployee != null)
            {
                bool alreadyCheckedIn = Attendance.Any(a => a.EmployeeId == selectedEmployee.EmployeeId);

                if (alreadyCheckedIn)
                {
                    MessageBox.Show($"Employee {selectedEmployee.Name} is already checked in.");
                }
                else
                {
                    Attendance newAttendance = new Attendance
                    {
                        EmployeeId = selectedEmployee.EmployeeId,
                        CheckInTime = DateTime.Now
                    };

                    Attendance.Add(newAttendance);
                    MessageBox.Show($"Employee {selectedEmployee.Name} checked in.");
                }
            }
        }

        private bool CanCheckIn(object parameter)
        {
            Employee selectedEmployee = parameter as Employee;

            return selectedEmployee != null;
        }

        private void CheckOut(object parameter)
        {
            Employee selectedEmployee = parameter as Employee;

            if (selectedEmployee != null)
            {
                Attendance attendance = Attendance.FirstOrDefault(a => a.EmployeeId == selectedEmployee.EmployeeId);

                if (attendance != null)
                {
                    attendance.CheckOutTime = DateTime.Now;
                    MessageBox.Show($"Employee {selectedEmployee.Name} checked out.");
                }
                else
                {
                    MessageBox.Show($"Employee {selectedEmployee.Name} has not checked in.");
                }
            }
        }

        private bool CanCheckOut(object parameter)
        {
            Employee selectedEmployee = parameter as Employee;

            bool alreadyCheckedIn = Attendance.Any(a => a.EmployeeId == selectedEmployee.EmployeeId);

            return selectedEmployee != null && alreadyCheckedIn;
        }

        private void GenerateAttendanceReport(object parameter)
        {
            DateTime startDate = GetSelectedStartDate();
            DateTime endDate = GetSelectedEndDate();
            Employee selectedEmployee = GetSelectedEmployee();


            var filteredAttendance = Attendance.Where(a =>
                (startDate == default || a.CheckInTime.Date >= startDate) &&
                (endDate == default || a.CheckInTime.Date <= endDate) &&
                (selectedEmployee == null || a.EmployeeId == selectedEmployee.EmployeeId)
            );

            StringBuilder report = new StringBuilder();
            report.AppendLine("Attendance Report");
            report.AppendLine("------------------------------");
            report.AppendLine($"Start Date: {startDate}");
            report.AppendLine($"End Date: {endDate}");
            report.AppendLine();

            foreach (var attendance in filteredAttendance)
            {
                Employee employee = Employees.FirstOrDefault(e => e.EmployeeId == attendance.EmployeeId);

                if (employee != null)
                {
                    report.AppendLine($"Employee: {employee.Name}");
                    report.AppendLine($"Check-In Time: {attendance.CheckInTime}");
                    report.AppendLine($"Check-Out Time: {attendance.CheckOutTime?.ToString() ?? "N/A"}");
                    report.AppendLine("------------------------------");
                }
            }

            MessageBox.Show(report.ToString(), "Attendance Report");
        }

        private bool CanGenerateAttendanceReport(object parameter)
        {
            return Attendance.Count > 0;
        }

        private DateTime GetSelectedStartDate()
        {
            return StartDatePicker.SelectedDate ?? DateTime.MinValue;
        }

        private DateTime GetSelectedEndDate()
        {
            return EndDatePicker.SelectedDate ?? DateTime.MaxValue;
        }

        private Employee GetSelectedEmployee()
        {
            return EmployeeListBox.SelectedItem as Employee;
        }

        private void SaveEmployee(object parameter)
        {
            Employee employee = parameter as Employee;
            if (employee != null)
            {
                Employees.Add(employee);

                // Close the AddEmployeeView or perform any other necessary actions
            }
        }

    }
}
