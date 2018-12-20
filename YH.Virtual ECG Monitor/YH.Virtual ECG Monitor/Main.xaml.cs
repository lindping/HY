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

            string number = button.Content.ToString().Substring(0,4).Trim();
            switch(number)
            {
                case "1.1": 
                    PatientInfo patientInfo = new PatientInfo();
                    patientInfo.Show();
                    break;
                case "1.2":
                    System.Diagnostics.Process.Start("ms-settings:dateandtime");

                    break;
                case "1.3":
                    DisplaySetting displaySetting = new DisplaySetting();
                    displaySetting.Show();
                    break;
                case "1.4": 
                    VolumnSetting volumnSetting = new VolumnSetting();
                    volumnSetting.Show();
                        break;
                case "2.1": 
                    ECGSetting frm = new ECGSetting();
                    frm.Show();
                    break;
                case "2.2": break;
                case "2.3": break;
                case "2.4": break;
                case "2.5": break;
                case "3.1":
                   new SettingDialog(0).ShowDialog();                   
                    break;
                case "3.2":
                    new SettingDialog(1).ShowDialog();
                    break;
                case "3.3":
                    new SettingDialog(2).ShowDialog();
                    break;
                case "3.4":
                    new SettingDialog(3).ShowDialog();
                    break;
                case "3.5":
                    new SettingDialog(4).ShowDialog();
                    break;
                case "3.6":
                    new SettingDialog(5).ShowDialog();
                    break;
                case "3.7": break;
                case "3.8": break;
                case "3.9": break;
                case "3.10":break;
                case "4.1": break;
                case "4.2": break;
                case "5.1": break;
                case "5.2": break;

            }
            

            
        }
    }
}
