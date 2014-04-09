using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace iteration3wpf
{
    public enum AllPages
    {
        Home,
        ViewMessages,
    }
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow mainWindow;
        public User activeUser;

        public Dictionary<AllPages, Page> pageDict;

        public MainWindow()
        {
            SQLiteDB.main = new SQLiteDB("iteration3.sql");

            mainWindow = this;
            InitializeComponent();
            Utilities.CenterWindow(this);

            LoginScreen loginScreen = new LoginScreen();
            loginScreen.Show();
            Utilities.CenterWindow(loginScreen);
            this.Hide();
            loginScreen.btnLogin.Click += (s, e) =>
            {
                if(loginScreen.result != null){
                InitUser(loginScreen.result);
                this.Show();
                loginScreen.Close();}
            };

            //menuTopRight.ContextMenu = new ContextMenu();
            MenuItem menuItemOptions = new MenuItem(); menuItemOptions.Header = "Options";
            MenuItem menuItemChangePassword = new MenuItem(); menuItemChangePassword.Header = "Change Password";
            MenuItem menuItemLogout = new MenuItem(); menuItemLogout.Header = "Logout";

            menuTopRight.Items.Add(menuItemOptions);
            menuItemOptions.Items.Add(menuItemChangePassword);
            menuItemOptions.Items.Add(menuItemLogout);

            menuItemChangePassword.Click += menuItemChangePassword_Click;

            pageDict = new Dictionary<AllPages, Page>()
            {
                {AllPages.Home, new HomePage()},
                {AllPages.ViewMessages, new ViewMessagesPage()},
            };
            //NavigationService nav = NavigationService.GetNavigationService(homePage);
            frameMainframe.Navigate(pageDict[AllPages.Home]);
            //nav.Navigate(homePage);
            
        }

        void InitUser(User u)
        {
            activeUser = u;

            lblName.Content = activeUser.firstName + " " + activeUser.lastName;
            lblUsertype.Content = activeUser.TypeName();

            cmbCourse.IsEditable = true;
            cmbCourse.Text = "Select Course";
            cmbCourse.IsReadOnly = true;
            cmbCourse.Items.Add("Smalltalk 5");
        }

        void menuItemChangePassword_Click(object sender, RoutedEventArgs e)
        {
            ChangePassWindow changePassWindow = new ChangePassWindow();
            this.IsEnabled = false;
            changePassWindow.Show();
            Utilities.CenterWindow(changePassWindow);
            changePassWindow.Closed += (s, ee) =>
            {
                this.IsEnabled = true;
            };
        }

        private void btnTest_Click(object sender, RoutedEventArgs e)
        {
            char letter = 'a';
            TreeViewItem tempItem = null;
            for(int i = 0; i < 26; i++)
            {
                char let = (char)(letter + i);
                object letobj = let;
                if (tempItem != null)
                {
                    TreeViewItem item = CreateTreeViewItem(letobj);
                    tempItem.Items.Add(item);
                    tempItem = item;
                }
                else
                {
                    TreeViewItem root = CreateTreeViewItem(letobj);
                    treeViewMain.Items.Add(root);
                    tempItem = root;
                }
                
                
                //TreeViewItem t = new TreeViewItem();
                //t.tag
            }
            //trvTest.Items.Add()
        }

        private TreeViewItem CreateTreeViewItem(object obj)
        {
            TreeViewItem item = new TreeViewItem();
            item.Header = obj.ToString();
            item.Tag = obj;
            return item;
        }

        private void cmbCourse_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnPage1_Click(object sender, RoutedEventArgs e)
        {
            frameMainframe.Navigate(pageDict[AllPages.Home]);
        }

        private void btnPage2_Click(object sender, RoutedEventArgs e)
        {
            frameMainframe.Navigate(pageDict[AllPages.ViewMessages]);
        }
    }
}
