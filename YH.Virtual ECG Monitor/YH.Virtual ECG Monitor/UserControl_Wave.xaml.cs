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
    public partial class UserControl_Wave : UserControl
    {
        int MaxwaveCount = 8;//显示波形个数上限

        int ecg_wave_column_index = 1;

        Content_Wave content =new Content_Wave();

        public UserControl_Wave()
        {
            InitializeComponent();
        }

        private ECG_Paras _ECG_Paras;
        public ECG_Paras ECG_Paras
        {
            get
            {
                return _ECG_Paras;
            }
            set
            {
                _ECG_Paras = value;
                Run_ECG(_ECG_Paras);
            }
        }

        private PLETH_Paras _PLETH_Paras;
        public PLETH_Paras PLETH_Paras
        {
            get
            {
                return _PLETH_Paras;
            }
            set
            {
                _PLETH_Paras = value;
                Run_PLETH(_PLETH_Paras);
            }
        }

        // ABP_Paras

        private ABP_Paras _ABP_Paras;
        public ABP_Paras ABP_Paras
        {
            get
            {
                return _ABP_Paras;
            }
            set
            {
                _ABP_Paras = value;
                Run_ABP(_ABP_Paras);
            }
        }

        //    Resp_Paras

        private Resp_Paras _Resp_Paras;
        public Resp_Paras Resp_Paras
        {
            get
            {
                return _Resp_Paras;
            }
            set
            {
                _Resp_Paras = value;
                Run_RESP(_Resp_Paras);
            }
        }

        
        public void Run_ECG(ECG_Paras paras)
        {
            //  launch_ECG.Stop();
            Rhythm rhythm = paras.Rhythm;
            int heartRate = paras.HeartRat;
            ECGSettingData ecgSetting = Setting.Get<ECGSettingData>();

            float[,] ecg_data = content.GetWaveData_ECG(rhythm, heartRate, ecgSetting.Custom.Gain / 5);
            float[] data = new float[ecg_data.GetLength(0)];
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = ecg_data[i, ecg_wave_column_index];
            }
            ecg_wave.Run(data, MaxwaveCount, ecgSetting.Custom.Speed);
            this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (System.Threading.ThreadStart)delegate ()
            {
                textblock_HeartRate.Text = heartRate.ToString();
            });
        }

        public void Run_PLETH(PLETH_Paras paras)
        {
            WaveSettingData waveSetting = Setting.Get<WaveSettingData>();
            float[] Pleth_data = content.GetWaveData_PLETH(paras.Plot, paras.Spo2, waveSetting.Custom.PLETH.Gain / 5.00f);
            pleth_wave.Run(Pleth_data, MaxwaveCount, waveSetting.Custom.PLETH.Speed);
            this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (System.Threading.ThreadStart)delegate ()
            {
                textblock_SpO2.Text = paras.Spo2.ToString();
            });
        }

        public void Run_ABP(ABP_Paras paras)
        {
            WaveSettingData waveSetting = Setting.Get<WaveSettingData>();
            float[] ABP_data = content.GetWaveData_ABP(paras.Plot, paras.Plot, paras.Diastolic, waveSetting.Custom.ABP.Gain / 10.00f);
            abp_wave.Run(ABP_data, MaxwaveCount, waveSetting.Custom.PLETH.Speed );
            this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (System.Threading.ThreadStart)delegate ()
            {
                textblock_IBP.Text = string.Format("{0}/{1}", paras.Systolic, paras.Diastolic);
            });
        }

        public void Run_RESP(Resp_Paras paras)
        {
            WaveSettingData waveSetting = Setting.Get<WaveSettingData>();
            float[] RESP_data = content.GetWaveData_RESP(paras.RespType, paras.Plot, paras.RespRate, paras.Capacity, paras.RespRatio, waveSetting.Custom.CO2.Gain * 20);
            resp_wave.Run(RESP_data, MaxwaveCount, waveSetting.Custom.PLETH.Speed);

            this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (System.Threading.ThreadStart)delegate ()
            {
                textblock_RespRate.Text = paras.RespRate.ToString();
            });
        }
    }


    public struct ECG_Paras
    {
        public Rhythm Rhythm;
        public int HeartRat;
    }

    public struct PLETH_Paras
    {
        public int Plot { get; set; }
        public int Spo2 { get; set; }
    }

    public struct ABP_Paras
    {
        public int Plot { get; set; }
        public int Systolic { get; set; }
        public int Diastolic { get; set; }
    }

    public struct Resp_Paras
    {
        public RespType RespType { get; set; }
        public int Plot { get; set; }
        public int RespRate { get; set; }
        public int Capacity { get; set; }
        public int RespRatio { get; set; }
    }
}
