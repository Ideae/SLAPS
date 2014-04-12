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
    /// Interaction logic for CreateCourseWindow.xaml
    /// </summary>
    public partial class CreateCourseWindow : Window
    {
        public CreateCourseWindow()
        {
            InitializeComponent();
            this.CenterWindow();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            MainWindow.mainWindow.IsEnabled = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            bool notvalid = string.IsNullOrWhiteSpace(txtCourseCode.Text)
                || string.IsNullOrWhiteSpace(txtCourseDescription.Text)
                || string.IsNullOrWhiteSpace(txtCourseTitle.Text)
                || dateEnd.SelectedDate == null
                || dateStart.SelectedDate == null
                || dateEnrollmentDeadline.SelectedDate == null;
            if (notvalid)
            {
                MessageBox.Show("Please enter valid input.");
                return;
            }
            Course course = Course.getNew();
            course.CourseCode = txtCourseCode.Text;
            course.Description = txtCourseDescription.Text;
            course.Title = txtCourseTitle.Text;
            course.DateStart = (DateTime)dateStart.SelectedDate;
            course.DateEnd = (DateTime)dateEnd.SelectedDate;
            course.DateEnrollment = (DateTime)dateEnrollmentDeadline.SelectedDate;
            MessageBox.Show("User succesfully created.");
            Close();
        }
    }
}
