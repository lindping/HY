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
    /// WaveSetting.xaml 的交互逻辑
    /// </summary>
    public partial class WaveSetting : Window
    {
        int type;
        WaveSettingData data;
        WaveSettingItem item;
        public WaveSetting(int type)
        {
            InitializeComponent();
            this.type = type;
            InitializeData();

        }



        private void InitializeData()
        {
            data = Setting.Get<WaveSettingData>();
            if (data == null)
            {
                data = new WaveSettingData()
                {
                    Default = new WaveSettingModel()
                    {
                        ABP = new WaveSettingItem() { Gain = 10, Speed = 25 },
                        CO2 = new WaveSettingItem() { Gain = 10, Speed = 25 },
                        PAP = new WaveSettingItem() { Gain = 10, Speed = 25 },
                        PLETH = new WaveSettingItem() { Gain = 10, Speed = 25 }
                    }
                    
                };  
            }

            switch (type)
            {
                case 0:
                    item = data.Custom.PLETH;break;
                case 1:
                    item = data.Custom.ABP;   break;
                case 2:
                    item = data.Custom.PAP;  break;
                case 3:
                    item=data.Custom.CO2; break;
            }
            setShow(item.Gain,item.Speed);
        }

        private void setShow(int gain,int speed)
        {
            foreach (var child in rb_gain.Children)
            {
                if (child is RadioButton)
                {
                    RadioButton rb = child as RadioButton;
                    rb.IsChecked = rb.Content.ToString()==gain.ToString();
                }
            }
            foreach (var child in rb_speed.Children)
            {
                if (child is RadioButton)
                {
                    RadioButton rb = child as RadioButton;
                    rb.IsChecked = rb.Content.ToString() == speed.ToString();
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            foreach (var child in rb_gain.Children)
            {
                if (child is RadioButton)
                {
                    RadioButton rb = child as RadioButton;
                    if (rb.IsChecked.Value)
                    {
                        item.Gain = Convert.ToInt32(rb.Content);
                        break;
                    }
                }
            }

            foreach (var child in rb_speed.Children)
            {
                if (child is RadioButton)
                {
                    RadioButton rb = child as RadioButton;
                    if (rb.IsChecked.Value)
                    {
                        item.Speed = Convert.ToInt32(rb.Content);
                        break;
                    }
                }
            }
            //data.Custom.ABP.Speed = item.Speed;
            //data.Custom.CO2.Speed = item.Speed;
            //data.Custom.PAP.Speed = item.Speed;
            //data.Custom.PLETH.Speed = item.Speed;
            Setting.Save(data);
            this.DialogResult = true;
            this.Close();
        }
    }
}
