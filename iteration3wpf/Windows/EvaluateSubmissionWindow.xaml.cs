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
    /// Interaction logic for EvaluateSubmissionWindow.xaml
    /// </summary>
    public partial class EvaluateSubmissionWindow : Window
    {
        ViewSubmissionsWindow viewSubmissionsWindow;
        Submission submission;
        public EvaluateSubmissionWindow(ViewSubmissionsWindow viewSubmissionsWindow, string infostring, Submission submission)
        {
            this.viewSubmissionsWindow = viewSubmissionsWindow;
            InitializeComponent();
            this.CenterWindow();
            this.lblInfo.Content = infostring;
            if (submission.Mark >= 0) txtGrade.Text = submission.Mark.ToString();
            txtComments.Text = submission.InstructorComments;
            lblOutof.Content = "/ " + submission.SmProject.MaxMarks.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            viewSubmissionsWindow.IsEnabled = true;
        }
    }
}
