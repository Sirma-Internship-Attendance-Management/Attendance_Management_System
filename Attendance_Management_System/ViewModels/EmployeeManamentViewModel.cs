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
        private ObservableCollection<Event> _events;
        private ObservableCollection<Attendance> _attendance;
        private string _connectionString = "dbConnect";

        // Add properties for the UI controls
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

        public ObservableCollection<Event> Events
        {
            get { return _events; }
            set
            {
                _events = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Events)));
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
        public RelayCommand AddEventCommand { get; }
        public RelayCommand EditEventCommand { get; }
        public RelayCommand DeleteEventCommand { get; }
        public RelayCommand CheckInCommand { get; }
        public RelayCommand CheckOutCommand { get; }
        public RelayCommand GenerateAttendanceReportCommand { get; }

        public EmployeeManagementViewModel()
        {
            AddEmployeeCommand = new RelayCommand(AddEmployee);
            EditEmployeeCommand = new RelayCommand(EditEmployee, CanEditOrDeleteEmployee);
            DeleteEmployeeCommand = new RelayCommand(DeleteEmployee, CanEditOrDeleteEmployee);
            AddEventCommand = new RelayCommand(AddEvent);
            EditEventCommand = new RelayCommand(EditEvent, CanEditOrDeleteEvent);
            DeleteEventCommand = new RelayCommand(DeleteEvent, CanEditOrDeleteEvent);
            CheckInCommand = new RelayCommand(CheckIn, CanCheckIn);
            CheckOutCommand = new RelayCommand(CheckOut, CanCheckOut);
            GenerateAttendanceReportCommand = new RelayCommand(GenerateAttendanceReport, CanGenerateAttendanceReport);

            Employees = new ObservableCollection<Employee>();
            Events = new ObservableCollection<Event>();
            Attendance = new ObservableCollection<Attendance>();

            LoadEmployees();
            LoadEvents();
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

        private void LoadEvents()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM Events";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int eventId = reader.GetInt32(0);
                            string eventName = reader.GetString(1);
                            DateTime eventDate = reader.GetDateTime(2);
                            string venue = reader.GetString(3);
                            string additionalInformation = reader.GetString(4);

                            Event @event = new Event
                            {
                                EventId = eventId,
                                EventName = eventName,
                                EventDate = eventDate,
                                Venue = venue,
                                AdditionalInformation = additionalInformation
                            };

                            Events.Add(@event);
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
                            int eventId = reader.GetInt32(2);
                            DateTime checkInTime = reader.GetDateTime(3);
                            DateTime? checkOutTime = reader.IsDBNull(4) ? (DateTime?)null : reader.GetDateTime(4);

                            Attendance attendance = new Attendance
                            {
                                AttendanceId = attendanceId,
                                EmployeeId = employeeId,
                                EventId = eventId,
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

        private void AddEvent(object parameter)
        {
            Event newEvent = new Event();
            Events.Add(newEvent);
        }

        private void EditEvent(object parameter)
        {
            Event selectedEvent = parameter as Event;
            if (selectedEvent != null)
            {
                // Update selectedEvent in the database
            }
        }

        private void DeleteEvent(object parameter)
        {
            Event selectedEvent = parameter as Event;
            if (selectedEvent != null)
            {
                MessageBoxResult result = MessageBox.Show($"Are you sure you want to delete the event {selectedEvent.EventName}?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    Events.Remove(selectedEvent);
                }
            }
        }

        private bool CanEditOrDeleteEvent(object parameter)
        {
            return true;
        }

        private void CheckIn(object parameter)
        {
            Employee selectedEmployee = parameter as Employee;
            Event selectedEvent = GetSelectedEvent();

            if (selectedEmployee != null && selectedEvent != null)
            {
                // Check if the employee is already checked in for the selected event
                bool alreadyCheckedIn = Attendance.Any(a => a.EmployeeId == selectedEmployee.EmployeeId && a.EventId == selectedEvent.EventId);

                if (alreadyCheckedIn)
                {
                    MessageBox.Show($"Employee {selectedEmployee.Name} is already checked in for the event {selectedEvent.EventName}.");
                }
                else
                {
                    Attendance newAttendance = new Attendance
                    {
                        EmployeeId = selectedEmployee.EmployeeId,
                        EventId = selectedEvent.EventId,
                        CheckInTime = DateTime.Now
                    };

                    Attendance.Add(newAttendance);
                    MessageBox.Show($"Employee {selectedEmployee.Name} checked in for the event {selectedEvent.EventName}.");
                }
            }
        }

        private bool CanCheckIn(object parameter)
        {
            Employee selectedEmployee = parameter as Employee;
            Event selectedEvent = GetSelectedEvent();

            return selectedEmployee != null && selectedEvent != null;
        }

        private void CheckOut(object parameter)
        {
            Employee selectedEmployee = parameter as Employee;
            Event selectedEvent = GetSelectedEvent();

            if (selectedEmployee != null && selectedEvent != null)
            {
                // Find the attendance record for the selected employee and event
                Attendance attendance = Attendance.FirstOrDefault(a => a.EmployeeId == selectedEmployee.EmployeeId && a.EventId == selectedEvent.EventId);

                if (attendance != null)
                {
                    attendance.CheckOutTime = DateTime.Now;
                    MessageBox.Show($"Employee {selectedEmployee.Name} checked out of the event {selectedEvent.EventName}.");
                }
                else
                {
                    MessageBox.Show($"Employee {selectedEmployee.Name} has not checked in for the event {selectedEvent.EventName}.");
                }
            }
        }

        private bool CanCheckOut(object parameter)
        {
            Employee selectedEmployee = parameter as Employee;
            Event selectedEvent = GetSelectedEvent();

            // Check if the employee is already checked in for the selected event
            bool alreadyCheckedIn = Attendance.Any(a => a.EmployeeId == selectedEmployee.EmployeeId && a.EventId == selectedEvent.EventId);

            return selectedEmployee != null && selectedEvent != null && alreadyCheckedIn;
        }

        private Event GetSelectedEvent()
        {

            return Events.FirstOrDefault();
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

        private void GenerateAttendanceReport(object parameter)
        {
            // Get the selected criteria for generating the report
            DateTime startDate = GetSelectedStartDate(); // Replace with the code to get the selected start date from the UI
            DateTime endDate = GetSelectedEndDate(); // Replace with the code to get the selected end date from the UI
            Employee selectedEmployee = GetSelectedEmployee(); // Replace with the code to get the selected employee from the UI
            Event selectedEvent = GetSelectedEvent(); // Replace with the code to get the selected event from the UI

            // Filter the attendance records based on the selected criteria
            var filteredAttendance = Attendance.Where(a =>
                (startDate == default || a.CheckInTime.Date >= startDate) &&
                (endDate == default || a.CheckInTime.Date <= endDate) &&
                (selectedEmployee == null || a.EmployeeId == selectedEmployee.EmployeeId) &&
                (selectedEvent == null || a.EventId == selectedEvent.EventId)
            );

            // Generate the report based on the filtered attendance records
            StringBuilder report = new StringBuilder();
            report.AppendLine("Attendance Report");
            report.AppendLine("------------------------------");
            report.AppendLine($"Start Date: {startDate}");
            report.AppendLine($"End Date: {endDate}");
            report.AppendLine();

            foreach (var attendance in filteredAttendance)
            {
                Employee employee = Employees.FirstOrDefault(e => e.EmployeeId == attendance.EmployeeId);
                Event @event = Events.FirstOrDefault(ev => ev.EventId == attendance.EventId);

                if (employee != null && @event != null)
                {
                    report.AppendLine($"Employee: {employee.Name}");
                    report.AppendLine($"Event: {@event.EventName}");
                    report.AppendLine($"Check-In Time: {attendance.CheckInTime}");
                    report.AppendLine($"Check-Out Time: {attendance.CheckOutTime?.ToString() ?? "N/A"}");
                    report.AppendLine("------------------------------");
                }
            }

            // Display the report or save it to a file, depending on your requirements
            MessageBox.Show(report.ToString(), "Attendance Report");
        }

        private bool CanGenerateAttendanceReport(object parameter)
        {
            // Enable the report generation if there is at least one attendance record
            return Attendance.Count > 0;
        }


    }
}
