using Attendance_Management_System.Commands;
using Attendance_Management_System.Views;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace Attendance_Management_System.ViewModels
{
    public class CompanyAdminViewModel : INotifyPropertyChanged
    {
        public ICommand OpenCommand { get; }
        public ICommand OpenCommand2 { get; }

        public CompanyAdminViewModel()
        {
            OpenCommand = new RelayCommand(OpenEmployeeManagementView);
            OpenCommand2 = new RelayCommand(OpenAttendanceView);
        }

        private void OpenEmployeeManagementView(object parameter)
        {
            EmployeeManagementView empman = new EmployeeManagementView();
            empman.Show();
        }

        private void OpenAttendanceView(object parameter)
        {
            AttendanceView attendanceView = new AttendanceView();
            attendanceView.Show();
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
