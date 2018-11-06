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

namespace YH.Language
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            SelectorWindow selectorWindow = new YH.Language.SelectorWindow();
            selectorWindow.LanguageCode = label.Content.ToString();
            if (selectorWindow.ShowDialog() == true)
            {
                label.Content = selectorWindow.LanguageCode;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            label.Content = "en-US";
        }
    }
}
