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
    /// Interaction logic for CreateProjectWindow.xaml
    /// </summary>
    public partial class CreateProjectWindow : Window
    {
        public CreateProjectWindow()
        {
            InitializeComponent();
            this.CenterWindow();
            foreach(Course c in MainWindow.mainWindow.cmbCourse.Items)
            {
                cmbCourse.Items.Add(c);
            }
            if (MainWindow.mainWindow.cmbCourse.SelectedIndex >= 0)
            {
                cmbCourse.SelectedIndex = MainWindow.mainWindow.cmbCourse.SelectedIndex;
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            MainWindow.mainWindow.IsEnabled = true;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            bool thisIsntWorking = cmbCourse.SelectedIndex == -1 || string.IsNullOrWhiteSpace(txtMaxMarks.Text)
                || string.IsNullOrWhiteSpace(txtProjectName.Text) || string.IsNullOrWhiteSpace(txtSummary.Text)
                || dateDueDate.SelectedDate == null;
            if (thisIsntWorking)
            {
                MessageBox.Show("Please fill in all requirements.");
                return;
            }
            int i = 0;
            if (!int.TryParse(txtMaxMarks.Text, out i))
            {
                MessageBox.Show("MaxMarks Incorrect");
                return;
            }
            Course c = (Course)cmbCourse.SelectedItem;
            Project p = Project.getNew();
            p.Summary = txtSummary.Text;
            p.Title = txtProjectName.Text;
            p.PrjCourse = c;
            p.MaxMarks = i;
            p.DueDate = dateDueDate.SelectedDate ?? DateTime.Now;
            c.Projects.Add(p);
            Close();
            MainWindow.mainWindow.UpdateTreeView();
        }
    }
}
