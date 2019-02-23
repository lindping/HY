using HYS.Library;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
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
    /// DisplaySetting.xaml 的交互逻辑
    /// </summary>
    public partial class VolumnSetting : Window
    {

        OtherSettingData model;
        Point  point;

        public VolumnSetting()
        {
            InitializeComponent();            
            InitializeData(); 
        }

        public void InitializeData()
        {
            model = Setting.Get<OtherSettingData>();
            point = new Point(model.Custom.AlartVolumn, 1);
            lgb_volumn.EndPoint = point;
        }
        //关闭窗口
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public class VomumnModel
        {
            public Point VolumnPoint ;
            public VomumnModel(double x)
            {
                VolumnPoint = new Point(x, 1);
            }
          
        }

        private void Path_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Path path = sender as Path;
             point = new Point(Convert.ToDouble(path.Tag.ToString()), 1);
            lgb_volumn.EndPoint = point;
            model.Custom.AlartVolumn = (int)point.X;
            Setting.Save(model);
            SetValue(10,1, model.Custom.AlartVolumn);

        }

        #region Windows Media API
        [DllImport("winmm.dll")]
        private static extern long waveOutSetVolume(UInt32 deviceID, UInt32 Volume);
        [DllImport("winmm.dll")]
        private static extern long waveOutGetVolume(UInt32 deviceID, out UInt32 Volume);
        #endregion

        private static void SetValue(int aMaxValue, int aMinValue, int aValue)
        {
            //先把trackbar的value值映射到0x0000～0xFFFF范围  
            UInt32 Value = (UInt32)((double)0xffff * (double)aValue / (double)(aMaxValue - aMinValue));
            //限制value的取值范围  
            if (Value < 0) Value = 0;
            if (Value > 0xffff) Value = 0xffff;
            UInt32 left = (UInt32)Value;//左声道音量  
            UInt32 right = (UInt32)Value;//右  
            waveOutSetVolume(0, left << 16 | right); //"<<"左移，“|”逻辑或运算  
        }

    }



}
