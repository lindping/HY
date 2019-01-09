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

namespace YH.Virtual_ECG_Monitor
{
    /// <summary>
    /// WaveDisplay.xaml 的交互逻辑
    /// </summary>
    public partial class WaveDisplay : Window
    {
        public WaveDisplay()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           // uc_wave.Run_ECG(Rhythm.Rhythm_01, 60);        
           // uc_wave.Run_ABP(100, 120, 80);
           //uc_wave.Run_PLETH(120, 50);
           // uc_wave.Run_RESP(RespType.Resp_01, 100, 60, 100, 80);

        }    
    }
}
