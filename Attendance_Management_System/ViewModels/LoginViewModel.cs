using Attendance_Management_System.Commands;
using Attendance_Management_System.Views;
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

            bool isAuthenticated = AuthenticateUser(username, password);

            if (isAuthenticated)
            {
                MessageBox.Show("Login successful!");

                CompanyAdminView companyAdminView = new CompanyAdminView();
                companyAdminView.Show();

                Application.Current.MainWindow?.Close();
            }
            else
            {
                MessageBox.Show("Invalid username or password. Please try again.");
            }
        }

        private bool CanLogin(object parameter)
        {
            return !string.IsNullOrEmpty(Username) && SecurePassword != null && SecurePassword.Length > 0;
        }

        private bool AuthenticateUser(string username, string password)
        {
            string storedUsername = "admin";
            string storedPassword = "hashedPassword";
            return (username == storedUsername && password == storedPassword);
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
