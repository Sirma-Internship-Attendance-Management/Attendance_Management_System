using Attendance_Management_System.Commands;
using Attendance_Management_System.DataAccess;
using Attendance_Management_System.Models;
using Attendance_Management_System.Views;
using Attendance_Management_System.Views.MessageBox;
using System.ComponentModel;
using System.Linq;
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

        private Company _loggedCompany { get; set; }

        public Company LoggedCompany
        {
            get { return _loggedCompany; }
            set
            {
                _loggedCompany = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(LoggedCompany)));
            }
        }

        private string _companyName;
        public string CompanyName
        {
            get { return _companyName; }
            set
            {
                if (_companyName != value)
                {
                    _companyName = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CompanyName)));
                }
            }
        }

        private string _type;
        public string Type
        {
            get { return _type; }
            set
            {
                if (_type != value)
                {
                    _type = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Type)));
                }
            }
        }

        private string _companyWebsite;
        public string CompanyWebsite
        {
            get { return _companyWebsite; }
            set
            {
                if (_companyWebsite != value)
                {
                    _companyWebsite = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CompanyWebsite)));
                }
            }
        }

        private string _companyAddress;
        public string CompanyAddress
        {
            get { return _companyAddress; }
            set
            {
                if (_companyAddress != value)
                {
                    _companyAddress = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CompanyAddress)));
                }
            }
        }


        //public string uc = "Under Construction!";

        public CompanyAdminViewModel()
        {
            OpenCommand = new RelayCommand(OpenEmployeeManagementView);
            OpenCommand2 = new RelayCommand(OpenAttendanceView);
            AccountCommand = new RelayCommand(OpenAccountManagmentView);
            SettingsCommand = new RelayCommand(OpenSettingsView);
            InfoCommand = new RelayCommand(OpenInfoView);
            LogoutCommand = new RelayCommand(OpenLogoutMB);
            //int companyID = 1;
            LoggedCompany = getLoggedInUserCompany(1);
            CompanyInformationBinding();
        }

        public void CompanyInformationBinding()
        {
            CompanyName = LoggedCompany.CompanyName;
            Type = LoggedCompany.Type;
            CompanyWebsite = LoggedCompany.Website;
            CompanyAddress = LoggedCompany.Address;
        }

        public Company getLoggedInUserCompany(int company_id)
        {
            using (var dbContext = new MyDbContext())
            {
                Company company = dbContext.Companies.FirstOrDefault(u => u.CompanyId == company_id);
                return company;
            }
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
