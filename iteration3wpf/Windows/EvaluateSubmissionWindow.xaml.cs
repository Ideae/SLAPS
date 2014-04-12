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
        LFile file;
        public EvaluateSubmissionWindow(ViewSubmissionsWindow viewSubmissionsWindow, string infostring, Submission submission)
        {
            this.submission = submission;
            this.viewSubmissionsWindow = viewSubmissionsWindow;
            InitializeComponent();
            this.CenterWindow();
            this.lblInfo.Content = infostring;
            if (submission.Mark >= 0) txtGrade.Text = submission.Mark.ToString();
            txtComments.Text = submission.InstructorComments;
            lblOutof.Content = "/ " + submission.SmProject.MaxMarks.ToString();

            if (submission.Files != null && submission.Files.Count > 0)
            {
                file = submission.Files[0];
                lblFilename.Content = file.FileName;
            }
        }
        //cancel
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            //viewSubmissionsWindow.IsEnabled = true;
            viewSubmissionsWindow.Close();
            ViewSubmissionsWindow newView = new ViewSubmissionsWindow(viewSubmissionsWindow.p);
            newView.Show();
        }
        //download
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (file == null)
            {
                MessageBox.Show("There were no files attached with the submission.");
                return;
            }
            file.Download();

        }
        //submit
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            int m = 0;
            if (!int.TryParse(txtGrade.Text, out m))
            {
                MessageBox.Show("Mark was entered incorrectly.");
                return;
            }
            submission.Mark = m;
            submission.InstructorComments = txtComments.Text;
            MessageBox.Show("Submission was evaluated successfully.");
            Close();
        }
    }
}
