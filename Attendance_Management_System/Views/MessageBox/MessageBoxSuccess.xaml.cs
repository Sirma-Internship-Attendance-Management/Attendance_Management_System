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
using System.Windows.Threading;

namespace Attendance_Management_System.Views.MessageBox
{
    /// <summary>
    /// Interaction logic for MessageBoxSuccess.xaml
    /// </summary>
    public partial class MessageBoxSuccess : Window
    {
        

        public MessageBoxSuccess()
        {
            InitializeComponent();
            
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
