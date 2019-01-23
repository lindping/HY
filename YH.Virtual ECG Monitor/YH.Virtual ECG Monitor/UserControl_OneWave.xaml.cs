using HYS.Library;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Linq;

namespace YH.Virtual_ECG_Monitor
{
    /// <summary>
    /// UserControl_OneWave.xaml 的交互逻辑
    /// </summary>
    public partial class UserControl_OneWave : UserControl
    {
        int interval = 60;
        Launch launch;
        double addX;
        double addY;
        int i = 0;
        double x = 0;
        int curWaveCount = 0;
        int intervalCount;
        float[] data;
    //    double controlHeight;
        public int MaxWaveCount { get; set; }

        public UserControl_OneWave()
        {
            InitializeComponent();
            MaxWaveCount = 8;
     //       controlHeight = ActualHeight;
        }

        public void Stop()
        {
            if (launch != null)
            {
                launch.Stop();
                launch = null;
            }
        }

        public void Run(float[] data, int maxWaveCount, float speed)
        {

            if (launch == null)
            {
                launch = new Launch();
                launch.Interval = interval;
                launch.OnElapsed += launch_OnElapsed;
            }

         
            float max =data.Max();
            float min = data.Min();
            float valueHeight = max - min;
            this.data = new float[data.Length];

            float controlHeight = (float)myCanvas.ActualHeight*0.9f;
            for (int i = 0; i < data.Length; i++)
            {
                  this.data[i] = controlHeight-((data[i] - min)/ valueHeight) * controlHeight+5;            
            }

            MaxWaveCount = maxWaveCount;

            int pointAmount = (int)(maxWaveCount * this.data.Length * (speed/5f));

            if (pointAmount < 60000 / interval)
            {
                interval = 60000 / pointAmount;
                intervalCount = 1;
            }
            else
            {
                intervalCount = pointAmount * interval / 60000;
            }
            addX = ActualWidth / (double)((maxWaveCount * data.Length));

            launch.Start();
        }

        private void launch_OnElapsed()
        {
            this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (System.Threading.ThreadStart)delegate ()
            {
                if (i >= this.data.GetLength(0))
                {
                    i = 0;
                    curWaveCount++;
                }

                for (int j = 0; j < intervalCount; j++)
                {
                    if (i >= this.data.Length)
                    {
                        break;
                    }
                    float y = this.data[i];
                    polyline.Points.Add(new Point(x, y));
                    x += addX;
                    i++;
                }

                if (curWaveCount >= MaxWaveCount)
                {
                    curWaveCount = 0;
                    i = 0;
                    x = 0;
                    polyline.Points.Clear();
                }
            });
        }
    }
}
