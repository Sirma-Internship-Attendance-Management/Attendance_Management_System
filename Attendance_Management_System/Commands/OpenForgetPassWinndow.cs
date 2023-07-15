using Attendance_Management_System.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Attendance_Management_System.Commands
{
    class OpenForgetPassWinndow : ICommand
    
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            ForgotPasswordView newWindow = new ForgotPasswordView();
            newWindow.Show();
        }
    }
}
