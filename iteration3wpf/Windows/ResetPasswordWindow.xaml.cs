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
using System.Data;

namespace iteration3wpf.Windows
{
    /// <summary>
    /// Interaction logic for ResetPasswordWindow.xaml
    /// </summary>
    public partial class ResetPasswordWindow : Window
    {
        public ResetPasswordWindow()
        {
            InitializeComponent();
            this.CenterWindow();

            DisplayRequests();
        }

        private void DisplayRequests()
        {
            DataRowCollection list = SQLiteDB.main.GetDataTable("SELECT * FROM PasswordRequests WHERE AlreadyHandled<>'True'").Rows; //
            foreach(DataRow d in list)
            {
                long? lonlon = d.Field<long?>("Id");
                if (lonlon == null) continue;
                int i = (int)lonlon;
                PasswordRequest u = PasswordRequest.GetById(i);
                if (u.UserRequesting == null)
                {
                    u.AlreadyHandled = true;
                    continue;
                }
                if (u != null)
                {
                    listRequests.Items.Add(u);
                }
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            MainWindow.mainWindow.IsEnabled = true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnResetPassword_Click(object sender, RoutedEventArgs e)
        {
            if (listRequests.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a user");
                return;
            }
            object o = listRequests.SelectedItem;
            if (!(o is PasswordRequest)) return;
            PasswordRequest pr = (PasswordRequest)o;
            char[] ps = new char[6];
            Random rand = new Random();
            for (int i = 0; i < ps.Length; i++)
            {
                ps[i] = (char)(rand.Next(122 - 97) + 97);
            }
            pr.UserRequesting.Password = new string(ps);
            MessageBox.Show("The user " + pr.UserRequesting.Username + " has had their password reset to " + pr.UserRequesting.Password);
            listRequests.Items.Remove(o);
            listRequests.SelectedIndex = -1;
            pr.AlreadyHandled = true;
        }

        

    }
}
