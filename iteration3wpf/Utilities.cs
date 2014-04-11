using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace iteration3wpf
{
    public static class Utilities
    {
        public static string LastWord(this string s, char delim)
        {
            return s.Substring(s.LastIndexOf(delim) + 1);
        }

        public static string TypeName(this object o)
        {
            return o.GetType().ToString().LastWord('.');
        }

        public static void CenterWindow(this Window window)
        {
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            double windowWidth = window.Width;
            double windowHeight = window.Height;
            window.Left = (screenWidth / 2) - (windowWidth / 2);
            window.Top = (screenHeight / 2) - (windowHeight / 2);
        }

        public static void AddTextBlock(StackPanel stk, string title, string text)
        {
            TextBlock tb1 = new TextBlock();
            tb1.Padding = new Thickness(5);
            //tb1.Text = title;
            tb1.Inlines.Add(new Bold(new Run(title)));
            stk.Children.Add(tb1);

            TextBlock tb2 = new TextBlock();
            tb2.Padding = new Thickness(15, 0, 5, 0);
            tb2.Text = text;
            stk.Children.Add(tb2);
        }
    }
}
