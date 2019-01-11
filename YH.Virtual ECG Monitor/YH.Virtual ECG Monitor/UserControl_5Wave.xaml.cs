using HYS.Library;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace YH.Virtual_ECG_Monitor
{
    /// <summary>
    /// UserControl_Wave.xaml 的交互逻辑
    /// </summary>
    public partial class UserControl_5Wave : UserControl
    {
        int MaxwaveCount = 8;//显示波形个数上限

        public Rhythm Rhythm { get; set; }
        public int HeartRate { get; set; }


        int ecg_wave_column_index = 1;

        Content_Wave content;
        Launch launch;
        Random random;
   
        public UserControl_5Wave()
        {
            InitializeComponent();
            random = new Random();
            content = new Content_Wave();
            launch = new Launch(15000);
            launch.OnElapsed += Launch_OnElapsed;
           
            launch.Start();
        }

        private void Launch_OnElapsed()
        {
            Run_ECG(Rhythm.Rhythm_01, 60 + random.Next(40));
            int systolic = 60 + random.Next(100);
            int diastolic = systolic - random.Next(30)-10;
            Run_ABP(100, systolic, diastolic);
           Run_PLETH(120, 50);
          Run_RESP(RespType.Resp_01, 100, 60, 100, 80);
        }

        public void Run_ECG(Rhythm mRhythm, int mHeartRatet)
        {            
            //  launch_ECG.Stop();
            Rhythm = mRhythm;
            HeartRate = mHeartRatet;
            ECGSettingData ecgSetting = Setting.Get<ECGSettingData>();

            float[,]  ecg_data = content.GetWaveData_ECG(Rhythm, HeartRate, ecgSetting.Custom.Gain/5);
            float[] data = new float[ecg_data.GetLength(0)];
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = ecg_data[i, ecg_wave_column_index];
            }
            ecg_wave.Run(data, MaxwaveCount, ecgSetting.Custom.Speed);
            this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (System.Threading.ThreadStart)delegate ()
            {
                textblock_HeartRate.Text = mHeartRatet.ToString();
            });
        }
        
        public void Run_PLETH(int plot, int spo2)
        {
            WaveSettingData waveSetting = Setting.Get<WaveSettingData>();
            float[] Pleth_data = content.GetWaveData_PLETH(plot, spo2, waveSetting.Custom.PLETH.Gain/5.00f);
            pleth_wave.Run(Pleth_data, MaxwaveCount, waveSetting.Custom.PLETH.Speed*20);
            this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (System.Threading.ThreadStart)delegate ()
            {
                textblock_SpO2.Text = spo2.ToString();
            });
        }

        public void Run_ABP(int nPlot, int nSystolic, int nDiastolic)
        {
            WaveSettingData waveSetting = Setting.Get<WaveSettingData>();
            float[]  ABP_data = content.GetWaveData_ABP(nPlot, nSystolic, nDiastolic, waveSetting.Custom.ABP.Gain/10.00f);
            abp_wave.Run(ABP_data, MaxwaveCount, waveSetting.Custom.PLETH.Speed * 20);
            this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (System.Threading.ThreadStart)delegate ()
            {
                textblock_IBP.Text = string.Format("{0}/{1}", nSystolic, nDiastolic);
            });
        }

        public void Run_RESP(RespType respType,int plot, int respRate,int capacity,int respRatio)
        {     
            WaveSettingData waveSetting = Setting.Get<WaveSettingData>();
            float[]  RESP_data = content.GetWaveData_RESP(respType,plot,respRate,capacity, respRatio, waveSetting.Custom.CO2.Gain*20);
            resp_wave.Run(RESP_data, MaxwaveCount, waveSetting.Custom.PLETH.Speed * 20);

            this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (System.Threading.ThreadStart)delegate ()
            {
                textblock_RespRate.Text = respRate.ToString();
            });
        }    


    }
}
