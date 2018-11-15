using HYS.Library;
using Microsoft.Research.DynamicDataDisplay;
using Microsoft.Research.DynamicDataDisplay.DataSources;
using System;
using System.Collections.Generic;
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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
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
        public ECGmonitor(int rhythm, int wave)
        {
            InitializeComponent();

            this.rhythm = rhythm;
            this.wave = wave;

            data =  DataToObject.To<RESPWaveData>(rhythm) ;
            dataLength = data.WaveData.GetLength(0);
            addY = data.WaveData.Length > 1000 ? 10 : 1;
            addX = (double)myCanvas.Width / (double)data.Rate / ((double)dataLength / (double)addY);

            int interval = dataLength > 1000 ? 4 : 20;
            launch1 = new Launch(interval);
            launch1.OnElapsed += Launch1_OnElapsed;
            launch1.Start();
            this.Title = data.Name;
        }

        private void Launch1_OnElapsed()
        {
            this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (System.Threading.ThreadStart)delegate ()
            {
              
                y = data.WaveData[i, wave] / (double)100;
                y = 150 - 100 * y;
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

