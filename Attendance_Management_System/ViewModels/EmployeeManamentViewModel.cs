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

namespace Attendance_Management_System.ViewModels
{
    public class EmployeeManagementViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<Employee> _employees;
        private ObservableCollection<Attendance> _attendance;
        private string _connectionString = "dbConnect";

        
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

        public EmployeeManagementViewModel()
        {
            AddEmployeeCommand = new RelayCommand(AddEmployee);
            EditEmployeeCommand = new RelayCommand(EditEmployee, CanEditOrDeleteEmployee);
            DeleteEmployeeCommand = new RelayCommand(DeleteEmployee, CanEditOrDeleteEmployee);
            CheckInCommand = new RelayCommand(CheckIn, CanCheckIn);
            CheckOutCommand = new RelayCommand(CheckOut, CanCheckOut);
            GenerateAttendanceReportCommand = new RelayCommand(GenerateAttendanceReport, CanGenerateAttendanceReport);

            Employees = new ObservableCollection<Employee>();
            Attendance = new ObservableCollection<Attendance>();

            LoadEmployees();
            LoadAttendance();
        }

        private void LoadEmployees()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM Employees";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int employeeId = reader.GetInt32(0);
                            string name = reader.GetString(1);
                            string position = reader.GetString(2);
                            string contactDetails = reader.GetString(3);

                            Employee employee = new Employee
                            {
                                EmployeeId = employeeId,
                                Name = name,
                                Position = position,
                                ContactDetails = contactDetails
                            };

                            Employees.Add(employee);
                        }
                    }
                }
            }
        }

        private void LoadAttendance()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM Attendance";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int attendanceId = reader.GetInt32(0);
                            int employeeId = reader.GetInt32(1);
                            DateTime checkInTime = reader.GetDateTime(2);
                            DateTime? checkOutTime = reader.IsDBNull(3) ? (DateTime?)null : reader.GetDateTime(3);

                            Attendance attendance = new Attendance
                            {
                                AttendanceId = attendanceId,
                                EmployeeId = employeeId,
                                CheckInTime = checkInTime,
                                CheckOutTime = checkOutTime
                            };

                            Attendance.Add(attendance);
                        }
                    }
                }
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
    }
}
