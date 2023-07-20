using Attendance_Management_System.Models;
using Attendance_Management_System.ViewModels;
using System.Windows;
using System.Windows.Input;

namespace Attendance_Management_System.Views
{
    public partial class EditEmployeeView : Window
    {
        private EditEmployeeViewModel _viewModel;

        public EditEmployeeView(Employee selectedEmployee)
        {
            InitializeComponent();
            DataContext = new EditEmployeeViewModel(selectedEmployee);

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
            Close();
        }
    }
}
