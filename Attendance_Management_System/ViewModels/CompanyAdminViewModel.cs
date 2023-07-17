using Attendance_Management_System.Commands;
using Attendance_Management_System.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Attendance_Management_System.ViewModels
{
    public class CompanyAdminViewModel : INotifyPropertyChanged
    {

        public CompanyAdminViewModel()
        {

        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}