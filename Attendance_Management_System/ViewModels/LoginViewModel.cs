using Attendance_Management_System.Commands;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace Attendance_Management_System.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _username;
        private string _password;

        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Username)));
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Password)));
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
            string password = Password;

            bool isAuthenticated = AuthenticateUser(username, password);

            if (isAuthenticated)
            {
                MessageBox.Show("Login successful!");
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                Application.Current.MainWindow?.Close();
            }
            else
            {
                // Authentication failed
                MessageBox.Show("Invalid username or password. Please try again.");
            }
        }

        private bool CanLogin(object parameter)
        {
            return !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password);
        }

        private bool AuthenticateUser(string username, string password)
        {
            string storedUsername = "admin";
            string storedPassword = "hashedPassword";
            return (username == storedUsername && password == storedPassword);
        }
    }
}
