using Attendance_Management_System.Commands;
using Attendance_Management_System.DataAccess;
using Attendance_Management_System.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Attendance_Management_System.ViewModels
{
    public class EmployeeManagementViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Employee> _employees;
        private ObservableCollection<Attendance> _attendance;

        public event PropertyChangedEventHandler PropertyChanged;

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

        public EmployeeManagementViewModel()
        {
            AddEmployeeCommand = new RelayCommand(AddEmployee);
            EditEmployeeCommand = new RelayCommand(EditEmployee, CanEditOrDeleteEmployee);
            DeleteEmployeeCommand = new RelayCommand(DeleteEmployee, CanEditOrDeleteEmployee);
            CheckInCommand = new RelayCommand(CheckIn, CanCheckIn);
            CheckOutCommand = new RelayCommand(CheckOut, CanCheckOut);

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
            LoadEmployees();
        }

        private void EditEmployee(object parameter)
        {
            Employee selectedEmployee = parameter as Employee;            
                LoadEmployees();
        }

        private void DeleteEmployee(object parameter)
        {
            Employee selectedEmployee = parameter as Employee;
            if (selectedEmployee != null)
            {
                MessageBoxResult result = MessageBox.Show($"Are you sure you want to delete the employee {selectedEmployee.Name}?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    using (var dbContext = new MyDbContext())
                    {
                        var employeeToDelete = dbContext.Employees.Find(selectedEmployee.EmployeeId);
                        if (employeeToDelete != null)
                        {
                            dbContext.Employees.Remove(employeeToDelete);
                            dbContext.SaveChanges();
                        }
                    }
                    Employees.Remove(selectedEmployee);
                    LoadEmployees();
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
                using (var dbContext = new MyDbContext())
                {
                    var attendance = new Attendance
                    {
                        EmployeeId = selectedEmployee.EmployeeId,
                        CheckInTime = DateTime.Now
                    };
                    dbContext.AttendanceRecords.Add(attendance);
                    dbContext.SaveChanges();
                }

                LoadAttendance();
                MessageBox.Show($"Employee {selectedEmployee.Name} checked in.");
            }
            else
            {
                MessageBox.Show("Please select a valid employee.");
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
                using (var dbContext = new MyDbContext())
                {
                    var latestCheckIn = dbContext.AttendanceRecords
                        .Where(a => a.EmployeeId == selectedEmployee.EmployeeId && a.CheckOutTime == null)
                        .OrderByDescending(a => a.CheckInTime)
                        .FirstOrDefault();

                    if (latestCheckIn != null)
                    {
                        latestCheckIn.CheckOutTime = DateTime.Now;
                        dbContext.SaveChanges();
                    }
                    else
                    {
                        MessageBox.Show($"Employee {selectedEmployee.Name} has not checked in.");
                    }
                }

                LoadAttendance();
                MessageBox.Show($"Employee {selectedEmployee.Name} checked out.");
            }
            else
            {
                MessageBox.Show("Please select a valid employee.");
            }
        }


        private bool CanCheckOut(object parameter)
        {
            Employee selectedEmployee = parameter as Employee;

            bool alreadyCheckedIn = Attendance.Any(a => a.EmployeeId == selectedEmployee.EmployeeId);

            return selectedEmployee != null && alreadyCheckedIn;
        }
    }
}
