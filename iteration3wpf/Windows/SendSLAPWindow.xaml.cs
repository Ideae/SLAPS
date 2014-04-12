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
        AddRecipientsWindow arw;
        public Group group = null;
        public SendSLAPWindow(Group group = null)
        {
            this.group = group;
            InitializeComponent();
            this.CenterWindow();
            if (MainWindow.activeUser.UserType == usertype.Admin)
            {
                lblTitle.Content = "Send SLAP (Announcement)";
                lblAddRecipients.Visibility = System.Windows.Visibility.Hidden;
                btnAddRecipients.Visibility = System.Windows.Visibility.Hidden;
            }
            else
            {
                lblTitle.Content = "Send SLAP (Message)";
            }

            if (group != null)
            {
                lblAddRecipients.Visibility = System.Windows.Visibility.Hidden;
                btnAddRecipients.Visibility = System.Windows.Visibility.Hidden;
            }

            arw = new AddRecipientsWindow(this);

        }
        

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnAddRecipients_Click(object sender, RoutedEventArgs e)
        {
            this.IsEnabled = false;
            arw.Show();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            MainWindow.mainWindow.IsEnabled = true;
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMessage.Text) || string.IsNullOrWhiteSpace(txtSubject.Text))
            {
                MessageBox.Show("Please complete the forms.");
                return;
            }
            Message m = Message.getNew();
            m.Title = txtSubject.Text;
            m.Content = txtMessage.Text;
            m.Sender = MainWindow.activeUser;

            if (group != null)
            {
                foreach (User u in group.Members)
                {
                    if (u != MainWindow.activeUser)
                    {
                        m.Recievers.Add(u);
                        u.Messages.Add(m);
                    }
                }
                MessageBox.Show("Message was sent.");
                arw.Close();
                Close();
                return;
            }

            if (MainWindow.activeUser.UserType == usertype.Admin)
            {
                m.Recievers.Add(User.GetById(0));
                User.GetById(0).Messages.Add(m);
            }
            else
            {
                foreach(Loadable q in arw.listAdded.Items)
                {
                    if (q.GetType() == typeof(User))
                    {
                        User u = (User)q;
                        m.Recievers.Add(u);
                        u.Messages.Add(m);
                    }
                    else if (q.GetType() == typeof(Course))
                    {
                        Course c = (Course)q;
                        m.Recievers.Add(c);
                        c.Announcements.Add(m);
                    }
                    else if (q.GetType() == typeof(Group))
                    {
                        Group g = (Group)q;
                        foreach(User u in g.Members)
                        {
                            if (u != MainWindow.activeUser)
                            {
                                m.Recievers.Add(u);
                                u.Messages.Add(m);
                            }
                        }
                        
                    }
                }
            }

            
            MessageBox.Show("Message was sent.");
            arw.Close();
            Close();
        }
    }
}
