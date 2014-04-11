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

namespace iteration3wpf.Windows
{
    /// <summary>
    /// Interaction logic for SendSLAPWindow.xaml
    /// </summary>
    public partial class SendSLAPWindow : Window
    {
        public SendSLAPWindow()
        {
            InitializeComponent();
            Utilities.CenterWindow(this);
            if (MainWindow.mainWindow.activeUser is Administrator)
            {
                lblTitle.Content = "Send SLAP (Announcement)";
                lblAddRecipients.Visibility = System.Windows.Visibility.Hidden;
                btnAddRecipients.Visibility = System.Windows.Visibility.Hidden;
                cmbCourses.Visibility = System.Windows.Visibility.Hidden;
            }
            else if (MainWindow.mainWindow.activeUser is Instructor)
            {
                lblTitle.Content = "Send SLAP (Message)";
                lblAddRecipients.Visibility = System.Windows.Visibility.Hidden;
                btnAddRecipients.Visibility = System.Windows.Visibility.Hidden;
                //cmbCourses.Visibility = System.Windows.Visibility.Visible;

                cmbCourses.Items.Add("All Courses");
                
            }
            else
            {
                lblTitle.Content = "Send SLAP (Message)";
                //lblAddRecipients.Visibility = System.Windows.Visibility.Visible;
                //btnAddRecipients.Visibility = System.Windows.Visibility.Visible;
                cmbCourses.Visibility = System.Windows.Visibility.Hidden;
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnAddRecipients_Click(object sender, RoutedEventArgs e)
        {
            //implement
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            MainWindow.mainWindow.IsEnabled = true;
        }
    }
}
