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
using System.Windows.Threading;
using YH.Network.Framework;
using YH.Simulator.Framework.Resolve;

namespace YH.Virtual_ECG_Monitor
{
    /// <summary>
    /// WaveDisplay.xaml 的交互逻辑
    /// </summary>
    public partial class WaveDisplay : Window
    {

        public WaveDisplay()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //应用语言资源
            this.ApplyLanguage();           

            // 设置全屏
            this.WindowState = System.Windows.WindowState.Normal;
            this.WindowStyle = System.Windows.WindowStyle.None;
            this.ResizeMode = System.Windows.ResizeMode.NoResize;
            //  this.Topmost = true;

            this.Left = 0.0;
            this.Top = 0.0;
            this.Width = System.Windows.SystemParameters.PrimaryScreenWidth;
            this.Height = System.Windows.SystemParameters.PrimaryScreenHeight;

              uc_wave.FirstRunWave();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //窗口换肤
        private void btnColor1_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            string uri;
            if (btn == btTheme1)
            {
                btTheme1.Visibility = Visibility.Collapsed;
                btTheme2.Visibility = Visibility.Visible;
                uri = "Dictionary/Skin.RoundedCornerStyle.xaml";
            }
            else
            {
                btTheme1.Visibility = Visibility.Visible;
                btTheme2.Visibility = Visibility.Collapsed;
                uri = "Dictionary/Skin.RegularStyle.xaml";
            }
            Application.Current.Resources.MergedDictionaries[1] = new ResourceDictionary()
            {
                Source = new Uri(uri, UriKind.Relative)
            };
        }

        /// <summary>
        /// 切换语言
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLanguage_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button.Content.ToString() == "en-US")
            {
                button.Content = "中文";
                ((Window)this).ApplyLanguage(AppLanguage.English);
            }
            else
            {
                button.Content = "en-US";
                ((Window)this).ApplyLanguage(AppLanguage.Chinese);
            }
        
        }
    }
}
