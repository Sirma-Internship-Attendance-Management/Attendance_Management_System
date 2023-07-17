using Attendance_Management_System.Commands;
using Attendance_Management_System.Models;
using Attendance_Management_System.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows;

namespace Attendance_Management_System.ViewModels
{
    public class CompanyAdminViewModel : INotifyPropertyChanged
    {

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

        private void OpenView(object parameter)
        {
            

                EmployeeManagment empman = new EmployeeManagment();
                empman.Show();

                Application.Current.MainWindow?.Close();
            
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}