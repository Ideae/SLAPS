using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace iteration3wpf
{
    /// <summary>
    /// Interaction logic for ViewMessagesPage.xaml
    /// </summary>
    public partial class EditCoursePage : Page
    {
        public Course course;
        public List<User> allStudents;
        public EditCoursePage(Course course)
        {
            this.course = course;
            InitializeComponent();

            if (course == null) return;
            txtCourseCode.Text = course.CourseCode;
            txtCourseName.Text = course.Title;

            allStudents = new List<User>();
            DataRowCollection list = SQLiteDB.main.GetDataTable("SELECT * FROM Users").Rows;
            foreach (DataRow d in list)
            {
                long? lonlon = d.Field<long?>("Id");
                if (lonlon == null) continue;
                int i = (int)lonlon;
                User u = User.GetById(i);
                if (u != null)
                {
                    if (u.UserType == usertype.Student)
                    {
                        allStudents.Add(u);
                        if (course.Students.Contains(u))
                        {
                            listAdded.Items.Add(u);
                        }
                        else
                        {
                            listUnadded.Items.Add(u);
                        }
                    }
                    else if (u.UserType == usertype.Instructor)
                    {
                        cmbProfessor.Items.Add(u);
                    }
                }
            }

            cmbProfessor.SelectedItem = course.Instructor;

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

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCourseCode.Text) || string.IsNullOrWhiteSpace(txtCourseName.Text))
            {
                MessageBox.Show("Please enter valid input.");
                return;
            }
            course.CourseCode = txtCourseCode.Text;
            course.Title = txtCourseName.Text;
            course.Instructor = (User)cmbProfessor.SelectedItem;
            if(!((User)cmbProfessor.SelectedItem).Courses.Contains(course))((User)cmbProfessor.SelectedItem).Courses.Add(course);

            foreach(User s in course.Students.ToList())
            {
                if (!listAdded.Items.Contains(s)) 
                {
                    course.Students.Remove(s);
                    
                }
                s.Courses.Remove(course);
            }
            foreach(User s in listAdded.Items)
            {
                if (!course.Students.Contains(s))
                {
                    course.Students.Add(s);
                    
                }
                if (!s.Courses.Contains(course)) s.Courses.Add(course);
            }
            MessageBox.Show("The course has been updated sucessfully.");
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.mainWindow.frameMainframe.Navigate(new EditCoursePage(course));
        }
    }
}
