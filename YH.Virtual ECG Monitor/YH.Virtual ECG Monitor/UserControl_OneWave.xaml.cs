using HYS.Library;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Linq;
using System.IO;
using System;
using System.Threading;

namespace YH.Virtual_ECG_Monitor
{
    /// <summary>
    /// 用于展示波形的控件
    /// </summary>
    public partial class UserControl_OneWave : UserControl
    {
        int interval = 100;    //描点周期 单位毫秒
        Launch launch;    //定时器
        double addX;      // x轴上 点距
        int arr_i = 0;

        int intervalCount;   //描点周期内描点个数
        float[] data;   //波形数据
        float[] runData;

        double showTime; //一个展示周期的时间， 毫秒
        DateTime startTime;  //波形展示开始时间


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
        public void Run(float[] mData, int maxWaveCount, float speed, float gain, float rate)
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
            rate = rate * speed;

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
            List<float> list = new List<float>();
            for (int i = 0; i < MaxWaveCount; i++)
            {
                list.AddRange(data);
            }
            runData = list.ToArray();

            float PointPerMin = rate * data.Length;  //每分钟波形的数据长度
            intervalCount = (int)Math.Ceiling(PointPerMin * interval / 60000); // 每个周期内需要的数据点数

            showTime = 60000 * MaxWaveCount / rate;
            arr_i = 0;
            launch.Start();
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

                if (arr_i == 0)
                {
                    polyline.Points.Clear();
                    startTime = DateTime.Now;
                }

                for (int j = 0; j < intervalCount; j++)
                {
                    if (arr_i >= runData.Length)
                    {
                        if(showTime < (DateTime.Now - startTime).TotalMilliseconds)
                        {
                            arr_i = 0;                         
                        }
                      break;                    
                    }
                    float y = runData[arr_i++];
                    double x = ActualWidth * arr_i / (runData.Length - 1);
                    polyline.Points.Add(new Point(x, y));
                    
                }
            });
        }
    }
}
