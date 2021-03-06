﻿using System;
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
using iteration3wpf.Pages;
using System.IO;
using System.Data;

namespace iteration3wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow mainWindow;
        public static readonly string MainDirectory = Environment.CurrentDirectory;
        private static readonly string _ServerDirectory = MainDirectory + "\\FakeServer";
        public static string ServerDirectory { 
            get 
            {
                if (!Directory.Exists(_ServerDirectory))
                    Directory.CreateDirectory(_ServerDirectory);
                return _ServerDirectory;
            }
        }
        public static User activeUser { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            this.CenterWindow();
            InitUser();


            if (activeUser.UserType == usertype.Admin)
            {
                cmbCourse.Visibility = System.Windows.Visibility.Hidden;
                lblAsgmnt.Visibility = System.Windows.Visibility.Hidden;
                PopulateCoursesInList();
                listProjects.SelectionChanged += listProjects_SelectionChangedAdmin;
            }
            else
            {
                listProjects.SelectionChanged += listProjects_SelectionChanged;
            }

            MenuItem menuItemAccount = new MenuItem(); menuItemAccount.Header = "Account";
            MenuItem menuItemChangePassword = new MenuItem(); menuItemChangePassword.Header = "Change Password";
            MenuItem menuItemLogout = new MenuItem(); menuItemLogout.Header = "Logout";


            menuTopRight.Items.Add(menuItemAccount);
            menuItemAccount.Items.Add(menuItemChangePassword);
            menuItemAccount.Items.Add(menuItemLogout);

            menuItemChangePassword.Click += menuItemChangePassword_Click;
            menuItemLogout.Click += menuItemLogout_Click;

            frameMainframe.NavigationUIVisibility = NavigationUIVisibility.Hidden;

            frameMainframe.Navigate(new HomePage());

            PopulateSidebar();

        }

        public void PopulateCoursesInList()
        {
            int count = listProjects.Items.Count;
            for(int i = 0; i < count; i++)
            {
                listProjects.Items.RemoveAt(0);
            }
            DataRowCollection list = SQLiteDB.main.GetDataTable("SELECT * FROM Courses").Rows;
            foreach (DataRow d in list)
            {
                long? lonlon = d.Field<long?>("Id");
                if (lonlon == null) continue;
                int i = (int)lonlon;
                Course c = Course.GetById(i);
                if (c == null)
                {
                    continue;
                }
                listProjects.Items.Add(c);
            }
        }

        private void listProjects_SelectionChangedAdmin(object sender, SelectionChangedEventArgs e)
        {
            if (listProjects.SelectedValue == null) return;
            Course course = (Course)listProjects.SelectedItem;
            frameMainframe.Navigate(new EditCoursePage(course));
        }

        void listProjects_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listProjects.SelectedValue == null) return;

            Project proj = (Project)listProjects.SelectedItem;
            frameMainframe.Navigate(new ProjectPage(proj));
        }

        void menuItemLogout_Click(object sender, RoutedEventArgs e)
        {
            activeUser = null;
            //push to database for good measure?
            LoginScreen loginScreen = new LoginScreen();
            loginScreen.Show();
            this.Close();
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

            if (activeUser.UserType == usertype.Admin)
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

            if (activeUser.UserType == usertype.Student || activeUser.UserType == usertype.Instructor)
            {

            }
            if (activeUser.UserType == usertype.Instructor)
            {
                Button createProject = CreateButton("Create Project");
                createProject.Click += createProject_Click;

                stackActions.Children.Add(createProject);
            }
        }

        void createProject_Click(object sender, RoutedEventArgs e)
        {
            CreateProjectWindow createProjectWindow = new CreateProjectWindow();
            this.IsEnabled = false;
            createProjectWindow.ShowDialog();
        }

        Button CreateButton(string text)
        {
            Button b = new Button();
            b.Content = text;
            return b;
        }

        void homeItem_Click(object sender, RoutedEventArgs e)
        {
            frameMainframe.Navigate(new HomePage());
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
        public void InitUser()
        {
            //activeUser = u;

            lblName.Content = activeUser.FirstName + " " + activeUser.LastName;
            lblUsertype.Content = activeUser.UserType;
            cmbCourse.Items.Add("-----------");
            cmbCourse.SelectedIndex = 0;
            foreach (var g in activeUser.Courses) cmbCourse.Items.Add(g);

            
            
        }

        void menuItemChangePassword_Click(object sender, RoutedEventArgs e)
        {
            ChangePassWindow changePassWindow = new ChangePassWindow();
            this.IsEnabled = false;
            changePassWindow.Show();
            changePassWindow.Closed += (s, ee) =>
            {
                this.IsEnabled = true;
            };
        }

        private void cmbCourse_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbCourse.SelectedIndex == 0) return;
            Course c = (Course)cmbCourse.SelectedItem;
            frameMainframe.Navigate(new CoursePage(c));

            UpdateTreeView();
        }

        public void UpdateTreeView()
        {
            int c = listProjects.Items.Count;
            for(int i = 0; i < c; i++)
            {
                listProjects.Items.RemoveAt(0);
            }
            if (cmbCourse.SelectedIndex != -1)
            {
                var projs = (cmbCourse.SelectedItem as Course).Projects;
                foreach (var p in projs)
                {
                    //projnode.MouseDoubleClick += (s, ee) => frameMainframe.Navigate(new ProjectPage((Project)projnode.Header));
                    listProjects.Items.Add(p);
                }
            }
        }

        private void btnPage1_Click(object sender, RoutedEventArgs e)
        {
            frameMainframe.Navigate(new HomePage());
        }

        private void btnPage2_Click(object sender, RoutedEventArgs e)
        {
            //frameMainframe.Navigate(pageDict[AllPages.ViewMessages]);
            //frameMainframe.Navigate(new ProjectPage());
        }

        private void btnFile_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            bool? result = dlg.ShowDialog();
            if (result == true)
            {
                string filename = dlg.FileName;
            }
        }
    }
}
