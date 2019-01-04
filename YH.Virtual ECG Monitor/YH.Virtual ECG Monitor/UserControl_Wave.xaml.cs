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
    /// UserControl_Wave.xaml 的交互逻辑
    /// </summary>
    public partial class UserControl_Wave : UserControl
    {
        int waveCountMax = 8;//显示波形个数上限

        public Rhythm Rhythm { get; set; }
        public int HeartRate { get; set; }


        int ecg_wave_column_index = 1;

        Content_Wave content;
        Launch launch_ECG;
        Launch launch_PLETH;
        Launch launch_ABP;
        Launch launch_RESP;

        float[,] ecg_data;
        double ecg_addX;
        public UserControl_Wave()
        {
            InitializeComponent();
            content = new Content_Wave();        
     
        }

        public void Run(Rhythm mRhythm, int mHeartRatet)
        {
            Rhythm = mRhythm;
            HeartRate = mHeartRatet;

            ECGSettingData ecgSetting = Setting.Get<ECGSettingData>();
            launch_ECG = new Launch((int)(600 / ecgSetting.Custom.Speed));

            WaveSettingData waveSetting = Setting.Get<WaveSettingData>();
            launch_PLETH = new Launch((int)(1000 / waveSetting.Custom.PLETH.Speed));
            launch_ABP = new Launch((int)(1000 / waveSetting.Custom.ABP.Speed));
            launch_RESP = new Launch((int)(1000 / waveSetting.Custom.CO2.Speed));


            launch_ECG.OnElapsed += launch_ECG_OnElapsed;    


            ecg_data = content.GetWaveData_ECG(Rhythm, HeartRate);
            ecg_addX = (double)myCanvas1.Width / (double)waveCountMax / ((double)ecg_data.GetLength(0));

            launch_ECG.Start();
        }      

        int ecg_i = 0;
        float ecg_x = 0;
        int ecg_waveCount = 0;

        private void launch_ECG_OnElapsed()
        {          
            this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (System.Threading.ThreadStart)delegate ()
            {             
                float   y = ecg_data[ecg_i, ecg_wave_column_index];               
       
                polyline1.Points.Add(new Point(ecg_x, y));
                ecg_x += (float)ecg_addX;
                ecg_i++;

                if (ecg_i >= ecg_data.GetLength(0))
                {
                    ecg_i = 0;
                    ecg_waveCount++;
                }
                if (ecg_waveCount >= waveCountMax)
                {
                    ecg_waveCount = 0;
                    ecg_i = 0;
                    ecg_x = 0;
                    polyline1.Points.Clear();
                }
            });

        }
    }
}
