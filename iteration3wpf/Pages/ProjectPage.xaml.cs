using iteration3wpf.Windows;
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

namespace iteration3wpf.Pages
{
    /// <summary>
    /// Interaction logic for ProjectPage.xaml
    /// </summary>
    public partial class ProjectPage : Page
    {
        public Project project;
        public ProjectPage(Project project)
        {
            this.project = project;
            InitializeComponent();

            lblProjectName.Content = project.Title;
            lblMaxMarks.Content = project.MaxMarks;
            lblDueDate.Content = project.DueDate.ToLongDateString();
            txtSummary.Text = project.Summary;
            txtSummary.IsReadOnly = true;

            if (MainWindow.activeUser.UserType == usertype.Student)
            {
                btnEditProject.Visibility = System.Windows.Visibility.Hidden;
                btnCreateGroup.Visibility = System.Windows.Visibility.Hidden;
                btnUploadInstructions.Visibility = System.Windows.Visibility.Hidden;
                //btnViewGroups.Visibility = System.Windows.Visibility.Hidden;

                Thickness t = btnSubmitProject.Margin;
                t.Top = btnEditProject.Margin.Top;
                btnSubmitProject.Margin = t;

                t = btnViewSubmissions.Margin;
                t.Top = btnCreateGroup.Margin.Top;
                btnViewSubmissions.Margin = t;
                btnViewSubmissions.Content = "View Submission";

                t = btnSLAPGroup.Margin;
                t.Top = btnUploadInstructions.Margin.Top;
                btnSLAPGroup.Margin = t;

                btnViewGroups.Content = "View Group";
            }
            else
            {
                btnSubmitProject.Visibility = System.Windows.Visibility.Hidden;
                btnSLAPGroup.Visibility = System.Windows.Visibility.Hidden;
                //btnViewSubmissions.Visibility = System.Windows.Visibility.Hidden;

            }
        }

        private void btnEditProject_Click(object sender, RoutedEventArgs e)
        {
            EditProjectWindow editProjectWindow = new EditProjectWindow(project);
            MainWindow.mainWindow.IsEnabled = false;
            editProjectWindow.ShowDialog();

        }
        private void btnCreateGroup_Click(object sender, RoutedEventArgs e)
        {
            CreateGroupWindow createGroupsWindow = new CreateGroupWindow(project);
            MainWindow.mainWindow.IsEnabled = false;
            createGroupsWindow.ShowDialog();
        }
        private void btnViewGroups_Click(object sender, RoutedEventArgs e)
        {
            if (MainWindow.activeUser.UserType == usertype.Instructor)
            {
                ViewGroupsWindow viewGroupsWindow = new ViewGroupsWindow(project);
                MainWindow.mainWindow.IsEnabled = false;
                viewGroupsWindow.ShowDialog();
            }
            else
            {
                Group group = null;
                foreach(Group g in MainWindow.activeUser.Groups)
                {
                    if (g.GrpProject == project)
                    {
                        group = g;
                    }
                }
                if (group == null)
                {
                    MessageBox.Show("You aren't in a group for this project yet.");
                    return;
                }

                ViewGroupsWindow viewGroupsWindow = new ViewGroupsWindow(group);
                MainWindow.mainWindow.IsEnabled = false;
                viewGroupsWindow.ShowDialog();
            }
        }

        private void btnViewSubmissions_Click(object sender, RoutedEventArgs e)
        {
            ViewSubmissionsWindow viewSubmissionsWindow = new ViewSubmissionsWindow(project);
            MainWindow.mainWindow.IsEnabled = false;
            if (!viewSubmissionsWindow.AlreadyClosed)
            {
                viewSubmissionsWindow.ShowDialog();
            }
            else
            {
                MainWindow.mainWindow.IsEnabled = true;
            }
        }

        private void btnSLAPGroup_Click(object sender, RoutedEventArgs e)
        {
            Group group = null;
            foreach (var g in MainWindow.activeUser.Groups)
            {
                if (g.GrpProject == project) group = g;
            }
            if (group == null)
            {
                MessageBox.Show("You aren't in a group.");
                return;
            }
            SendSLAPWindow viewSubmissionsWindow = new SendSLAPWindow(group);
            MainWindow.mainWindow.IsEnabled = false;
            viewSubmissionsWindow.ShowDialog();
        }
    }
}
