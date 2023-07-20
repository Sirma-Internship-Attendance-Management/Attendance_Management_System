using Attendance_Management_System.DataAccess;
using Attendance_Management_System.ViewModels;
using Attendance_Management_System.Views.MessageBox;
using Attendance_Management_System.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Drawing;
using System.Drawing.Imaging;

namespace Attendance_Management_System.Views
{
    /// <summary>
    /// Interaction logic for CompanyAdminView.xaml
    /// </summary>
    public partial class CompanyAdminView : Window
    {
        private readonly CompanyAdminViewModel _viewModel;
        
        public User LoggedUser { get; set; }

        public CompanyAdminView(User Loggeduser)
        {
            InitializeComponent();
            LoggedUser = Loggeduser;
            _viewModel = new CompanyAdminViewModel(LoggedUser);
            DataContext = _viewModel;
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
            Application.Current.Shutdown();
        }

        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.OpenCommand.Execute(null);
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            LogoutNotification lgn = new LogoutNotification();
            lgn.ShowDialog();
            this.Close();
        }

        /*private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
        }
        private void btnAccount_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnInfo_Click(object sender, RoutedEventArgs e)
        {

        }*/

        bool StateClosed = true;
        private void ButtonMenu_Click(object sender, RoutedEventArgs e)
        {
            if (StateClosed)
            {
                Storyboard sb = this.FindResource("OpenMenu") as Storyboard;
                sb.Begin();
            }
            else
            {
                Storyboard sb = this.FindResource("CloseMenu") as Storyboard;
                sb.Begin();
            }

            StateClosed = !StateClosed;
        }

        private void btnChangeLogo_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                System.Drawing.Image image = System.Drawing.Image.FromFile(openFileDialog.FileName);
                using (MemoryStream ms = new MemoryStream())
                {
                    System.Drawing.Image img = System.Drawing.Image.FromFile(openFileDialog.FileName);
                    byte[] logo = ConverImageToByte(img);
                    UpdateCompanyLogotoDB(logo, LoggedUser.CompanyId);
                    MyDbContext dbContext = new MyDbContext();
                    Company company = (from c in dbContext.Companies
                                      where c.CompanyId.Equals(_viewModel.LoggedCompany.CompanyId)
                                      select c).FirstOrDefault();
                    imgLogo.Source = ConvertToBitMapImage(byteArrToImg(company.Logo));
                }
            }
        }

        static public BitmapImage ConvertToBitMapImage(System.Drawing.Image img)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                img.Save(memory, ImageFormat.Png);
                memory.Position = 0;

                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();

                return bitmapImage;
            }
        }

        static public byte[] ConverImageToByte(System.Drawing.Image image)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return ms.ToArray();
            }
        }

        static public System.Drawing.Image byteArrToImg(byte[] data)
        {
            using (MemoryStream ms = new MemoryStream(data))
            {
                return System.Drawing.Image.FromStream(ms);
            }

        }

        private void UpdateCompanyLogotoDB(byte[] logo, int company_id)
        {
            MyDbContext contextDB = new MyDbContext();
            var updateDB = contextDB.Companies.Where(company => company.CompanyId.Equals(company_id)).First();
            updateDB.Logo = logo;
            contextDB.SaveChanges();
            MessageBoxSuccess mb = new MessageBoxSuccess();
            mb.ShowDialog();
        }

        private void btnRedact_Click(object sender, RoutedEventArgs e)
        {
            tbCompanyName.Visibility = Visibility.Visible;
            tbIndustryType.Visibility = Visibility.Visible;
            tbWebsite.Visibility = Visibility.Visible;
            tbAddress.Visibility = Visibility.Visible;
            lblCompanyName.Visibility = Visibility.Collapsed;
            lblIndustryType.Visibility = Visibility.Collapsed;
            lblWebsite.Visibility = Visibility.Collapsed;
            lblAddress.Visibility = Visibility.Collapsed;
            btnlogoChange.Visibility = Visibility.Visible;
            btnCommit.Visibility = Visibility.Visible;
            btnCancelReadact.Visibility = Visibility.Visible;
            btnRedact.Visibility = Visibility.Collapsed;
        }       

        private void btnCommit_Click(object sender, RoutedEventArgs e)
        {
            tbCompanyName.Visibility = Visibility.Collapsed;
            tbIndustryType.Visibility = Visibility.Collapsed;
            tbWebsite.Visibility = Visibility.Collapsed;
            tbAddress.Visibility = Visibility.Collapsed;
            lblCompanyName.Visibility = Visibility.Visible;
            lblIndustryType.Visibility = Visibility.Visible;
            lblWebsite.Visibility = Visibility.Visible;
            lblAddress.Visibility = Visibility.Visible;
            btnlogoChange.Visibility = Visibility.Collapsed;
            btnCommit.Visibility = Visibility.Collapsed;
            btnCancelReadact.Visibility = Visibility.Collapsed;
            btnRedact.Visibility = Visibility.Visible;
            UpdateDataBase();
            MessageBoxSuccess mb = new MessageBoxSuccess();
            mb.ShowDialog();            
            //UnderConstruction uc = new UnderConstruction();
            //uc.ShowDialog();
        }

        private void UpdateDataBase()
        {
            MyDbContext dbContext = new MyDbContext();
            var updateDB = dbContext.Companies.Where(company => company.CompanyId.Equals(_viewModel.LoggedCompany.CompanyId)).First();
            updateDB.CompanyName = _viewModel.LoggedCompany.CompanyName;
            updateDB.Type = _viewModel.LoggedCompany.Type;
            updateDB.Website = _viewModel.LoggedCompany.Website;
            updateDB.Address = _viewModel.LoggedCompany.Address;
            updateDB.Logo = _viewModel.LoggedCompany.Logo;
            dbContext.SaveChanges();    
            
        }
        private void btnCancelRedact_Click(object sender, RoutedEventArgs e)
        {
            tbCompanyName.Visibility = Visibility.Collapsed;
            tbIndustryType.Visibility = Visibility.Collapsed;
            tbWebsite.Visibility = Visibility.Collapsed;
            tbAddress.Visibility = Visibility.Collapsed;
            lblCompanyName.Visibility = Visibility.Visible;
            lblIndustryType.Visibility = Visibility.Visible;
            lblWebsite.Visibility = Visibility.Visible;
            lblAddress.Visibility = Visibility.Visible;
            btnlogoChange.Visibility = Visibility.Collapsed;
            btnCommit.Visibility = Visibility.Collapsed;
            btnCancelReadact.Visibility = Visibility.Collapsed;
            btnRedact.Visibility = Visibility.Visible;
            _viewModel.LoggedCompany = _viewModel.getLoggedInUserCompany(1);
            _viewModel.CompanyInformationBinding();
        }

    }
}
