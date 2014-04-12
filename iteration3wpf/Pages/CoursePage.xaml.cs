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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace iteration3wpf
{
    /// <summary>
    /// Interaction logic for CoursePage.xaml
    /// </summary>
    public partial class CoursePage : Page
    {
        Course course;
        public CoursePage(Course c)
        {
            course = c;
            InitializeComponent();
            lblCourseName.Content = c.Title;
            lblCourseCode.Content = c.CourseCode;
            lblInstructor.Content = c.Instructor;
            txtSummary.Text = c.Description;
            foreach (var a in c.Announcements)
            {
                Utilities.AddTextBlock(stkAnnouncements, a.Title, a.Content);
            }
            
        }

        //private void mouseUp(object sender, MouseButtonEventArgs e)
        //{
        //    Message m = Message.getNew();
        //    m.Sender = MainWindow.mainWindow.activeUser;
        //    m.Recievers.Add(course);
        //    m.Title = "Welcome to " + course.CourseCode;
        //    m.Content = "We are very happy to have you enrolled, we hope you learn a lot";
        //
        //    course.Announcements.Add(m);
        //}
    }
}
