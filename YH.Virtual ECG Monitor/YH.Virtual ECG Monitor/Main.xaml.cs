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

namespace YH.Virtual_ECG_Monitor
{
    /// <summary>
    /// Main.xaml 的交互逻辑
    /// </summary>
    public partial class Main : Window
    {
        private DispatcherTimer ShowTimer;
        public Main()
        {
            InitializeComponent();

            ShowTime();
            ShowTimer = new System.Windows.Threading.DispatcherTimer();
            ShowTimer.Tick += new EventHandler(ShowCurTimer);//起个Timer一直获取当前时间
            ShowTimer.Interval = new TimeSpan(0, 0, 0, 1, 0);
            ShowTimer.Start();
        }

        public void ShowCurTimer(object sender, EventArgs e)
        {
            ShowTime();
        }
        //ShowTime方法
        private void ShowTime()
        {
            //获得年月日
            this.tbDate.Text = DateTime.Now.ToString("yyyy/MM/dd");   //yyyy/MM/dd
            //获得时分秒
            this.tbTime.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (sender.GetType() != typeof(Button))
                return;
            Button button = sender as Button;
            int tag;
            if (button.Tag != null && int.TryParse(button.Tag.ToString(), out tag))
            {
                new VirtualManSetting(tag).ShowDialog();
            }
            else
            {

                string number = button.Content.ToString().Substring(0, 4).Trim();
                switch (number)
                {
                    case "1.1":
                     new PatientInfo().ShowDialog();
                        
                        break;
                    case "1.2":
                        //      System.Diagnostics.Process.Start("ms-settings:dateandtime");

                        break;
                    case "1.3":
                        new DisplaySetting().ShowDialog();                        
                        break;
                    case "1.4":
                      new VolumnSetting().ShowDialog();                      
                        break;
                    case "2.1":
                        new ECGSetting().ShowDialog();                       
                        break;
                    case "2.2":
                        new WaveSetting(0).ShowDialog();
                        break;
                    case "2.3":
                        new WaveSetting(1).ShowDialog();
                        break;
                    case "2.4":
                        new WaveSetting(2).ShowDialog();
                        break;
                    case "2.5":
                        new WaveSetting(3).ShowDialog();
                        break;
                    case "3.7": break;
                    case "3.8": break;
                    case "3.9": break;
                    case "3.10": break;
                    case "4.1": break;
                    case "4.2": break;
                    case "5.1": break;
                    case "5.2": break;

                }
            }

            
        }
    }
}
