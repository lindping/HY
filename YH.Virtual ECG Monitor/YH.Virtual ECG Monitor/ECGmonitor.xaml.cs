using HYS.Library;
using System.Windows;
using System.Windows.Threading;
using YH.ECGMonitor.WaveData.RESPWaveData;
using YH.Simulator.Framework;

namespace YH.Virtual_ECG_Monitor
{
    /// <summary>
    /// ECGmonitor.xaml 的交互逻辑
    /// </summary>
    public partial class ECGmonitor : Window
    {
         Launch launch1;
        double addX = 0;
        int addY = 1;
        double x = 0;
        double y = 0;

        int i = 0;
        int rate = 0;
        RESPWaveData data;

        int rhythm;  //心率类型
        int wave;    //波形

        int dataLength;

        ECGSettingModel setting;
        public ECGmonitor(int rhythm)
        {
            InitializeComponent();

            this.rhythm = rhythm;

            setting = Setting.GetSetting();
            this.wave = setting.Lead;

            data =  DataToObject.To<RESPWaveData>(rhythm) ;
            dataLength = data.WaveData.GetLength(0);
            addY = data.WaveData.Length > 1000 ? 10 : 1;
            addX = (double)myCanvas.Width / (double)data.Rate / ((double)dataLength / (double)addY);
            
            int interval = dataLength > 1000 ? 4 : (int)(addX/setting.Speed);
            launch1 = new Launch(interval);
            launch1.OnElapsed += Launch1_OnElapsed;
            launch1.Start();
            this.Title = data.Name;
        }

        private void Launch1_OnElapsed()
        {
            this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (System.Threading.ThreadStart)delegate ()
            {             
              
                y = setting.Gain * (15 - 10 * data.WaveData[i, wave]);
                polyline1.Points.Add(new Point(x, y));
                x += addX;
                i += addY;

                if (i >= dataLength)
                {
                    i = 0;
                    rate++;                 
                }
                if (rate >= data.Rate)
                {
                    rate = 0;
                    i = 0;
                    x = 0;
                    polyline1.Points.Clear();
                }
              
            });
        }
    }


}

