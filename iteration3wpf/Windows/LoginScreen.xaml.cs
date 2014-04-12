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

namespace iteration3wpf
{
    /// <summary>
    /// Interaction logic for LoginScreen.xaml
    /// </summary>
    public partial class LoginScreen : Window
    {
        public LoginScreen()
        {
            InitializeComponent();
            this.CenterWindow();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            //temporary, remove later
            //if (txtUsername.Text.Equals(""))
            //{
            //    TryLogin(User.login("system", "NeverLogInAsThisGuy"));
            //    return;
            //}
            TryLogin(User.login(txtUsername.Text.ToLowerInvariant(), txtPassword.Password));
        }

        private void TryLogin(User usr)
        {
            if (usr == null)
            {
                MessageBox.Show("Login Error!");
            }
            else
            {
                MainWindow.activeUser = usr;
                MainWindow.mainWindow = new MainWindow();
                MainWindow.mainWindow.Show();
                //MainWindow.mainWindow.InitUser(usr);
                Close();
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnLogin_Click(null, null);
            }
        }

        private void grid1_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.Key == Key.F9)
            //{
            //    //TryLogin(User.GetById<User>(0));
            //    TryLogin(User.login("system", "NeverLogInAsThisGuy"));
            //}
        }

        private void btnForgotPassword_Click(object sender, RoutedEventArgs e)
        {
            //if (txtUsername.Text.Equals(""))
            //{
            //    TryLogin(User.login("vmisic", "406Prof"));
            //    return;
            //}

            User u = User.login(txtUsername.Text, txtPassword.Password, false);
            if (u == null)
            {
                MessageBox.Show("User did not exist.");
                return;
            }
            PasswordRequest prequest = PasswordRequest.getNew();
            prequest.UserRequesting = u;
            prequest.AlreadyHandled = false;
            MessageBox.Show("Password request has been sent to admin.");
            txtUsername.Text = "";
            txtPassword.Clear();

        }

        private void txtUsername_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //if (txtUsername.Text.Equals(""))
            //{
            //    TryLogin(User.login("dcamaren", "Saharris1"));
            //    return;
            //}
        }

    }
}
