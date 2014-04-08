﻿using System;
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

namespace iteration3wpf
{
    /// <summary>
    /// Interaction logic for ChangePassWindow.xaml
    /// </summary>
    public partial class ChangePassWindow : Window
    {
        public ChangePassWindow()
        {
            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (!txtOldPass.Text.Equals(MainWindow.mainWindow.activeUser.password))
            {
                //PopupWindow popup = new PopupWindow();
                MessageBox.Show(this, "Old password is incorrect.");
            }
            else if (!txtNewPass1.Text.Equals(txtNewPass2.Text))
            {
                MessageBox.Show(this, "The new password fields did not match.");
            }
            else
            {
                MainWindow.mainWindow.activeUser.password = txtNewPass1.Text;
                MessageBox.Show(this, "Your password is born again.");
                Close();
            }
        }
    }
}