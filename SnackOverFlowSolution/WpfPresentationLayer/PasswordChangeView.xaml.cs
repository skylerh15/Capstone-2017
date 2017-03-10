﻿using LogicLayer;
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

namespace WpfPresentationLayer
{
    /// <summary>
    /// William Flood
    /// Created 2017/03/09
    /// 
    /// Interaction logic for PasswordChangeView.xaml
    /// </summary>
    public partial class PasswordChangeView : Window
    {
        private string userName;

        /// <summary>
        /// William Flood
        /// Created 2017/03/09
        /// 
        /// </summary>
        /// <param name="userName"></param>
        public PasswordChangeView(String userName)
        {
            this.userName = userName;
            InitializeComponent();
        }

        /// <summary>
        /// William Flood
        /// Created 2017/03/09
        /// 
        /// Sets the search critera based on user input, and closes the window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPost_Click(object sender, RoutedEventArgs e)
        {
            if(txtNewPassword.Password.Length < 8)
            {
                MessageBox.Show("New password is too short");
            }
            else if(txtConfirmPassword.Password.Equals(txtNewPassword.Password))
            {
                try
                {
                    var updateResults = (new UserManager()).ChangePassword(userName, txtOldPassword.Password, txtNewPassword.Password, txtConfirmPassword.Password);
                    if(1==updateResults)
                    {
                        MessageBox.Show("Password changed");
                    } else
                    {
                        MessageBox.Show("Update failed");
                    }
                } catch
                {
                    ErrorAlert.ShowDatabaseError();
                }
            }
            else
            {
                MessageBox.Show("Passwords don't match");
            }
        }
    }
}
