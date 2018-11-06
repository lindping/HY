using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using YH.MetroTile;

namespace YH.Simulation_Home
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
            //隐藏右上角还原按钮
            this.btnNormal.Visibility = Visibility.Collapsed;
        }

        //全屏显示
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Left = 0;
            this.Top = 0;
            this.Height = SystemParameters.WorkArea.Height;//获取屏幕的宽高  使之不遮挡任务栏
            this.Width = SystemParameters.WorkArea.Width;
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
        //最小化
        private void btnMin_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }


        //窗口换肤-浅色
        private void btnColor1_Click(object sender, RoutedEventArgs e)
        {
            GlobalParams.ColorCode = 1;
            Application.Current.Resources.MergedDictionaries[1] = new ResourceDictionary()
            {
                Source = new Uri("/YH.Theme;component/Dictionary/Skin.RegularStyle.xaml", UriKind.Relative)
            };
        }

        //窗口换肤-深色
        private void btnColor2_Click(object sender, RoutedEventArgs e)
        {
            GlobalParams.ColorCode = 2;
            Application.Current.Resources.MergedDictionaries[1] = new ResourceDictionary()
            {
                Source = new Uri("/YH.Theme;component/Dictionary/Skin.RoundedCornerStyle.xaml", UriKind.Relative)
            };
        }

        Rect rcnormal;//定义一个全局rect记录还原状态下窗口的位置和大小。
        /// <summary>
        /// 最大化
        /// </summary>
        private void btnMaximize_Click(object sender, RoutedEventArgs e)
        {
            this.btnMaximize.Visibility = Visibility.Collapsed;
            this.btnNormal.Visibility = Visibility.Visible;
            rcnormal = new Rect(this.Left, this.Top, this.Width, this.Height);//保存下当前位置与大小
            this.Left = 0;//设置位置
            this.Top = 0;
            Rect rc = SystemParameters.WorkArea;//获取工作区大小
            this.Width = rc.Width;
            this.Height = rc.Height;
        }
        /// <summary>
        /// 还原
        /// </summary>
        private void btnNormal_Click(object sender, RoutedEventArgs e)
        {
            this.Left = rcnormal.Left;
            this.Top = rcnormal.Top;
            this.Width = rcnormal.Width;
            this.Height = rcnormal.Height;
            this.btnMaximize.Visibility = Visibility.Visible;
            this.btnNormal.Visibility = Visibility.Collapsed;
        }
        //双击标题
        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if (this.ActualWidth == SystemParameters.WorkArea.Width)
                {
                    btnNormal_Click(null, null);
                }
                else
                {
                    btnMaximize_Click(null, null);
                }
            }
        }
    }
}
