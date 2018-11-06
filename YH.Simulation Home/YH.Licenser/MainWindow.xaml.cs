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

namespace YH.Licenser
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

        //界面可以拖动
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        //关闭窗口
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void button_Add_Click(object sender, RoutedEventArgs e)
        {
            AddWindow addWindow = new Licenser.AddWindow();
            if (addWindow.ShowDialog() == true)
            {

            }
        }

        private void button_Close_Click(object sender, RoutedEventArgs e)
        {
            //this.Close();
            ViewWindow viewWindow = new Licenser.ViewWindow();
            if (viewWindow.ShowDialog() == true)
            {

            }
        }
    }
}
