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
using YH.Theme;

namespace YH.Virtual_ECG_Monitor
{
    /// <summary>
    /// ECGSetting.xaml 的交互逻辑
    /// </summary>
    public partial class ECGSetting : Window
    {
        public ECGSetting()
        {
            InitializeComponent();
            Application.Current.Resources.MergedDictionaries[1] = new ResourceDictionary()
            {
                Source = new Uri("Dictionary/Skin.RegularStyle.xaml", UriKind.Relative)
            };
        }
        //关闭窗口
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        //滑块控制
        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }
    }
}
