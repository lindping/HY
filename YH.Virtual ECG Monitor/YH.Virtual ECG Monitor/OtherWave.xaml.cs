using HYS.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
using YH.Simulator.Framework;

namespace YH.Virtual_ECG_Monitor
{
    /// <summary>
    /// OtherWave.xaml 的交互逻辑
    /// </summary>
    public partial class OtherWave : Window
    {
        MediaPlayer player;
        Launch launch1;
        //  Launch soundLauch;
        double addX = 0;//x轴上增量值
        int IndexInterval = 1;   //样本数据的数组索引间隔
        double x = 0; // x坐标
        double y = 0; // y坐标
        int i = 0;    //样本数据的数组索引

        WaveData_PLETH data;

        int rhythm;  //心率类型
        int wave;    //心律波形索引值

        int dataLength; //数据长度
        int waveCount = 0; //显示波形个数计数器
        int waveCountMax = 8;//显示波形个数上限

        WaveSettingModel setting;
        public OtherWave()
        {
            InitializeComponent();
            InitData();
         //   this.rhythm = rhythm;

        }

        private void InitData()
        {
            setting = Setting.Get<WaveSettingData>().Custom;
            data = DataToObject.To<WaveData_PLETH>(0);
            dataLength = data.WaveData.GetLength(0);
            IndexInterval = dataLength > 1000 ? 10 : 1;
            addX = (double)myCanvas.Width / (double)waveCountMax / ((double)dataLength / (double)IndexInterval);

            player = new MediaPlayer();
        //    player.Volume = setting.QRSVolumn / (double)10;

            int interval = (int)(1000 / setting.PLETH.Speed);
            launch1 = new Launch(interval);
            launch1.OnElapsed += Launch1_OnElapsed;
            launch1.Start();
            this.Title = data.Name;
        }

        private void Launch1_OnElapsed()
        {
            this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (System.Threading.ThreadStart)delegate ()
            {
                y = data.WaveData[i];

                SoundPlay(y);

            //    y *= setting.PLETH.Gain;
                polyline1.Points.Add(new Point(x, y));
                x += addX;
                i += IndexInterval;

                if (i >= dataLength)
                {
                    i = 0;
                    waveCount++;
                }
                if (waveCount >= waveCountMax)
                {
                    waveCount = 0;
                    i = 0;
                    x = 0;
                    polyline1.Points.Clear();
                }
            });
        }

        private void SoundPlay(double y)
        {
            this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (System.Threading.ThreadStart)delegate ()
            {
                if (i > 0 && i < dataLength - 1 && ((y <= data.WaveData[i - 1] && y < data.WaveData[i + 1]) || (y < data.WaveData[i - 1] && y <= data.WaveData[i + 1])))
                {
                    player.Open(new Uri(Constants.GeneralWaveFile, UriKind.Relative));
                    player.Play();
                }
            });
        }

    }
}
