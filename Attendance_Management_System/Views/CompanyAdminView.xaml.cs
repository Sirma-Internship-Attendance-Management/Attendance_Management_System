﻿using Attendance_Management_System.DataAccess;
using Attendance_Management_System.ViewModels;
using Attendance_Management_System.Views.MessageBox;
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

namespace Attendance_Management_System.Views
{
    /// <summary>
    /// Interaction logic for CompanyAdminView.xaml
    /// </summary>
    public partial class CompanyAdminView : Window
    {
        private readonly CompanyAdminViewModel _viewModel;

        public CompanyAdminView()
        {
            InitializeComponent();
            _viewModel = new CompanyAdminViewModel();
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
                
            }
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
