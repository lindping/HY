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
using System.Windows.Shapes;
using System.Windows.Threading;
using YH.Network.Framework;
using YH.Simulator.Framework.Resolve;

namespace YH.Virtual_ECG_Monitor
{
    /// <summary>
    /// WaveDisplay.xaml 的交互逻辑
    /// </summary>
    public partial class WaveDisplay : Window
    {
        NetClient m_NetClient;
        public WaveDisplay()
        {
            InitializeComponent();
            NetInitializeComponent();
        }
        private void NetInitializeComponent()
        {
            m_NetClient = new NetClient(new Coder(Coder.EncodingMothord.Default));

            //m_NetClient.Resovlver=new DatagramResolver("]}");

            m_NetClient.Resovlver = new Network.Framework.DatagramResolver(new byte[4] { 0xfa, 0xfb, 0xfc, 0xfd });

            m_NetClient.ReceivedDatagram += M_NetClient_ReceivedDatagram;

            m_NetClient.DisConnectedServer += M_NetClient_DisConnectedServer;

            m_NetClient.ConnectedServer += M_NetClient_ConnectedServer;

            m_NetClient.Connect("127.0.0.1", 8899);
            //m_NetClient.Connect("10.10.100.254", 8899);
            //  string ip = ConfigurationManager.AppSettings["NetClientIP"].ToString();
            //  int port = int.Parse(ConfigurationManager.AppSettings["NetClientPort"].ToString());
            //     m_NetClient.Connect(ip, port);
        }

        private void M_NetClient_ConnectedServer(object sender, NetEventArgs e)
        {
            //throw new NotImplementedException();
            string info = string.Format("A Client:{0} connect server :{1}", e.ClientSession,
             e.ClientSession.ClientSocket.RemoteEndPoint.ToString());

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

                if (data!=null &&  data.Length>0 && Convert.ToInt32(data[0])== LifeSign_Paras.CategoryLevel0_LifeSign)
                {
                    RunWave(LifeSign_Paras.GetData_ECG(data));                  
                }
            
                //  Modle.Function Function = Resolve.Resolve.GetData_Treatment(e.ClientSession.RecvDataBuffer, ref _treatment);
                //  SetData_Treatment(Function, _treatment);
            });
        }



        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            // 设置全屏
            this.WindowState = System.Windows.WindowState.Normal;
            this.WindowStyle = System.Windows.WindowStyle.None;
            this.ResizeMode = System.Windows.ResizeMode.NoResize;
            //  this.Topmost = true;

            this.Left = 0.0;
            this.Top = 0.0;
            this.Width = System.Windows.SystemParameters.PrimaryScreenWidth;
            this.Height = System.Windows.SystemParameters.PrimaryScreenHeight;

            RunWave(null);



        }

        private void RunWave(int[] paras)
        {
            VirtualManAttributeData setting = Setting.Get<VirtualManAttributeData>();
            int heartRate = setting.Custom.HeartRate.Value;
            int systolic = setting.Custom.IBP[0].Value;
            int diastolic = setting.Custom.IBP[1].Value;
            int spo2 = setting.Custom.SPO2.Value;

            int respRate = setting.Custom.RESP.Value;
            int respRatio = 80;
            int capacity = 3000;



            if (paras != null && paras.Length == 6)
            {              
                if (paras[1] == LifeSign_Paras.CategoryLevel1_Circulation)
                {
                    if (paras[2] == LifeSign_Paras.CategoryLevel1_Circulation_HeartRate)
                    {
                        uc_wave.ECG_Paras.HeartRat = paras[3];
                    }
                    else if (paras[2] == LifeSign_Paras.CategoryLevel1_Circulation_SPO2)
                    {
                        uc_wave.PLETH_Paras.Spo2 = paras[3];
                    }
                    else if (paras[2] == LifeSign_Paras.CategoryLevel1_Circulation_ABP)
                    {
                        uc_wave.ABP_Paras.Systolic = paras[3];
                        uc_wave.ABP_Paras.Diastolic = paras[4];
                    }
                }
                else if (paras[1] == LifeSign_Paras.CategoryLevel1_Respiratory)
                {
                    if (paras[2] == LifeSign_Paras.CategoryLevel1_Respiratory_RespRate)
                    {
                        uc_wave.Resp_Paras.RespRate = paras[3];
                    }
                    else if (paras[2] == LifeSign_Paras.CategoryLevel1_Respiratory_Capacity)
                    {
                        uc_wave.Resp_Paras.RespRate = paras[3] * 256 + paras[4];
                    }
                    else if (paras[2] == LifeSign_Paras.CategoryLevel1_Respiratory_RespRatio)
                    {
                        uc_wave.Resp_Paras.RespRatio = paras[3] ;
                    }
                }
                //   uc_wave.RunAllWave();
                uc_wave.Run3Wave();

            }
            else
            {
                uc_wave.ABP_Paras = new ABP_Paras() { Systolic = systolic, Diastolic = diastolic, Plot = 200 };
                uc_wave.Resp_Paras = new Resp_Paras() { RespType = RespType.Resp_02, Capacity = capacity, Plot = 200, RespRate = respRate, RespRatio = respRatio };
                uc_wave.PLETH_Paras = new PLETH_Paras() { Plot = 200, Spo2 = spo2 };
                uc_wave.ECG_Paras = new ECG_Paras() { HeartRat = setting.Custom.HeartRate.Value, Rhythm = Rhythm.Rhythm_01 };
                uc_wave.RunAllWave();
            }

         
        }
    }
}
