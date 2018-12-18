using HYS.Library;
using System;
using System.Collections.Generic;
using System.Globalization;
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

        }
    }


   
}
