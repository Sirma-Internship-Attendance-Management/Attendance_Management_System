using Attendance_Management_System.Commands;
using Attendance_Management_System.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Windows;

namespace Attendance_Management_System.ViewModels
{
    public class CompanyAdminViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<Employee> _employees;
        private string _connectionString = "dbConnect"; 

        public ObservableCollection<Employee> Employees
        {
            get { return _employees; }
            set
            {
                _employees = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Employees)));
            }
        }

        public RelayCommand AddEmployeeCommand { get; }
        public RelayCommand EditEmployeeCommand { get; }
        public RelayCommand DeleteEmployeeCommand { get; }

        public CompanyAdminViewModel()
        {
            AddEmployeeCommand = new RelayCommand(AddEmployee);
            EditEmployeeCommand = new RelayCommand(EditEmployee, CanEditOrDeleteEmployee);
            DeleteEmployeeCommand = new RelayCommand(DeleteEmployee, CanEditOrDeleteEmployee);

            Employees = new ObservableCollection<Employee>();

            LoadEmployees();
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
                            string name = reader.GetString(0);
                            string position = reader.GetString(1);
                            string contactDetails = reader.GetString(2);

                            Employee employee = new Employee
                            {
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

        private void AddEmployee(object parameter)
        {
            Employee newEmployee = new Employee();
            // Add newEmployee to the database

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
    }
}
