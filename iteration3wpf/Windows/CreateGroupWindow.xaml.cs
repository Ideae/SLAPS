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

namespace iteration3wpf.Windows
{
    /// <summary>
    /// Interaction logic for CreateGroupWindow.xaml
    /// </summary>
    public partial class CreateGroupWindow : Window
    {
        Project p;
        public CreateGroupWindow(Project p)
        {
            InitializeComponent();
            this.p = p;
            foreach (var s in p.PrjCourse.Students)
            {
                bool notAvailable = false;
                foreach (var g in s.Groups)
                {
                    if (g.GrpProject == p) notAvailable = true;
                }
                if (notAvailable) continue;
                listUnadded.Items.Add(s);
            }
            
            this.CenterWindow();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (listUnadded.SelectedIndex >= 0)
            {
                User s = (User)listUnadded.SelectedItem;
                listAdded.Items.Add(s);
                listUnadded.Items.Remove(s);
            }
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            if (listAdded.SelectedIndex >= 0)
            {
                User s = (User)listAdded.SelectedItem;
                listUnadded.Items.Add(s);
                listAdded.Items.Remove(s);
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            MainWindow.mainWindow.IsEnabled = true;
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(txtGroupName.Text))
            {
                MessageBox.Show("Please name group");
                return;
            }

            Group g = Group.getNew();
            g.GrpProject = p;
            g.Name = txtGroupName.Text;
            p.Groups.Add(g);
            foreach( User u in listAdded.Items)
            {
                g.Members.Add(u);
                u.Groups.Add(g);
            }
            Close();
        }

    }
}
