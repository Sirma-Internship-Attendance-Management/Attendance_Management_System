using Attendance_Management_System.Commands;
using Attendance_Management_System.DataAccess;
using Attendance_Management_System.Models;
using Attendance_Management_System.Views.MessageBox;
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

        public Company LoggedCompany { get; set; }

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

        public EditEmployeeViewModel()
        {
        }

        public EditEmployeeViewModel(Employee selectedEmployee, Company Loggedcompany)
        {
            SelectedEmployee = selectedEmployee;
            LoggedCompany = Loggedcompany;
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

            //MessageBox.Show("Employee updated successfully!");
            MessageBoxSuccess mb = new MessageBoxSuccess();
            mb.ShowDialog();

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
