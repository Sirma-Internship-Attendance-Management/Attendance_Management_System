using Attendance_Management_System.ViewModels;
using Attendance_Management_System.Views.MessageBox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Attendance_Management_System.Views
{
    /// <summary>
    /// Interaction logic for CompanyAdminView.xaml
    /// </summary>
    public partial class CompanyAdminView : Window
    {

        private readonly CompanyAdminViewModel _viewModel;
        public CompanyAdminView()
        {
            InitializeComponent();
            _viewModel = new CompanyAdminViewModel();
            DataContext = _viewModel;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnMaximize_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                WindowState = WindowState.Normal; 
            }
            else
            {
                WindowState = WindowState.Maximized; 
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.OpenViewCommand.Execute(null);
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            LogoutNotification lgn = new LogoutNotification();
            lgn.ShowDialog();
            this.Close();
        }
    }
}
