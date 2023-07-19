using Attendance_Management_System.ViewModels;
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
using Attendance_Management_System.ViewModels;
namespace Attendance_Management_System.Views
{
    /// <summary>
    /// Interaction logic for EmployeeManagementView.xaml
    /// </summary>
    public partial class EmployeeManagementView : Window
    {
        

        public EmployeeManagementView()
        {            
            InitializeComponent();
            DataContext = new EmployeeManagementViewModel();
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
            this.Close();
            //Application.Current.Shutdown();
        }
        private void AddEmployee_Click(object sender, RoutedEventArgs e)
        {
            AddEmployeeView addEmployeeView = new AddEmployeeView();
            addEmployeeView.ShowDialog();
        }

        
    }
}
