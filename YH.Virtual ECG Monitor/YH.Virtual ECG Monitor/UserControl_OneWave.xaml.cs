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
     
        Launch launch;    //定时器
        int interval = 100;    //定时器描点周期 单位毫秒   
        int intervalCount;   //描点周期内描点个数    
        float[] runData;  //一个展示波形内的数据
        int data_i = 0;    
        int dataLength = 0; //一个波形的数据长度
        double showTime; //一个展示周期的时间， 毫秒
        DateTime startTime;  //波形展示开始时间      
        public float MaxWaveCount { get; set; }   //要展示的波形个数

        double remain; //intervalCoun取整以后形成的差

        public delegate void OnWaveTopEventDelegate();
        public event OnWaveTopEventDelegate OnWaveStart;

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
            if (Name.ToLower().Contains("ecg"))
            {
           //     player.PlayNormalSound();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="maxWaveCount"></param>
        /// <param name="speed"></param>
        /// <param name="gain"></param>
        /// <param name="frequent"></param>
        public void Run(float[] data, float speed, float gain, float rate)
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
            MaxWaveCount = 25 * 16 / speed;
            showTime = 60000 * MaxWaveCount / rate;
            intervalCount = (int)Math.Ceiling(MaxWaveCount * data.Length * interval / showTime);
            remain = intervalCount - MaxWaveCount * data.Length * interval / showTime;

            runData = GetRunData(data, gain);
            dataLength = data.Length;
            data_i = 0;
            Start();
        }

        private float[] GetRunData(float[] data, float gain)
        {
            //  this.data = new float[mData.Length];
            List<float> list = new List<float>();
            float controlHeight = (float)myCanvas.ActualHeight;
            float max = data.Max();
            float min = data.Min();
            float valueHeight = max - min;

            for (int i = 0; i < data.Length; i++)
            {
                list.Add(((data[i] - min) / valueHeight) * controlHeight * gain);
            }

            float maxData = list.Max();
            for (int i = 0; i < list.Count; i++)
            {
                if (gain < 1)
                {
                    list[i] += (controlHeight - maxData) / 2;
                }
                list[i] = controlHeight - list[i];
            };

            List<float> newList = new List<float>();
            for (int i = 0; i < MaxWaveCount; i++)
            {
                newList.AddRange(list);
            }
            return newList.ToArray();
        }

    
        private void launch_OnElapsed()
        {
            //if (sumRemain >= 2*intervalCount)
            //{
            //    sumRemain -= intervalCount ;
            //    return;
            //}
          

            this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (System.Threading.ThreadStart)delegate ()
            {           
                if (data_i == 0)
                {            
                    polyline.Points.Clear();
                    startTime = DateTime.Now;
                }
                bool isWaveStart = false;
                for (int j = 0; j < intervalCount; j++)
                {               

                    if (data_i >= runData.Length)
                    {
                        if (showTime < (DateTime.Now - startTime).TotalMilliseconds)  //波形展示完毕，如果时间还没到就跳出循环等待下一次计时。
                        {
                            data_i = 0;
                        }
                        break;
                    }

                    float y = runData[data_i++];
                    double x = ActualWidth * data_i / (runData.Length - 1);
                    polyline.Points.Add(new Point(x, y));

                    if (OnWaveStart != null  && data_i % dataLength == 0)
                    {
                        isWaveStart = true;
                    }
                }
                if (isWaveStart)
                {
                    OnWaveStart();
                }         

            });
        }
    }
}
