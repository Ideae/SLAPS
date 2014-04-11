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
    /// Interaction logic for ViewSubmissionsWindow.xaml
    /// </summary>
    public partial class ViewSubmissionsWindow : Window
    {
        public Project p;
        public bool AlreadyClosed = false;
        public ViewSubmissionsWindow(Project p)
        {
            this.p = p;
            InitializeComponent();
            this.CenterWindow();

            int count = 0;
            if (MainWindow.activeUser.UserType == usertype.Instructor)
            {
                foreach(Group g in p.Groups)
                {
                    if (g.GrpSubmission != null)
                    {
                        AddSubmission(g.GrpSubmission);
                        count++;
                    }
                }
                if (count == 0)
                {
                    MessageBox.Show("There are no submissions!");
                    AlreadyClosed = true;
                    Close();
                }
            }
            else if (MainWindow.activeUser.UserType == usertype.Student)
            {
                foreach (Group g in p.Groups)
                {
                    if (MainWindow.activeUser.Groups.Contains(g))
                    {
                        if (g.GrpSubmission != null)
                        {
                            AddSubmission(g.GrpSubmission);
                            count++;
                            break;
                        }
                    }
                }
                if (count == 0)
                {
                    MessageBox.Show("There are no submissions!");
                    AlreadyClosed = true;
                    Close();
                }
            }


        }

        private void AddSubmission(Submission sub)
        {
            //GroupBox gbox = new GroupBox();
            //gbox.Header = name;

            string title = "";
            if (sub.SmGroup != null)
            {
                title = sub.SmGroup.Name;
            }
            else if (sub.Submitter != null)
            {
                title = sub.Submitter.Username;
            }
            else
            {
                title = "Submission";
            }

            title += " : " + sub.SubmitTime.ToShortDateString() + " " + sub.SubmitTime.ToShortTimeString();


            Expander exp = new Expander();
            stkSubmissions.Children.Add(exp);
            exp.Header = title;
            StackPanel sp = new StackPanel();
            exp.Content = sp;

            string mark = "Grade: ";
            bool graded = sub.Mark >= 0;
            if (!graded)
            {
                mark += "UNGRADED";
            }
            else
            {
                float percent = sub.Mark / sub.SmProject.MaxMarks * 100f;
                string p = string.Format("{0:00.00}", percent);
                mark += sub.Mark + " / " + sub.SmProject.MaxMarks + " (" + p + "%)";
            }
            Label l = new Label();
            l.Content = mark;
            sp.Children.Add(l);

            if (graded)
            {
                Label l2 = new Label();
                l2.Content = "Comments:";
                sp.Children.Add(l2);
                Label l3 = new Label();
                l3.Content = sub.InstructorComments;
                if (l3.Content.Equals("")) l3.Content = "None";
                sp.Children.Add(l3);

            }
            if (MainWindow.activeUser.UserType == usertype.Instructor)
            {
                Button b = new Button();
                b.Content = "Evaluate";
                if (graded) b.Content = "Reevalute";
                b.Click += (s, e) =>
                {
                    //open evalute window
                };
                sp.Children.Add(b);
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            MainWindow.mainWindow.IsEnabled = true;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
