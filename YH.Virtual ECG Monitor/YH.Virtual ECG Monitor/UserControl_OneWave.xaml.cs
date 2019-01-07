using HYS.Library;
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
using System.Windows.Threading;

namespace YH.Virtual_ECG_Monitor
{
    /// <summary>
    /// UserControl_OneWave.xaml 的交互逻辑
    /// </summary>
    public partial class UserControl_OneWave : UserControl
    {
        Launch launch;
        double addX;
        int i = 0;
        float x = 0;
        int curWaveCount = 0;

        float[] data;
        public int MaxWaveCount { get; set; }

        public UserControl_OneWave()
        {
            InitializeComponent();
            MaxWaveCount = 8;
        }

        public void Run(float[] data,int maxWaveCount,float speed)
        {
            if (IsLoaded)
            {
                this.data = data;
                MaxWaveCount = maxWaveCount;
                addX = (double)ActualWidth / (double)maxWaveCount / ((double)data.GetLength(0));
                launch = new Launch((int)(1000 / speed));
                launch.OnElapsed += launch_OnElapsed;
                launch.Start();
            }
        }

        private void launch_OnElapsed()
        {
            this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (System.Threading.ThreadStart)delegate ()
            {
                float y = data[i];

                polyline.Points.Add(new Point(x, y));
                x += (float)addX;
                i++;

                if (i >= data.GetLength(0))
                {
                    i = 0;
                    curWaveCount++;
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
