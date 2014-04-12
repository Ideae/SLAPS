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
    /// Interaction logic for RegisterUserWindow.xaml
    /// </summary>
    public partial class RegisterUserWindow : Window
    {
        public RegisterUserWindow()
        {
            InitializeComponent();
            this.CenterWindow();

            cmbUsertype.Items.Add(usertype.Student);
            cmbUsertype.Items.Add(usertype.Instructor);
            cmbUsertype.Items.Add(usertype.Admin);
            cmbUsertype.SelectedIndex = 0;

        }

        private void Window_Closed(object sender, EventArgs e)
        {
            MainWindow.mainWindow.IsEnabled = true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            bool notvalid = string.IsNullOrWhiteSpace(txtFirstname.Text)
                || string.IsNullOrWhiteSpace(txtLastname.Text)
                || string.IsNullOrWhiteSpace(txtUsername.Text)
                || string.IsNullOrWhiteSpace(txtPassword.Text);
            if (notvalid)
            {
                MessageBox.Show("Please enter valid input.");
                return;
            }
            User user = User.getNew();
            user.FirstName = txtFirstname.Text;
            user.LastName = txtLastname.Text;
            user.Username = txtUsername.Text;
            user.Password = txtPassword.Text;
            user.UserType = (usertype)cmbUsertype.SelectedItem;
            MessageBox.Show("User succesfully created.");
            Close();
        }
    }
}
