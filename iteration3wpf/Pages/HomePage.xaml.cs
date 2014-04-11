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
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();
            PopulateHomePage();
        }

        public void PopulateHomePage()
        {
            //Utilities.AddTextBlock(stkAnnouncements, "Announcement 1", "This announcement would normally be important.");
            //Utilities.AddTextBlock(stkAnnouncements, "Announcement 2", "Yeah, right.");
            //Utilities.AddTextBlock(stkAnnouncements, "3", "3");
            //Utilities.AddTextBlock(stkAnnouncements, "4", "4");
            //Utilities.AddTextBlock(stkAnnouncements, "5", "5");
            //Utilities.AddTextBlock(stkAnnouncements, "6", "6");

            foreach(var m in User.GetById(0).Messages)
            {
                Utilities.AddTextBlock(stkAnnouncements, m.Title, m.Content);
            }
            foreach (Message m in MainWindow.activeUser.Messages)
            {
                Utilities.AddTextBlock(stkSLAPS, m.Title, m.Content);
            }
        }
    }
}
