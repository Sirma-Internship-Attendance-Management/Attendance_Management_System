using Attendance_Management_System.Commands;
using Attendance_Management_System.DataAccess;
using Attendance_Management_System.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace Attendance_Management_System.ViewModels
{
    public class AddEmployeeViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Employee> employees;
        private string name;
        private string position;
        private string contactDetails;

        public ObservableCollection<Employee> Employees
        {
            get { return employees; }
            set
            {
                employees = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }

        public string Position
        {
            get { return position; }
            set
            {
                position = value;
                OnPropertyChanged();
            }
        }

        public string ContactDetails
        {
            get { return contactDetails; }
            set
            {
                contactDetails = value;
                OnPropertyChanged();
            }
        }

        public ICommand SaveEmployeeCommand { get; }

        public AddEmployeeViewModel()
        {
            SaveEmployeeCommand = new RelayCommand(SaveEmployee);
            Employees = new ObservableCollection<Employee>();
        }

        private void SaveEmployee(object parameter)
        {
            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Position) || string.IsNullOrEmpty(ContactDetails))
            {
                MessageBox.Show("Warning! You cannot add Employees empty fields!");
            }
            else
            {
                Employee newEmployee = new Employee
                {
                    Name = Name,
                    Position = Position,
                    ContactDetails = ContactDetails
                };

                using (var context = new MyDbContext())
                {
                    context.Employees.Add(newEmployee);
                    context.SaveChanges();
                }

                Employees.Add(newEmployee);

                ClearFields();
                MessageBox.Show("You successfully added employee!");
            }
        }


        private void ClearFields()
        {
            Name = string.Empty;
            Position = string.Empty;
            ContactDetails = string.Empty;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
