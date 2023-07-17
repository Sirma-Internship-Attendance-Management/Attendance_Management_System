using Attendance_Management_System.Commands;
using Attendance_Management_System.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace Attendance_Management_System.ViewModels
{
    public class CompanyAdminViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<Employee> _employees;

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
            // Retrieve employee records from the database and populate the Employees collection
            // Replace the code below with your actual data retrieval logic
            Employees.Add(new Employee { Name = "John Doe", Position = "Manager", ContactDetails = "john.doe@example.com" });
            Employees.Add(new Employee { Name = "Jane Smith", Position = "Supervisor", ContactDetails = "jane.smith@example.com" });
            // ...
        }

        private void AddEmployee(object parameter)
        {
            // Create a new instance of the Employee class
            Employee newEmployee = new Employee();

            // Show a dialog or navigate to a new view for entering employee details
            // Update the newEmployee object with the entered details

            // Add the new employee to the Employees collection
            Employees.Add(newEmployee);

            // Optionally, save the new employee to the database or perform any other required operations
        }

        private void EditEmployee(object parameter)
        {
            Employee selectedEmployee = parameter as Employee;

            if (selectedEmployee != null)
            {
                // Show a dialog or navigate to a new view for editing employee details
                // Update the selectedEmployee object with the edited details

                // Optionally, save the updated employee to the database or perform any other required operations
            }
        }

        private void DeleteEmployee(object parameter)
        {
            Employee selectedEmployee = parameter as Employee;

            if (selectedEmployee != null)
            {
                // Show a confirmation dialog to confirm the deletion
                MessageBoxResult result = MessageBox.Show($"Are you sure you want to delete the employee {selectedEmployee.Name}?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    // Remove the selected employee from the Employees collection
                    Employees.Remove(selectedEmployee);

                    // Optionally, delete the employee from the database or perform any other required operations
                }
            }
        }

        private bool CanEditOrDeleteEmployee(object parameter)
        {
            // Determine whether an employee can be edited or deleted based on your business rules
            // For example, only enable editing or deletion if an employee is selected in the data grid
            return true; // Replace with your actual condition
        }
    }
}
