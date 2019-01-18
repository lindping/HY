using HYS.Library;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

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
        int i = 0;
        double x = 0;
        int curWaveCount = 0;
        int intervalCount;
        float[] data;
        public int MaxWaveCount { get; set; }

        public UserControl_OneWave()
        {
            InitializeComponent();
            MaxWaveCount = 8;
        }

        public void Run(float[] data, int maxWaveCount, float speed)
        {
          
            if (launch == null)
            {
                launch = new Launch();
                launch.Interval = interval;
                launch.OnElapsed += launch_OnElapsed;
            }
        
            this.data = data;
            MaxWaveCount = maxWaveCount;         

             int  pointAmount= (int)(maxWaveCount * data.GetLength(0) * speed);
        
            if (pointAmount < 60000 / interval)
            {
                interval = 60000 / pointAmount;
                intervalCount = 1;
            }
            else
            {
                intervalCount = pointAmount * interval / 60000;
            }
            addX = ActualWidth /(double)( (maxWaveCount * data.Length));

            launch.Start();
        }

        private void launch_OnElapsed()
        {
            this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (System.Threading.ThreadStart)delegate ()
            {
                if (i >= data.GetLength(0))
                {
                    i = 0;
                    curWaveCount++;
                }

                for (int j = 0; j < intervalCount; j++)
                {
                    if (i >= data.Length)
                    {
                        break;
                    }
                    float y = data[i];
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
