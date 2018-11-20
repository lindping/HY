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
    /// DisplaySetting.xaml 的交互逻辑
    /// </summary>
    public partial class DisplaySetting : Window
    {
        SolidColorBrush selected = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#0089e1"));
        SolidColorBrush unSelected = Brushes.Transparent;

        OtherSettingData model;
        public DisplaySetting()
        {
            InitializeComponent();
            InitializeData();
        }

        public void InitializeData()
        {
            model = Setting.Get<OtherSettingData>();
            foreach (var element in btnGroup.Children)
            {
                if (element is Button)
                {
                    Button button = element as Button;
                    if (button.Tag.ToString() == model.Custom.DisplayWaveNumber.ToString())
                    {
                        button.Background = selected;
                        break;
                    }
                    
                }
            }
        }
        //关闭窗口
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            foreach (var element in btnGroup.Children)
            {
                if (element is Button)
                {
                    Button button = element as Button;
                    if (element == sender)
                    {

                        if (button.Background != selected)
                        {
                            button.Background = selected;
                        }
                        model.Custom.DisplayWaveNumber = Convert.ToInt32(button.Tag);

                    }
                    else
                    {
                        if (button.Background == selected)
                        {
                            button.Background = unSelected;
                        }
                    }
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Setting.Save(model);
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            LayoutSetting frm = new Virtual_ECG_Monitor.LayoutSetting();
            frm.Show();

        }
    }
}
