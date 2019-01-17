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
        }

        private void InitShow()
        {
            LayoutSettingData setting = Setting.Get<LayoutSettingData>();
          //  setting.Layouts[0].MainWaveCategories;
        }

    }
}
