using HYS.Library;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using YH.Network.Framework;
using YH.Simulator.Framework.DataDictionary;
using YH.Simulator.Framework.Resolve;

namespace YH.Virtual_ECG_Monitor
{
    /// <summary>
    /// UserControl_Wave.xaml 的交互逻辑
    /// </summary>
    public partial class UserControl_Wave : UserControl
    {
        NetClient m_NetClient;

        int ecg_wave_column_index = 1;
        AlermSoundPlayer player = new AlermSoundPlayer();
        bool existAlerm = false;
        Content_Wave content = new Content_Wave();



        public UserControl_Wave()
        {
            InitializeComponent();
            PatientInfoData patient = Setting.Get<PatientInfoData>();
            txtPatient.Text = string.Format("姓名:{0} 性别:{1}  年龄:{2}  病历号:{3}  床位号:{4}", patient.Custom.Name, patient.Custom.Sex, patient.Custom.Age, patient.Custom.MedRecNo, patient.Custom.BedNo);

         //   NetInitializeComponent();
        }

        private void NetInitializeComponent()
        {
            m_NetClient = new NetClient(new Coder(Coder.EncodingMothord.Default));

            //m_NetClient.Resovlver=new DatagramResolver("]}");

            m_NetClient.Resovlver = new Network.Framework.DatagramResolver(new byte[4] { 0xfa, 0xfb, 0xfc, 0xfd });

            m_NetClient.ReceivedDatagram += M_NetClient_ReceivedDatagram;

            m_NetClient.DisConnectedServer += M_NetClient_DisConnectedServer;

            m_NetClient.ConnectedServer += M_NetClient_ConnectedServer;

            if (m_NetClient.IsConnected)
            {
                m_NetClient.Close();
            }
            try
            {
                string ip = ConfigurationManager.AppSettings["NetClientIP"].ToString();
                int port = int.Parse(ConfigurationManager.AppSettings["NetClientPort"].ToString());
                m_NetClient.Connect(ip, port);             
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }      
        }


        private void M_NetClient_ConnectedServer(object sender, NetEventArgs e)
        {
            //throw new NotImplementedException();
            string info = string.Format("A Client:{0} connect server :{1}", e.ClientSession,
             e.ClientSession.ClientSocket.RemoteEndPoint.ToString());

            SyncParas();
            MessageBox.Show("连接成功");
        }

        private void M_NetClient_DisConnectedServer(object sender, NetEventArgs e)
        {
            //throw new NotImplementedException();
            string info;

            if (e.ClientSession.TypeOfExit == Session.ExitType.ExceptionExit)
            {
                info = string.Format("A Client Session:{0} Exception Closed.",
                 e.ClientSession.ID);
            }
            else
            {
                info = string.Format("A Client Session:{0} Normal Closed.",
                 e.ClientSession.ID);
            }
        }

        private void M_NetClient_ReceivedDatagram(object sender, NetEventArgs e)
        {
            this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (System.Threading.ThreadStart)delegate ()
            {
                byte[] data = e.ClientSession.RecvDataBuffer;

                if (data != null && data.Length > 0 && Convert.ToInt32(data[0]) == LifeSign_Paras.CategoryLevel0_LifeSign)
                {
                    RunWave(LifeSign_Paras.GetData_ECG(data));
                }

            });
        }

        public void RunWave(int[] paras)
        {
            VirtualManAttributeData setting = Setting.Get<VirtualManAttributeData>();
            int heartRate = setting.Custom.HeartRate.Value;
            int systolic = setting.Custom.IBP[0].Value;
            int diastolic = setting.Custom.IBP[1].Value;
            int spo2 = setting.Custom.SPO2.Value;

            int respRate = setting.Custom.RESP.Value;        

            if (paras != null && paras.Length == 6)
            {
                if (paras[1] == LifeSign_Paras.CategoryLevel1_Circulation)
                {
                    if (paras[2] == LifeSign_Paras.CategoryLevel1_Circulation_HeartRate)
                    {
                        ECG_Paras.HeartRat = paras[3];
                    }
                    else if (paras[2] == LifeSign_Paras.CategoryLevel1_Circulation_SPO2)
                    {
                        PLETH_Paras.Spo2 = paras[3];
                    }
                    else if (paras[2] == LifeSign_Paras.CategoryLevel1_Circulation_ABP)
                    {
                        ABP_Paras.Systolic = paras[3];
                        ABP_Paras.Diastolic = paras[4];
                    }
                    Run3Wave(true);
                }
                else if (paras[1] == LifeSign_Paras.CategoryLevel1_Respiratory)
                {
                    if (paras[2] == LifeSign_Paras.CategoryLevel1_Respiratory_RespRate)
                    {
                        Resp_Paras.RespRate = paras[3];
                    }
                    else if (paras[2] == LifeSign_Paras.CategoryLevel1_Respiratory_Capacity)
                    {
                        Resp_Paras.RespRate = paras[3] * 256 + paras[4];
                    }
                    else if (paras[2] == LifeSign_Paras.CategoryLevel1_Respiratory_RespRatio)
                    {
                        Resp_Paras.RespRatio = paras[3];
                    }
                    Run_RESP(true);
                }
                RunAllWave();
            }
        }

        public void FirstRunWave()
        {
            VirtualManAttributeData setting = Setting.Get<VirtualManAttributeData>();
            int heartRate = setting.Custom.HeartRate.Value;
            int systolic = setting.Custom.IBP[0].Value;
            int diastolic = setting.Custom.IBP[1].Value;
            int spo2 = setting.Custom.SPO2.Value;

            int respRate = setting.Custom.RESP.Value;
            int respRatio = 80;
            int capacity = 3000;

            ABP_Paras = new ABP_Paras() { Systolic = systolic, Diastolic = diastolic, Plot = 200 };
            Resp_Paras = new Resp_Paras() { RespType = RespType.Resp_02, Capacity = capacity, Plot = 200, RespRate = respRate, RespRatio = respRatio };
            PLETH_Paras = new PLETH_Paras() { Plot = 200, Spo2 = spo2 };
            ECG_Paras = new ECG_Paras() { HeartRat = setting.Custom.HeartRate.Value, Rhythm = Rhythm.Rhythm_01 };
            RunAllWave();
        }

        private void SyncParas()
        {
            if (m_NetClient!=null && m_NetClient.IsConnected)
            {
                List<byte[]> data = LifeSign_Paras.GetByte_ECG(ECG_Paras.HeartRat, PLETH_Paras.Spo2, ABP_Paras.Systolic * 256 + ABP_Paras.Diastolic, Resp_Paras.RespRate, Resp_Paras.Capacity, Resp_Paras.RespRatio);
                data.ForEach(p => m_NetClient.SendBytes(p));
            }
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

        public void Run_RESP(bool isCheck)
        {
            WaveSettingData waveSetting = Setting.Get<WaveSettingData>();
            float[] RESP_data = content.GetWaveData_RESP(Resp_Paras.RespType, Resp_Paras.Plot, Resp_Paras.RespRate, Resp_Paras.Capacity, Resp_Paras.RespRatio, waveSetting.Custom.RESP.Gain);
            resp_wave.Run(RESP_data, waveSetting.Custom.RESP.Speed, waveSetting.Custom.RESP.Gain, Resp_Paras.RespRate);
            this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (System.Threading.ThreadStart)delegate ()
            {
                textblock_RespRate.Text = string.Format("{0}  {1}  {2}", Resp_Paras.RespRate, Resp_Paras.Capacity, Resp_Paras.RespRatio);
            });
            if (isCheck)
            {
                CheckAlerm();
                SyncParas();
            }
        }

        public void Run3Wave(bool isCheck)
        {
            Run_ABP();
            Run_PLETH();
            Run_ECG();
            if (isCheck)
            {
                CheckAlerm();
                SyncParas();
            }
        }

        public void RunAllWave()
        {
            Run3Wave(false);
            Run_RESP(true);

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
                        Run3Wave(true);
                    }
                    break;
                case "ECG选项":
                    ecgSetting = new ECGSetting();
                    dialog = ecgSetting.ShowDialog();
                    if (dialog.HasValue && dialog.Value)
                    {
                        Run3Wave(true);
                    }
                    break;
                case "ABP设置":
                    ABPRunSetting abpSetting = new ABPRunSetting(ABP_Paras);
                    dialog = abpSetting.ShowDialog();
                    if (dialog.HasValue && dialog.Value)
                    {
                        ABP_Paras = abpSetting.ABP_Paras;
                        Run3Wave(true);
                    }
                    break;
                case "ABP选项":
                    WaveSetting waveSetting = new WaveSetting(1);
                    dialog = waveSetting.ShowDialog();
                    if (dialog.HasValue && dialog.Value)
                    {
                        Run3Wave(true);
                    }
                    break;

                case "PLETH设置":
                    PLETHRunSetting plethRunSetting = new PLETHRunSetting(PLETH_Paras);
                    dialog = plethRunSetting.ShowDialog();
                    if (dialog.HasValue && dialog.Value)
                    {
                        PLETH_Paras = plethRunSetting.PLETH_Paras;
                        Run3Wave(true);
                    }
                    break;
                case "PLETH选项":
                    waveSetting = new WaveSetting(0);
                    dialog = waveSetting.ShowDialog();
                    if (dialog.HasValue && dialog.Value)
                    {
                        Run3Wave(true);
                    }
                    break;

                case "RESP设置":
                    RESPRunSetting respRunSetting = new RESPRunSetting(Resp_Paras);
                    dialog = respRunSetting.ShowDialog();
                    if (dialog.HasValue && dialog.Value)
                    {
                        Resp_Paras = respRunSetting.Resp_Paras;
                        Run_RESP(true);
                    }
                    break;
                case "RESP选项":
                    waveSetting = new WaveSetting(3);
                    dialog = waveSetting.ShowDialog();
                    if (dialog.HasValue && dialog.Value)
                    {
                        Run_RESP(true);
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

        private void btConnection_Click(object sender, RoutedEventArgs e)
        {
            NetInitializeComponent();
        }

        private void btClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Close();
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
