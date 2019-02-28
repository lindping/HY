using HYS.Library;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using YH.Simulator.Framework.DataDictionary;

namespace YH.Virtual_ECG_Monitor
{
    /// <summary>
    /// UserControl_Wave.xaml 的交互逻辑
    /// </summary>
    public partial class UserControl_Wave : UserControl
    {
        int ecg_wave_column_index = 1;
        AlermSoundPlayer player = new AlermSoundPlayer();
        bool existAlerm = false;
        Content_Wave content = new Content_Wave();

        public UserControl_Wave()
        {
            InitializeComponent();
            PatientInfoData patient = Setting.Get<PatientInfoData>();
            txtPatient.Text = string.Format("姓名:{0} 性别:{1}  年龄:{2}  病历号:{3}  床位号:{4}", patient.Custom.Name, patient.Custom.Sex, patient.Custom.Age, patient.Custom.MedRecNo, patient.Custom.BedNo);
            //   patient.Custom.
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
                //   Run_ECG(_ECG_Paras);
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
                //   Run_PLETH(_PLETH_Paras);
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
                //   Run_ABP(_ABP_Paras);
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
                //  Run_RESP(_Resp_Paras);
            }
        }


        public void Run_ECG()
        {
            //  launch_ECG.Stop();
            Rhythm rhythm = ECG_Paras.Rhythm;
            int heartRate = ECG_Paras.HeartRat;
            ECGSettingData ecgSetting = Setting.Get<ECGSettingData>();



            float[,] ecg_data = content.GetWaveData_ECG(rhythm, heartRate, ecgSetting.Custom.Gain);
            float[] data = new float[ecg_data.GetLength(0)];
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = ecg_data[i, ecg_wave_column_index];
            }
            this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (System.Threading.ThreadStart)delegate ()
            {
                if (!ecgSetting.Custom.ECGSwitch)
                {
                    data = new float[100];
                    textblock_HeartRate.Text = "";
                }
                else
                {
                    string rhythmName = MyDictionary.Rhythm_Basic[(int)rhythm].Split('.')[1];
                    textblock_HeartRate.Text = string.Format("{0}\n{1}", rhythmName, heartRate.ToString());
                }

                ecg_wave.Run(data, ecgSetting.Custom.Speed, ecgSetting.Custom.Gain, ECG_Paras.HeartRat);
                //     PLETH_Paras = PLETH_Paras;
                //    ABP_Paras = ABP_Paras;


            });
        }

        public void Run_PLETH()
        {
            WaveSettingData waveSetting = Setting.Get<WaveSettingData>();
            float[] Pleth_data = content.GetWaveData_PLETH(PLETH_Paras.Plot, PLETH_Paras.Spo2, waveSetting.Custom.PLETH.Gain);

            pleth_wave.Run(Pleth_data, waveSetting.Custom.PLETH.Speed, waveSetting.Custom.PLETH.Gain, ECG_Paras.HeartRat);



            this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (System.Threading.ThreadStart)delegate ()
            {
                textblock_SpO2.Text = PLETH_Paras.Spo2.ToString();
            });
        }

        public void Run_ABP()
        {
            WaveSettingData waveSetting = Setting.Get<WaveSettingData>();
            float[] ABP_data = content.GetWaveData_ABP(ABP_Paras.Plot, ABP_Paras.Systolic, ABP_Paras.Diastolic, waveSetting.Custom.ABP.Gain);
            abp_wave.Run(ABP_data, waveSetting.Custom.ABP.Speed, waveSetting.Custom.ABP.Gain, ECG_Paras.HeartRat);
            this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (System.Threading.ThreadStart)delegate ()
            {
                textblock_IBP.Text = string.Format("{0}/{1}", ABP_Paras.Systolic, ABP_Paras.Diastolic);
            });
        }

        public void Run_RESP()
        {
            WaveSettingData waveSetting = Setting.Get<WaveSettingData>();
            float[] RESP_data = content.GetWaveData_RESP(Resp_Paras.RespType, Resp_Paras.Plot, Resp_Paras.RespRate, Resp_Paras.Capacity, Resp_Paras.RespRatio, waveSetting.Custom.RESP.Gain);
            resp_wave.Run(RESP_data, waveSetting.Custom.RESP.Speed, waveSetting.Custom.RESP.Gain, Resp_Paras.RespRate);
            this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (System.Threading.ThreadStart)delegate ()
            {
                textblock_RespRate.Text = string.Format("{0}  {1}  {2}", Resp_Paras.RespRate, Resp_Paras.Capacity, Resp_Paras.RespRatio);
            });
            CheckAlerm();
        }

        public void Run3Wave()
        {
            Run_ABP();
            Run_PLETH();
            Run_ECG();
            CheckAlerm();
        }

        public void RunAllWave()
        {
            Run3Wave();
            Run_RESP();
       

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bool? dialog;
            Button btn = sender as Button;
            ECGSetting ecgSetting;
            switch (btn.Content.ToString())
            {

                case "ECG设置":
                    ECGRunSetting runSetting = new ECGRunSetting(ECG_Paras.Rhythm, ECG_Paras.HeartRat);
                    dialog = runSetting.ShowDialog();
                    if (dialog.HasValue && dialog.Value)
                    {
                        ECG_Paras = new ECG_Paras() { Rhythm = runSetting.Rhythm, HeartRat = runSetting.HeartRate };
                        Run3Wave();
                    }
                    break;
                case "ECG选项":
                    ecgSetting = new ECGSetting();
                    dialog = ecgSetting.ShowDialog();
                    if (dialog.HasValue && dialog.Value)
                    {
                        Run3Wave();
                    }
                    break;
                case "ABP设置":
                    ABPRunSetting abpSetting = new ABPRunSetting(ABP_Paras);
                    dialog = abpSetting.ShowDialog();
                    if (dialog.HasValue && dialog.Value)
                    {
                        ABP_Paras = abpSetting.ABP_Paras;
                        Run3Wave();
                    }
                    break;
                case "ABP选项":
                    WaveSetting waveSetting = new WaveSetting(1);
                    dialog = waveSetting.ShowDialog();
                    if (dialog.HasValue && dialog.Value)
                    {
                        Run3Wave();
                    }
                    break;

                case "PLETH设置":
                    PLETHRunSetting plethRunSetting = new PLETHRunSetting(PLETH_Paras);
                    dialog = plethRunSetting.ShowDialog();
                    if (dialog.HasValue && dialog.Value)
                    {
                        PLETH_Paras = plethRunSetting.PLETH_Paras;
                        Run3Wave();
                    }
                    break;
                case "PLETH选项":
                    waveSetting = new WaveSetting(0);
                    dialog = waveSetting.ShowDialog();
                    if (dialog.HasValue && dialog.Value)
                    {
                        Run3Wave();
                    }
                    break;

                case "RESP设置":
                    RESPRunSetting respRunSetting = new RESPRunSetting(Resp_Paras);
                    dialog = respRunSetting.ShowDialog();
                    if (dialog.HasValue && dialog.Value)
                    {
                        Resp_Paras = respRunSetting.Resp_Paras;
                        Run_RESP();
                    }
                    break;
                case "RESP选项":
                    waveSetting = new WaveSetting(3);
                    dialog = waveSetting.ShowDialog();
                    if (dialog.HasValue && dialog.Value)
                    {
                        Run_RESP();
                    }
                    break;
            }
        }

        private void btPause_Click(object sender, RoutedEventArgs e)
        {
            if (ecg_wave.IsPause)
            {
                ecg_wave.Start();
            }
            else
            {
                ecg_wave.Pause();
            }

            if (resp_wave.IsPause)
            {
                resp_wave.Start();
            }
            else
            {
                resp_wave.Pause();
            }

            if (abp_wave.IsPause)
            {
                abp_wave.Start();
            }
            else
            {
                abp_wave.Pause();
            }

            if (pleth_wave.IsPause)
            {
                pleth_wave.Start();
            }
            else
            {
                pleth_wave.Pause();
            }

        }

        private void btSetting_Click(object sender, RoutedEventArgs e)
        {
            new Main().ShowDialog();
        }

        /// <summary>
        /// 检查是否需要报警
        /// </summary>
        public void CheckAlerm()
        {
            //  List<WaveSettingItem> list = new List<WaveSettingItem>();
            existAlerm = false;            
            player.Stop();
            WaveSettingData waveSetting = Setting.Get<WaveSettingData>();

            WarnRange range = waveSetting.Custom.PLETH.WarnRanges[0];
            textblock_SpO2.Foreground = Brushes.White;
            if (PLETH_Paras.Spo2 < range.Min || PLETH_Paras.Spo2 > range.Max)
            {
                if (waveSetting.Custom.PLETH.WarnSwitch)
                {
                    existAlerm = true;
                }
                textblock_SpO2.Foreground = Brushes.Red;
            }

            textblock_RespRate.Foreground = Brushes.White;
            range = waveSetting.Custom.RESP.WarnRanges[0];
            if (Resp_Paras.RespRate < range.Min || Resp_Paras.RespRate > range.Max)
            {
                if (waveSetting.Custom.RESP.WarnSwitch)
                {
                    existAlerm = true;
                }
                textblock_RespRate.Foreground = Brushes.Red;
            }

            textblock_IBP.Foreground = Brushes.White;
            if (waveSetting.Custom.ABP.WarnRanges.Count == 3)
            {
                int BPAverage = (ABP_Paras.Systolic + 2 * ABP_Paras.Diastolic) / 3;
                range = waveSetting.Custom.ABP.WarnRanges[0];
                WarnRange range1 = waveSetting.Custom.ABP.WarnRanges[1];
                WarnRange range2 = waveSetting.Custom.ABP.WarnRanges[2];
                if (ABP_Paras.Systolic < range.Min || ABP_Paras.Systolic > range.Max
                    || ABP_Paras.Diastolic < range1.Min || ABP_Paras.Diastolic > range1.Max
                    || BPAverage < range2.Min || BPAverage > range2.Max
                    )
                {
                    if (waveSetting.Custom.ABP.WarnSwitch)
                    {
                        existAlerm = true;
                    }
                    textblock_IBP.Foreground = Brushes.Red;
                }
            }         
          
             ecg_wave.OnWaveStart -= Ecg_wave_OnWaveTop;        
            textblock_HeartRate.Foreground = Brushes.White;
            ECGSettingData ecgSetting = Setting.Get<ECGSettingData>();
            if (ECG_Paras.HeartRat < ecgSetting.Custom.Min || ECG_Paras.HeartRat > ecgSetting.Custom.Max)
            {
                if (ecgSetting.Custom.Warning)
                {
                    existAlerm = true;
                }
                textblock_HeartRate.Foreground = Brushes.Red;
            }

            if (existAlerm)
            {
                player.PlayWarnSound();
          
            }
            else
            {
                ecg_wave.OnWaveStart += Ecg_wave_OnWaveTop;
            }

        }

        private void Ecg_wave_OnWaveTop()
        {
            player.PlayNormalSoundOneTime();
        }
    }


    public class ECG_Paras
    {
        public Rhythm Rhythm;
        public int HeartRat;
    }

    public class PLETH_Paras
    {
        public int Plot { get; set; }
        public int Spo2 { get; set; }
    }

    public class ABP_Paras
    {
        public int Plot { get; set; }
        public int Systolic { get; set; }
        public int Diastolic { get; set; }
    }

    public class Resp_Paras
    {
        public RespType RespType { get; set; }
        public int Plot { get; set; }
        public int RespRate { get; set; }
        public int Capacity { get; set; }
        public int RespRatio { get; set; }
    }
}
