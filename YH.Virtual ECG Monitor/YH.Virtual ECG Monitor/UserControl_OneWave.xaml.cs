using HYS.Library;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Linq;

namespace YH.Virtual_ECG_Monitor
{
    /// <summary>
    /// 用于展示波形的控件
    /// </summary>
    public partial class UserControl_OneWave : UserControl
    {
        int interval = 200;    //描点周期 单位毫秒
        Launch launch;    //定时器
        double addX;      // x轴上 点距
        int i = 0;            //
        double x = 0;  // x坐标
        int curWaveCount = 0;  //表示当前第几个波形
        int intervalCount;   //描点周期内描点个数
        float[] data;   //波形数据

        public int MaxWaveCount { get; set; }   //要展示的波形个数



        public bool IsPause
        {
            get
            {
                return launch._isPause;
            }
        }

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

        public void Pause()
        {

            launch.Pause();
        }

        public void Start()
        {
            launch.Start();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="maxWaveCount"></param>
        /// <param name="speed"></param>
        /// <param name="gain"></param>
        /// <param name="frequent"></param>
        public void Run(float[] mData, int maxWaveCount, float speed, float gain,float rate)
        {
            if (rate <= 0)
            {
                return;
            }
            if (launch == null)
            {
                launch = new Launch();
                launch.Interval = interval;
                launch.OnElapsed += launch_OnElapsed;
            }

            gain = gain / 40;
            speed = speed / 50;

            float max = mData.Max();
            float min = mData.Min();
            float valueHeight = max - min;
            this.data = new float[mData.Length];

            float controlHeight = (float)myCanvas.ActualHeight;
            for (int i = 0; i < mData.Length; i++)
            {
                data[i] = ((mData[i] - min) / valueHeight) * controlHeight * gain;
            }
            float maxData = data.Max();
            for (int i = 0; i < data.Length; i++)
            {
                if (gain < 1)
                {
                    this.data[i] += (controlHeight - maxData) / 2;
                }
                this.data[i] = controlHeight - this.data[i];
            }

            int pointAmount = maxWaveCount * data.Length ;
            int t = (int)(60000 * maxWaveCount / rate);

            // intervalCount =(int) (10*speed);
            //  interval = t * intervalCount / pointAmount;

            interval = t/ f(pointAmount, t);
            intervalCount =(int)( pointAmount * interval/ t * speed);

            addX = ActualWidth / pointAmount;
            launch.Interval = interval;
            launch.Start();
        }

        static int f(int a, int b)//最大公约数 
         {
             if (a<b) { a = a + b; b = a - b; a = a - b; }
             return (a % b == 0) ? b : f(a % b, b);
         }
 
         static int m(int a, int b)//最小公倍数 
         {
             return a* b / f(a, b);
         }


        public void Run(float[] mData, int maxWaveCount, float speed, float gain)
        {

            if (launch == null)
            {
                launch = new Launch();
                launch.Interval = interval;
                launch.OnElapsed += launch_OnElapsed;
            }

            gain = gain / 40;
            speed = speed / 50;
            float max = mData.Max();
            float min = mData.Min();
            float valueHeight = max - min;
            data = new float[mData.Length];

            float controlHeight = (float)myCanvas.ActualHeight;
            for (int i = 0; i < mData.Length; i++)
            {
                data[i] = ((mData[i] - min) / valueHeight) * controlHeight * gain;          
            }
            float maxData = data.Max();
            for (int i = 0; i < data.Length; i++)
            {
                if (gain < 1)
                {
                    this.data[i] += (controlHeight - maxData) / 2;
                }
                this.data[i] = controlHeight - this.data[i];
            }
            

            int pointAmount = (int)(maxWaveCount * this.data.Length * speed);

            if (pointAmount < 60000 / interval)
            {
                interval = 60000 / pointAmount;
                intervalCount = 1;
            }
            else
            {
                intervalCount = pointAmount * interval / 60000;
                if (intervalCount == 0)
                {
                    intervalCount = 1;
                }
            }
            addX = ActualWidth / pointAmount;
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

                if (curWaveCount >= MaxWaveCount || x > ActualWidth)
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
