using HYS.Library;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace YH.Virtual_ECG_Monitor
{
    /// <summary>
    /// UserControl_Wave.xaml 的交互逻辑
    /// </summary>
    public partial class UserControl_Wave : UserControl
    {
        int MaxwaveCount = 8;//显示波形个数上限

        public Rhythm Rhythm { get; set; }
        public int HeartRate { get; set; }


        int ecg_wave_column_index = 1;

        Content_Wave content;
        Launch launch_ECG;
        Launch launch_PLETH;
        Launch launch_ABP;
        Launch launch_RESP;

   
        public UserControl_Wave()
        {
            InitializeComponent();
            content = new Content_Wave();
            launch_ECG = new Launch();      
            launch_PLETH = new Launch();
            launch_ABP = new Launch();
            launch_RESP = new Launch();
        }

     
        public void Run_ECG(Rhythm mRhythm, int mHeartRatet)
        {
          //  launch_ECG.Stop();
            Rhythm = mRhythm;
            HeartRate = mHeartRatet;
            ECGSettingData ecgSetting = Setting.Get<ECGSettingData>();

            float[,]  ecg_data = content.GetWaveData_ECG(Rhythm, HeartRate, ecgSetting.Custom.Gain);
            float[] data = new float[ecg_data.GetLength(0)];
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = ecg_data[i, ecg_wave_column_index];
            }
            ecg_wave.Run(data, MaxwaveCount, ecgSetting.Custom.Speed);         
            textblock_HeartRate.Text = mHeartRatet.ToString();
        }


        public void Run_PLETH(int plot, int spo2)
        {
            WaveSettingData waveSetting = Setting.Get<WaveSettingData>();
            float[] Pleth_data = content.GetWaveData_PLETH(plot, spo2, waveSetting.Custom.PLETH.Gain);
            pleth_wave.Run(Pleth_data, MaxwaveCount, waveSetting.Custom.PLETH.Speed);
            textblock_SpO2.Text = spo2.ToString();
        }

        public void Run_ABP(int nPlot, int nSystolic, int nDiastolic)
        {
            WaveSettingData waveSetting = Setting.Get<WaveSettingData>();
            float[]  ABP_data = content.GetWaveData_ABP(nPlot, nSystolic, nDiastolic, waveSetting.Custom.ABP.Gain);
            abp_wave.Run(ABP_data, MaxwaveCount, waveSetting.Custom.ABP.Speed);       
            textblock_IBP.Text = string.Format("{0}/{1}", nSystolic, nDiastolic);
        }

        public void Run_RESP(RespType respType,int plot, int respRate,int capacity,int respRatio)
        {     
            WaveSettingData waveSetting = Setting.Get<WaveSettingData>();
            float[]  RESP_data = content.GetWaveData_RESP(respType,plot,respRate,capacity, respRatio, waveSetting.Custom.CO2.Gain);
            resp_wave.Run(RESP_data, MaxwaveCount, waveSetting.Custom.CO2.Speed);
            textblock_RespRate.Text = respRate.ToString();
        }    


    }
}
