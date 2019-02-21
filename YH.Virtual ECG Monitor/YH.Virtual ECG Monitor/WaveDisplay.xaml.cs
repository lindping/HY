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

            // 设置全屏
            this.WindowState = System.Windows.WindowState.Normal;
            this.WindowStyle = System.Windows.WindowStyle.None;
            this.ResizeMode = System.Windows.ResizeMode.NoResize;
          //  this.Topmost = true;

            this.Left = 0.0;
            this.Top = 0.0;
            this.Width = System.Windows.SystemParameters.PrimaryScreenWidth;
            this.Height = System.Windows.SystemParameters.PrimaryScreenHeight;


            VirtualManAttributeData setting = Setting.Get<VirtualManAttributeData>();
      
            uc_wave.ABP_Paras = new ABP_Paras() { Systolic = setting.Custom.IBP[0].Value, Diastolic = setting.Custom.IBP[1].Value,  Plot = 200 };
          
            uc_wave.Resp_Paras = new Resp_Paras() { RespType = RespType.Resp_02, Capacity = 3000, Plot = 200, RespRate =  setting.Custom.RESP.Value , RespRatio = 80 };
            uc_wave.PLETH_Paras = new PLETH_Paras() { Plot = 200, Spo2 = setting.Custom.SPO2.Value };
            uc_wave.ECG_Paras = new ECG_Paras() { HeartRat = setting.Custom.HeartRate.Value, Rhythm = Rhythm.Rhythm_01 };


            uc_wave.RunWave();          
           uc_wave.Run_RESP();
        }
    }
}
