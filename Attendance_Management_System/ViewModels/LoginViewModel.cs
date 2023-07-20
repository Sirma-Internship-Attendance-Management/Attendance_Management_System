﻿using Attendance_Management_System.Commands;
using Attendance_Management_System.DataAccess;
using Attendance_Management_System.Models;
using Attendance_Management_System.Views.MessageBox;
using System;
using System.ComponentModel;
using System.Linq;
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

        public ICommand LoginCommand { get; }
        public ICommand ResetPasswordCommand { get; }

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

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(Login, CanLogin);
            ResetPasswordCommand = new RelayCommand(ResetPasswordView);
        }

        private void Login(object parameter)
        {
            string username = Username;
            string password = ConvertSecureStringToString(SecurePassword);

            bool isAuthenticated = AuthenticateUser(username, password);

            if (isAuthenticated)
            {
                LogInSuccess lgs = new LogInSuccess();
                lgs.ShowDialog();

                // Close the login window and open the main application window
                Application.Current.MainWindow?.Close();
            }
            else
            {
                LogInFail lgf = new LogInFail();
                lgf.ShowDialog();
            }
        }

        private bool CanLogin(object parameter)
        {
            return !string.IsNullOrEmpty(Username) && SecurePassword != null && SecurePassword.Length > 0;
        }

        private bool AuthenticateUser(string username, string password)
        {
            User storedUser = GetUserByCredentials(username, password);

            if (storedUser != null)
            {
                // Replace the placeholder VerifyPassword method with your actual password hashing and salting implementation
                return VerifyPassword(password, storedUser.Password);
            }

            return false;
        }

        private User GetUserByCredentials(string username, string password)
        {
            using (var dbContext = new MyDbContext())
            {
                // Find the user with the provided username in the database
                User user = dbContext.Users.FirstOrDefault(u => u.Username == username);
                return user;
            }
        }

        private bool VerifyPassword(string providedPassword, string hashedPassword)
        {
            return providedPassword == hashedPassword;
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

        private void ResetPasswordView(object parameter)
        {
            UnderConstruction uc = new UnderConstruction();
            uc.ShowDialog();
        }
    }
}
