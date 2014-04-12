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
    /// Interaction logic for ViewGroupsWindow.xaml
    /// </summary>
    public partial class ViewGroupsWindow : Window
    {
        Project p = null;
        public ViewGroupsWindow(Project p)
        {
            this.p = p;
            InitializeComponent();
            this.CenterWindow();

            foreach(var g in p.Groups)
            {
                AddGroup(g);
            }
        }
        public ViewGroupsWindow(Group g)
        {
            InitializeComponent();
            this.CenterWindow();
            AddGroup(g);
        }
        //public ViewGroupsWindow(Group)

        private void AddGroup(Group g)
        {
            //GroupBox gbox = new GroupBox();
            //gbox.Header = name;

            Expander exp = new Expander();
            stkGroups.Children.Add(exp);
            exp.Header = g.Name;
            StackPanel sp = new StackPanel();
            exp.Content = sp;
            foreach(var n in g.Members)
            {
                Label l = new Label();
                l.Content = n.FirstName + " " + n.LastName;
                sp.Children.Add(l);
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            MainWindow.mainWindow.IsEnabled = true;
        }
    }
}
