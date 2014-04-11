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
    /// Interaction logic for EditProjectWindow.xaml
    /// </summary>
    public partial class EditProjectWindow : Window
    {
        Project p;
        public EditProjectWindow(Project p)
        {
            this.p = p;
            txtMaxMarks.Text = p.MaxMarks.ToString();
            txtProjectName.Text = p.Title;
            txtSummary.Text = p.Summary;
            dateDueDate.SelectedDate = p.DueDate;

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

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            float f = 0f;
            if (!float.TryParse(txtMaxMarks.Text, out f))
            {
                MessageBox.Show("MaxMarks Incorrect");
                return;
            }
            p.MaxMarks = f;
            p.Title = txtProjectName.Text;
            txtSummary.Text = p.Summary;
            dateDueDate.SelectedDate = p.DueDate;
            Close();
        }
    }
}
