using System;
using System.Collections;
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
    /// Interaction logic for AddRecipientsWindow.xaml
    /// </summary>
    public partial class AddRecipientsWindow : Window
    {
        SendSLAPWindow sendSlapWindow;
        List<object> list;
        public AddRecipientsWindow(SendSLAPWindow sendSlapWindow)
        {
            this.sendSlapWindow = sendSlapWindow;
            InitializeComponent();
            this.CenterWindow();

            cmbType.Items.Add("Students");
            cmbType.SelectedIndex = 0;
            cmbType.Items.Add("Groups");
            cmbType.Items.Add("Instructors");
            cmbType.Items.Add("Administrators");
            if(MainWindow.activeUser.UserType == usertype.Instructor) cmbType.Items.Add("Courses");

        }


        private void cmbType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            list = new List<object>();

            int c = listUnadded.Items.Count;
            for (int i = 0; i < c; i++)
            {
                listUnadded.Items.RemoveAt(0);
            }
            string t = (sender as ComboBox).SelectedItem as string;
            switch (t)
            {
                case "Students":
                    foreach(Course course in MainWindow.activeUser.Courses)
                        foreach(User user in course.Students)
                            if (!list.Contains(user))
                                list.Add(user);
                    break;
                case "Groups":
                    foreach (Group g in MainWindow.activeUser.Groups) list.Add(g);
                    break;
                case "Instructors":
                    foreach(Course course in MainWindow.activeUser.Courses) list.Add(course.Instructor);
                    break;
                case "Courses":
                    foreach (Course course in MainWindow.activeUser.Courses) list.Add(course);
                    break;
                case "Administrators":
                    foreach (DataRow d in SQLiteDB.main.GetDataTable("SELECT * FROM Users WHERE UserType='Admin'").Rows) 
                        list.Add(User.GetById((int)d.Field<long>("Id")));
                    list.Remove(User.GetById(0));
                    break;
            }
            foreach (object d in list)
            {
                //bool bad = false;
                //foreach(DataRowWrapper dd in listAdded.Items)
                //{
                //    bool b1 = dd.dataRow[1].Equals(d[1]);
                //    bool b2 = dd.dataRow[0].Equals(d[0]);
                //    if (b1 && b2)
                //        bad = true;
                //}
                if (listAdded.Items.Contains(d)) continue;
                listUnadded.Items.Add(d);//new DataRowWrapper(d, t));
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (listUnadded.SelectedIndex >= 0)
            {
                //DataRowWrapper d = (DataRowWrapper)listUnadded.Items[listUnadded.SelectedIndex];
                Object d = listUnadded.SelectedItems[0];
                listAdded.Items.Add(d);
                listUnadded.Items.Remove(d);
            }
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            if (listAdded.SelectedIndex >= 0)
            {
                //DataRowWrapper d = (DataRowWrapper)listAdded.Items[listAdded.SelectedIndex];
                Object d = listAdded.SelectedItems[0];
                listUnadded.Items.Add(d);
                listAdded.Items.Remove(d);
            }
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            sendSlapWindow.IsEnabled = true;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            this.Hide();
            sendSlapWindow.IsEnabled = true;
        }
    }
}
