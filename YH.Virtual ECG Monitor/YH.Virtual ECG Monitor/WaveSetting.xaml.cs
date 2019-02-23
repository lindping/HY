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

            switch (type)
            {
                case 0:
                    item = data.Custom.PLETH; break;
                case 1:
                    item = data.Custom.ABP; break;
                case 2:
                    item = data.Custom.PAP; break;
                case 3:
                    item = data.Custom.RESP; break;
            }
            setShow(item);
        }

        private void setShow(WaveSettingItem item)
        {
            foreach (var child in rb_gain.Children)
            {
                if (child is RadioButton)
                {
                    RadioButton rb = child as RadioButton;
                    rb.IsChecked = rb.Content.ToString() == item.Gain.ToString();
                }
            }
            foreach (var child in rb_speed.Children)
            {
                if (child is RadioButton)
                {
                    RadioButton rb = child as RadioButton;
                    rb.IsChecked = rb.Content.ToString() == item.Speed.ToString();
                }
            }

            foreach (var child in rb_warnSwitch.Children)
            {
                if (child is RadioButton)
                {
                    RadioButton rb = child as RadioButton;
                    rb.IsChecked = (rb.Content.ToString() == "开" && item.WarnSwitch) || (rb.Content.ToString() != "开" && !item.WarnSwitch);
                }
            }

            foreach (var child in rb_warnLevel.Children)
            {
                if (child is RadioButton)
                {
                    RadioButton rb = child as RadioButton;
                    rb.IsChecked = (rb.Content.ToString() == "高" && item.WarnLevel == 3)
                                          || (rb.Content.ToString() == "中" && item.WarnLevel == 2)
                                          || (rb.Content.ToString() == "低" && item.WarnLevel == 1);
                }
            }

            if (item == data.Custom.ABP)
            {
                tb_warn_0.Text = "收缩压报警限定";

                tb_warn_1.Text = "舒张压报警限定";
                tb_warn_1.Visibility = Visibility.Visible;
                sp_warn_1.Visibility = Visibility.Visible;

                tb_warn_2.Text = "平均压报警限定";
                tb_warn_2.Visibility = Visibility.Visible;
                sp_warn_2.Visibility = Visibility.Visible;


                nud_hignLimit_0.MaxValue = 140;
                nud_hignLimit_0.MinValue = 80;
                nud_hignLimit_0.Value = item.WarnRanges[0].Max;
                nud_hignLimit_0.Increment = 10;
                nud_lowLimit_0.MaxValue = 140;
                nud_lowLimit_0.MinValue = 80;
                nud_lowLimit_0.Increment = 10;
                nud_lowLimit_0.Value = item.WarnRanges[0].Min;


                nud_hignLimit_1.MaxValue = 100;
                nud_hignLimit_1.MinValue = 10;
                nud_hignLimit_1.Increment = 10;
                nud_hignLimit_1.Value = item.WarnRanges[1].Max;
                nud_lowLimit_1.MaxValue = 100;
                nud_lowLimit_1.MinValue = 10;
                nud_lowLimit_1.Increment = 10;
                nud_lowLimit_1.Value = item.WarnRanges[1].Min;

                nud_hignLimit_2.MaxValue = 110;
                nud_hignLimit_2.MinValue = 50;
                nud_hignLimit_2.Increment = 10;
                nud_hignLimit_2.Value = item.WarnRanges[2].Max;
                nud_lowLimit_2.MaxValue = 110;
                nud_lowLimit_2.MinValue = 50;
                nud_lowLimit_2.Increment = 10;
                nud_lowLimit_2.Value = item.WarnRanges[2].Min;
            }
            else if (item == data.Custom.PLETH)
            {
                tb_warn_0.Text = "血氧饱和度报警限定";
                nud_hignLimit_0.MaxValue = 100;
                nud_hignLimit_0.MinValue = 95;
                nud_hignLimit_0.Increment = 1;
                nud_hignLimit_0.Value = item.WarnRanges[0].Max;
                nud_lowLimit_0.MaxValue = 97;
                nud_lowLimit_0.MinValue = 92;
                nud_lowLimit_0.Increment = 1;
                nud_lowLimit_0.Value = item.WarnRanges[0].Min;
            }
            else if (item == data.Custom.RESP)
            {
                tb_warn_0.Text = "呼吸率报警限定";
                nud_hignLimit_0.MaxValue = 60;
                nud_hignLimit_0.MinValue = 20;
                nud_hignLimit_0.Increment = 10;
                nud_hignLimit_0.Value = item.WarnRanges[0].Max;
                nud_lowLimit_0.MaxValue = 30;
                nud_lowLimit_0.MinValue = 5;
                nud_lowLimit_0.Increment = 5;
                nud_lowLimit_0.Value = item.WarnRanges[0].Min;
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

            foreach (var child in rb_warnSwitch.Children)
            {
                if (child is RadioButton)
                {
                    RadioButton rb = child as RadioButton;
                    if (rb.IsChecked.Value)
                    {
                        item.WarnSwitch = rb.Content.ToString() == "开";
                        break;
                    }
                }
            }

            foreach (var child in rb_warnLevel.Children)
            {
                if (child is RadioButton)
                {
                    RadioButton rb = child as RadioButton;
                    if (rb.IsChecked.Value)
                    {
                        item.WarnLevel = rb.Content.ToString() == "低" ? 1 : (rb.Content.ToString() == "中" ? 2 : 3);
                        break;
                    }
                }
            }


            if (nud_hignLimit_0.Value <= nud_lowLimit_0.Value)
            {
                MessageBox.Show("限定范围有误，必须从小到大");
                return;
            }


            item.WarnRanges[0].Min = nud_lowLimit_0.Value;
            item.WarnRanges[0].Max = nud_hignLimit_0.Value;

            if (item.WarnRanges.Count == 3)
            {
                if (nud_hignLimit_1.Value <= nud_lowLimit_1.Value || nud_hignLimit_2.Value <= nud_lowLimit_2.Value)
                {
                    MessageBox.Show("限定范围有误，必须从小到大");
                    return;
                }

                item.WarnRanges[1].Min = nud_lowLimit_1.Value;
                item.WarnRanges[1].Max = nud_hignLimit_1.Value;

                item.WarnRanges[2].Min = nud_lowLimit_2.Value;
                item.WarnRanges[2].Max = nud_hignLimit_2.Value;
            }



            Setting.Save(data);
            this.DialogResult = true;
            this.Close();
        }
    }
}
