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
        int waveCountMax = 8;//显示波形个数上限

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

        float[,] ecg_data;
        double ecg_addX;
        int ecg_i = 0;
        float ecg_x = 0;
        int ecg_waveCount = 0;
        public void Run_ECG(Rhythm mRhythm, int mHeartRatet)
        {
          //  launch_ECG.Stop();
            Rhythm = mRhythm;
            HeartRate = mHeartRatet;
            ECGSettingData ecgSetting = Setting.Get<ECGSettingData>();
            launch_ECG = new Launch((int)(600 / ecgSetting.Custom.Speed));

            WaveSettingData waveSetting = Setting.Get<WaveSettingData>();   
            launch_ABP = new Launch((int)(1000 / waveSetting.Custom.ABP.Speed));
            launch_RESP = new Launch((int)(1000 / waveSetting.Custom.CO2.Speed));

            
            ecg_data = content.GetWaveData_ECG(Rhythm, HeartRate);
            ecg_addX = (double)myCanvas1.Width / (double)waveCountMax / ((double)ecg_data.GetLength(0));

            launch_ECG.OnElapsed += launch_ECG_OnElapsed;
            launch_ECG.Start();
        }
        private void launch_ECG_OnElapsed()
        {
            this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (System.Threading.ThreadStart)delegate ()
            {
                float y = ecg_data[ecg_i, ecg_wave_column_index];
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


        float[] ABP_data;
        double ABP_addX;
        int ABP_i = 0;
        float ABP_x = 0;
        int ABP_waveCount = 0;
        public void Run_ABP(int nPlot, int nSystolic, int nDiastolic)
        {
            ABP_data = content.GetWaveData_ABP(nPlot, nSystolic, nDiastolic);
            ABP_addX = (double)myCanvas2.Width / (double)waveCountMax / ((double)ABP_data.GetLength(0));

            WaveSettingData waveSetting = Setting.Get<WaveSettingData>();
            launch_ABP = new Launch((int)(1000 / waveSetting.Custom.ABP.Speed));
            launch_ABP.OnElapsed += launch_ABP_OnElapsed;
            launch_ABP.Start();
        }
        private void launch_ABP_OnElapsed()
        {
            this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (System.Threading.ThreadStart)delegate ()
            {
                float y = ABP_data[ABP_i];

                polyline2.Points.Add(new Point(ABP_x, y));
                ABP_x += (float)ABP_addX;
                ABP_i++;

                if (ABP_i >= ABP_data.GetLength(0))
                {
                    ABP_i = 0;
                    ABP_waveCount++;
                }
                if (ABP_waveCount >= waveCountMax)
                {
                    ABP_waveCount = 0;
                    ABP_i = 0;
                    ABP_x = 0;
                    polyline2.Points.Clear();
                }
            });
        }


        float[] Pleth_data;
        double Pleth_addX;
        int pleth_i = 0;
        float pleth_x = 0;
        int pleth_waveCount = 0;
        public void Run_PLETH(int plot,int spo2)
        {
           // launch_PLETH.Stop();
            WaveSettingData waveSetting = Setting.Get<WaveSettingData>();
            launch_PLETH = new Launch((int)(1000 / waveSetting.Custom.PLETH.Speed));

            Pleth_data = content.GetWaveData_PLETH(plot, spo2);
            Pleth_addX = (double)myCanvas3.Width / (double)waveCountMax / ((double)Pleth_data.GetLength(0));

            launch_PLETH.OnElapsed += launch_PLETH_OnElapsed;
            launch_PLETH.Start();
        }
        private void launch_PLETH_OnElapsed()
        {
            this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (System.Threading.ThreadStart)delegate ()
            {
                float y = Pleth_data[pleth_i];

                polyline3.Points.Add(new Point(pleth_x, y));
                pleth_x += (float)Pleth_addX;
                pleth_i++;

                if (pleth_i >= Pleth_data.GetLength(0))
                {
                    pleth_i = 0;
                    pleth_waveCount++;
                }
                if (pleth_waveCount >= waveCountMax)
                {
                    pleth_waveCount = 0;
                    pleth_i = 0;
                    pleth_x = 0;
                    polyline3.Points.Clear();
                }
            });
        }


        float[] RESP_data;
        double RESP_addX;
        int RESP_i = 0;
        float RESP_x = 0;
        int RESP_waveCount = 0;
        public void Run_RESP(RespType respType,int plot, int respRate,int capacity,int respRatio)
        {
            // launch_PLETH.Stop();
            WaveSettingData waveSetting = Setting.Get<WaveSettingData>();
            launch_RESP = new Launch((int)(1000 / waveSetting.Custom.CO2.Speed));

            RESP_data = content.GetWaveData_RESP(respType,plot,respRate,capacity, respRatio);
            RESP_addX = (double)myCanvas4.Width / (double)waveCountMax / ((double)RESP_data.GetLength(0));

            launch_RESP.OnElapsed += launch_RESP_OnElapsed;
            launch_RESP.Start();
        }
        private void launch_RESP_OnElapsed()
        {
            this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (System.Threading.ThreadStart)delegate ()
            {
                float y = RESP_data[RESP_i];

                polyline4.Points.Add(new Point(RESP_x, y));
                RESP_x += (float)RESP_addX;
                RESP_i++;

                if (RESP_i >= RESP_data.GetLength(0))
                {
                    RESP_i = 0;
                    RESP_waveCount++;
                }
                if (RESP_waveCount >= waveCountMax)
                {
                    RESP_waveCount = 0;
                    RESP_i = 0;
                    RESP_x = 0;
                    polyline4.Points.Clear();
                }
            });
        }



    }
}
