using Attendance_Management_System.Commands;
using Attendance_Management_System.Models;
using Attendance_Management_System.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Input;

namespace Attendance_Management_System.ViewModels
{
    public class CompanyAdminViewModel : INotifyPropertyChanged
    {

        public ICommand OpenViewCommand { get; }
        public CompanyAdminViewModel()
        {
            OpenViewCommand = new RelayCommand(OpenView);
        }

        private void OpenView(object parameter)
        {


            EmployeeManagementView empman = new EmployeeManagementView();
            empman.Show();

            Application.Current.MainWindow?.Close();

        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}