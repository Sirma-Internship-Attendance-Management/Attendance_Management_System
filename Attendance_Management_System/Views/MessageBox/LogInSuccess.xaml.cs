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
using Attendance_Management_System.DataAccess;
using Attendance_Management_System.ViewModels;
using Attendance_Management_System.Views.MessageBox;
using Attendance_Management_System.Models;

namespace Attendance_Management_System.Views.MessageBox
{
    /// <summary>
    /// Interaction logic for LogInSuccess.xaml
    /// </summary>
    public partial class LogInSuccess : Window
    {
        public User LoggedUser { get; set; }
        public LogInSuccess(User Loggeduser)
        {
            InitializeComponent();
            LoggedUser = Loggeduser;
        }


        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            CompanyAdminView companyAdminView = new CompanyAdminView(LoggedUser);
            companyAdminView.Show();

            Application.Current.MainWindow?.Close();
            this.Close();
        }
        
    }
}
