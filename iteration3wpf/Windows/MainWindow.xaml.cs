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
using iteration3wpf.Windows;

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

        //static MainWindow()
        //{
        //    mainWindow = new MainWindow();
        //}

        public MainWindow(User usr)
        {
            InitializeComponent();
            Utilities.CenterWindow(this);
            InitUser(usr);

            this.Closed += (s, e) => Application.Current.Shutdown();

            if (false && activeUser is Administrator)
            {
                cmbCourse.Visibility = System.Windows.Visibility.Hidden;
            }
            else
            {
                cmbCourse.IsEditable = true;
                cmbCourse.Text = "Select Course";
                cmbCourse.IsReadOnly = true;
            }

            MenuItem menuItemAccount = new MenuItem(); menuItemAccount.Header = "Account";
            MenuItem menuItemChangePassword = new MenuItem(); menuItemChangePassword.Header = "Change Password";
            MenuItem menuItemLogout = new MenuItem(); menuItemLogout.Header = "Logout";


            menuTopRight.Items.Add(menuItemAccount);
            menuItemAccount.Items.Add(menuItemChangePassword);
            menuItemAccount.Items.Add(menuItemLogout);

            menuItemChangePassword.Click += menuItemChangePassword_Click;

            pageDict = new Dictionary<AllPages, Page>()
            {
                {AllPages.Home, new HomePage()},
                {AllPages.ViewMessages, new ViewMessagesPage()},
            };
            frameMainframe.NavigationUIVisibility = NavigationUIVisibility.Hidden;

            frameMainframe.Navigate(pageDict[AllPages.Home]);

            PopulateSidebar();

        }

        void PopulateSidebar()
        {
            //TreeViewItem homeItem = CreateTreeViewItem("Home");
            Button homeItem = CreateButton("Home");
            homeItem.Click += homeItem_Click;

            //TreeViewItem sendSlaps = CreateTreeViewItem("Create new SLAPS");
            Button sendSlaps = CreateButton("Create new SLAPS");
            sendSlaps.Click += menuItemSendSLAP_Click;

            //treeViewMain.Items.Add(homeItem);
            //treeViewMain.Items.Add(sendSlaps);
            stackActions.Children.Add(homeItem);
            stackActions.Children.Add(sendSlaps);

            if (activeUser is Administrator)
            {
                //TreeViewItem registerUser = CreateTreeViewItem("Create a new User");
                Button registerUser = CreateButton("Create a new User");
                registerUser.Click += registerUser_Click;

                //TreeViewItem createCourse = CreateTreeViewItem("Create Course");
                Button createCourse = CreateButton("Create Course");
                createCourse.Click += createCourse_Click;

                //TreeViewItem resetPasswords = CreateTreeViewItem("Reset User's Password");
                Button resetPasswords = CreateButton("Reset User's Password");
                resetPasswords.Click += resetPasswords_Click;


                //treeViewMain.Items.Add(registerUser);
                //treeViewMain.Items.Add(createCourse);
                //treeViewMain.Items.Add(resetPasswords);
                stackActions.Children.Add(registerUser);
                stackActions.Children.Add(createCourse);
                stackActions.Children.Add(resetPasswords);
                return;
            }

            if (activeUser is Student || activeUser is Instructor)
            {

            }
            if (activeUser is Instructor)
            {

            }
        }

        Button CreateButton(string text)
        {
            Button b = new Button();
            b.Content = text;
            return b;
        }

        void homeItem_Click(object sender, RoutedEventArgs e)
        {
            frameMainframe.Navigate(pageDict[AllPages.Home]);
        }

        void resetPasswords_Click(object sender, RoutedEventArgs e)
        {
            ResetPasswordWindow resetPasswordWindow = new ResetPasswordWindow();
            this.IsEnabled = false;
            resetPasswordWindow.ShowDialog();
        }

        void createCourse_Click(object sender, RoutedEventArgs e)
        {
            CreateCourseWindow createCourseWindow = new CreateCourseWindow();
            this.IsEnabled = false;
            createCourseWindow.ShowDialog();
        }

        void registerUser_Click(object sender, RoutedEventArgs e)
        {
            RegisterUserWindow registerWindow = new RegisterUserWindow();
            this.IsEnabled = false;
            registerWindow.ShowDialog();
        }
        
        void menuItemSendSLAP_Click(object sender, RoutedEventArgs e)
        {
            SendSLAPWindow slapWindow = new SendSLAPWindow();
            this.IsEnabled = false;
            slapWindow.ShowDialog();
        }
        private TreeViewItem CreateTreeViewItem(object obj)
        {
            TreeViewItem item = new TreeViewItem();
            item.Header = obj.ToString();
            item.Tag = obj;
            return item;
        }
        public T GetPage<T>() where T : Page
        {
            Page p = pageDict.Values.First(pp => pp.GetType() == typeof(T));
            return p != null ? (T)p : null;
        }

        public void InitUser(User u)
        {
            activeUser = u;

            lblName.Content = activeUser.firstName + " " + activeUser.lastName;
            lblUsertype.Content = activeUser.TypeName();

            
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
            for (int i = 0; i < 1; i++)
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
                    root.MouseDoubleClick += (s, ee) => MessageBox.Show("Click");
                    treeViewMain.Items.Add(root);
                    tempItem = root;
                }

                //TreeViewItem t = new TreeViewItem();
                //t.tag
            }
            //trvTest.Items.Add()
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
