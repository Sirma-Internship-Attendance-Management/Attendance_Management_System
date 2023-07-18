using Attendance_Management_System.Commands;
using Attendance_Management_System.Models;
using Attendance_Management_System.Views;
using Attendance_Management_System.Views.MessageBox;
using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows;
using System.Windows.Input;

namespace Attendance_Management_System.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _username;
        private SecureString _securePassword;

        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Username)));
            }
        }

        public SecureString SecurePassword
        {
            get { return _securePassword; }
            set
            {
                _securePassword = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SecurePassword)));
            }
        }

        public ICommand LoginCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(Login, CanLogin);
        }

        private void Login(object parameter)
        {
            string username = Username;
            string password = ConvertSecureStringToString(SecurePassword);

            bool isAuthenticated = AuthenticateAdmin(username, password);

            if (isAuthenticated)
            {
                LogInSuccess lgs = new LogInSuccess();
                lgs.ShowDialog();
                

                /*MessageBox.Show("Login successful!");

                CompanyAdminView companyAdminView = new CompanyAdminView();
                companyAdminView.Show();

                Application.Current.MainWindow?.Close();*/
            }
            else
            {
                LogInFail lgf = new LogInFail();
                lgf.ShowDialog();
                //MessageBox.Show("Invalid username or password. Please try again.");
            }
        }

        private bool CanLogin(object parameter)
        {
            return !string.IsNullOrEmpty(Username) && SecurePassword != null && SecurePassword.Length > 0;
        }

        private bool AuthenticateAdmin(string username, string password)
        {
            // Retrieve company admin credentials from the Company model or any other secure storage
            Company storedCompany = GetCompanyByAdminCredentials(username, password);

            if (storedCompany != null)
            {
                // Compare the provided password with the stored admin password
                return password == storedCompany.AdminPassword;
            }

            return false;
        }

        // Simulated function to retrieve company by admin credentials
        private Company GetCompanyByAdminCredentials(string username, string password)
        {
            // In a real-world scenario, you would retrieve the company from the database based on the admin credentials
            // Here, we'll simulate it by hardcoding a company
            if (username == "admin" && password == "password")
            {
                return new Company
                {
                    CompanyId = 1,
                    CompanyName = "My Company",
                    AdminUsername = "admin",
                    AdminPassword = "password"
                };
            }

            return null;
        }

        private string ConvertSecureStringToString(SecureString secureString)
        {
            IntPtr unmanagedString = IntPtr.Zero;
            try
            {
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(secureString);
                string regularString = Marshal.PtrToStringUni(unmanagedString);
                return regularString;
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }
    }
}
