using Attendance_Management_System.Commands;
using Attendance_Management_System.DataAccess;
using Attendance_Management_System.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace Attendance_Management_System.ViewModels
{
    public class AttendanceViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Attendance> _attendances;
        private Employee _selectedEmployee;

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Attendance> Attendances
        {
            get { return _attendances; }
            set
            {
                _attendances = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Attendances)));
            }
        }

        public Employee SelectedEmployee
        {
            get { return _selectedEmployee; }
            set
            {
                _selectedEmployee = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedEmployee)));
            }
        }

        public AttendanceViewModel()
        {
            LoadAttendances();
        }

        private void LoadAttendances()
        {
            using (var dbContext = new MyDbContext())
            {
                var attendances = dbContext.AttendanceRecords.ToList();
                Attendances = new ObservableCollection<Attendance>(attendances);
            }
        }
    }
}
