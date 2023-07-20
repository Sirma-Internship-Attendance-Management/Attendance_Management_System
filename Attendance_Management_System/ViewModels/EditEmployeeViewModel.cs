using Attendance_Management_System.Commands;
using Attendance_Management_System.DataAccess;
using Attendance_Management_System.Models;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace Attendance_Management_System.ViewModels
{
    public class EditEmployeeViewModel : INotifyPropertyChanged
    {
        private Employee _selectedEmployee;
        private string _editedName;
        private string _editedPosition;
        private string _editedContactDetails;

        public event PropertyChangedEventHandler PropertyChanged;

        public Employee SelectedEmployee
        {
            get { return _selectedEmployee; }
            set
            {
                _selectedEmployee = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedEmployee)));
            }
        }

        public string EditedName
        {
            get { return _editedName; }
            set
            {
                _editedName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(EditedName)));
            }
        }

        public string EditedPosition
        {
            get { return _editedPosition; }
            set
            {
                _editedPosition = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(EditedPosition)));
            }
        }

        public string EditedContactDetails
        {
            get { return _editedContactDetails; }
            set
            {
                _editedContactDetails = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(EditedContactDetails)));
            }
        }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        // Parameterless constructor required for XAML
        public EditEmployeeViewModel()
        {
            // Initialize properties or perform any other required actions here
        }

        public EditEmployeeViewModel(Employee selectedEmployee)
        {
            SelectedEmployee = selectedEmployee;
            InitializeProperties();

            SaveCommand = new RelayCommand(SaveChanges, CanSaveChanges);
            CancelCommand = new RelayCommand(Cancel);
            
        }

        private void InitializeProperties()
        {
            EditedName = SelectedEmployee.Name;
            EditedPosition = SelectedEmployee.Position;
            EditedContactDetails = SelectedEmployee.ContactDetails;
        }

        private bool CanSaveChanges(object parameter)
        {
            // Add validation logic here if needed
            return true;
        }

        private void SaveChanges(object parameter)
        {
            using (var dbContext = new MyDbContext())
            {
                var employeeToUpdate = dbContext.Employees.Find(SelectedEmployee.EmployeeId);
                if (employeeToUpdate != null)
                {
                    employeeToUpdate.Name = EditedName;
                    employeeToUpdate.Position = EditedPosition;
                    employeeToUpdate.ContactDetails = EditedContactDetails;

                    dbContext.SaveChanges();
                }
            }

            MessageBox.Show("Employee updated successfully!");

            CloseWindow(parameter);
        }


        private void Cancel(object parameter)
        {
            CloseWindow(parameter);
        }

        private void CloseWindow(object parameter)
        {
            if (parameter is Window window)
            {
                window.Close();
            }
        }
    }
}
