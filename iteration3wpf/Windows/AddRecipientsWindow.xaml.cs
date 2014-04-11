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
        DataRowCollection list = null;
        public AddRecipientsWindow(SendSLAPWindow sendSlapWindow)
        {
            this.sendSlapWindow = sendSlapWindow;
            InitializeComponent();
            this.CenterWindow();

            cmbType.Items.Add("Students");
            cmbType.Items.Add("Groups");
            cmbType.Items.Add("Instructors");
            cmbType.Items.Add("Courses");
            cmbType.Items.Add("Administrators");
            cmbType.SelectedIndex = 0;
        }


        private void cmbType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int c = listUnadded.Items.Count;
            for (int i = 0; i < c; i++)
            {
                listUnadded.Items.RemoveAt(0);
            }
            string t = (sender as ComboBox).SelectedItem as string;
            switch (t)
            {
                case "Students":
                    list = SQLiteDB.main.GetDataTable("SELECT * FROM Users WHERE UserType='Student'").Rows;
                    break;
                case "Groups":
                    list = SQLiteDB.main.GetDataTable("SELECT * FROM Groups").Rows;
                    break;
                case "Instructors":
                    list = SQLiteDB.main.GetDataTable("SELECT * FROM Users WHERE UserType='Instructor'").Rows;
                    break;
                case "Courses":
                    list = SQLiteDB.main.GetDataTable("SELECT * FROM Courses").Rows;
                    break;
                case "Administrators":
                    list = SQLiteDB.main.GetDataTable("SELECT * FROM Users WHERE UserType='Admin'").Rows;
                    break;
            }
            foreach (DataRow d in list)
            {
                bool bad = false;
                foreach(DataRowWrapper dd in listAdded.Items)
                {
                    bool b1 = dd.dataRow[1].Equals(d[1]);
                    bool b2 = dd.dataRow[0].Equals(d[0]);
                    if (b1 && b2)
                        bad = true;
                }
                if (bad) continue;
                listUnadded.Items.Add(new DataRowWrapper(d, t));
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (listUnadded.SelectedIndex >= 0)
            {
                DataRowWrapper d = (DataRowWrapper)listUnadded.Items[listUnadded.SelectedIndex];
                listAdded.Items.Add(d);
                listUnadded.Items.Remove(d);
            }
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            if (listAdded.SelectedIndex >= 0)
            {
                DataRowWrapper d = (DataRowWrapper)listAdded.Items[listAdded.SelectedIndex];
                listUnadded.Items.Add(d);
                listAdded.Items.Remove(d);
            }
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            sendSlapWindow.IsEnabled = true;
        }
    }
}
