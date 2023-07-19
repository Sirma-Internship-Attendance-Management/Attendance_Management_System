using Attendance_Management_System.Commands;
using Attendance_Management_System.Views;
using Attendance_Management_System.Views.MessageBox;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace Attendance_Management_System.ViewModels
{
    public class CompanyAdminViewModel : INotifyPropertyChanged
    {
        public ICommand OpenCommand { get; }
        public ICommand OpenCommand2 { get; }

        public ICommand AccountCommand { get; }

        public ICommand SettingsCommand { get; }

        public ICommand InfoCommand { get; }
        public ICommand LogoutCommand { get; }

        //public string uc = "Under Construction!";

        public CompanyAdminViewModel()
        {
            OpenCommand = new RelayCommand(OpenEmployeeManagementView);
            OpenCommand2 = new RelayCommand(OpenAttendanceView);
            AccountCommand = new RelayCommand(OpenAccountManagmentView);
            SettingsCommand = new RelayCommand(OpenSettingsView);
            InfoCommand = new RelayCommand(OpenInfoView);
            LogoutCommand = new RelayCommand(OpenLogoutMB);
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

        private void OpenAccountManagmentView(object parameter)
        {
            //MessageBox.Show(uc);
            UnderConstruction uc = new UnderConstruction();
            uc.ShowDialog();
        }

        private void OpenSettingsView(object parameter)
        {
            UnderConstruction uc = new UnderConstruction();
            uc.ShowDialog();
        }

        private void OpenInfoView(object parameter)
        {
            UnderConstruction uc = new UnderConstruction();
            uc.ShowDialog();
        }

        private void OpenLogoutMB (object parameter)
        {

            //Application.Current.MainWindow?.Close();
            LogoutNotification lgn = new LogoutNotification();
            lgn.ShowDialog();
            

        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
