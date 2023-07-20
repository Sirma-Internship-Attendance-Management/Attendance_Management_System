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
using Attendance_Management_System.Models;
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
        private void DeleteEmployee_Click(object sender, RoutedEventArgs e)
        {
            // Get the selected employee from the data grid
            Employee selectedEmployee = EmployeeDataGrid.SelectedItem as Employee;

            // Invoke the DeleteEmployeeCommand in the ViewModel
            if (DataContext is EmployeeManagementViewModel viewModel && selectedEmployee != null)
            {
                viewModel.DeleteEmployeeCommand.Execute(selectedEmployee);
            }
        }
        private void CheckIn_Click(object sender, RoutedEventArgs e)
        {
            // Get the selected employee from the data grid
            Employee selectedEmployee = EmployeeDataGrid.SelectedItem as Employee;

            // Invoke the CheckInCommand in the ViewModel
            if (DataContext is EmployeeManagementViewModel viewModel && selectedEmployee != null)
            {
                viewModel.CheckInCommand.Execute(selectedEmployee);
            }
        }

        private void CheckOut_Click(object sender, RoutedEventArgs e)
        {
            // Get the selected employee from the data grid
            Employee selectedEmployee = EmployeeDataGrid.SelectedItem as Employee;

            // Invoke the CheckOutCommand in the ViewModel
            if (DataContext is EmployeeManagementViewModel viewModel && selectedEmployee != null)
            {
                viewModel.CheckOutCommand.Execute(selectedEmployee);
            }
        }
        private void EditEmployee_Click(object sender, RoutedEventArgs e)
        {
            // Get the selected employee from the data grid
            Employee selectedEmployee = EmployeeDataGrid.SelectedItem as Employee;

            // Check if the selected employee is not null before opening the EditEmployeeView
            if (selectedEmployee != null)
            {
                EditEmployeeView editEmployeeView = new EditEmployeeView(selectedEmployee);
                editEmployeeView.ShowDialog();
            }
        }

    }
}
