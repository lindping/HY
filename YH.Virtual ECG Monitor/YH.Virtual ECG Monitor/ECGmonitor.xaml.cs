using HYS.Library;
using Microsoft.Research.DynamicDataDisplay;
using Microsoft.Research.DynamicDataDisplay.DataSources;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using YH.ECGMonitor.WaveData.RESPWaveData;

namespace YH.Virtual_ECG_Monitor
{
    /// <summary>
    /// ECGmonitor.xaml 的交互逻辑
    /// </summary>
    public partial class ECGmonitor : Window
    {
        Launch launch1;
        public ECGmonitor()
        {
            InitializeComponent();

            launch1 = new Launch(4);
            launch1.OnElapsed += Launch1_OnElapsed;
            //launch1.CreateWork();
            launch1.Start();

        }

        private void Launch1_OnElapsed()
        {
            throw new NotImplementedException();
        }
    }


}

