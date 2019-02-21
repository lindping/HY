using HYS.Library;
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
    /// SettingDialog.xaml 的交互逻辑
    /// </summary>
    public partial class VirtualManSetting : Window
    {
       
        int index;
        VirtualManAttributeData model;
        VirtualManAttributeItem item;
        VirtualManAttributeItem[] items;
        public VirtualManSetting(int i)
        {
            InitializeComponent();
            model = Setting.Get<VirtualManAttributeData>();
            if (model == null)
            {
                model = new VirtualManAttributeData();
                model.Default = new VirtualManAttributeModel()
                {
                    HeartRate = new VirtualManAttributeItem() { Value = 80, Description = "心率 HEART RATE", Name = "HeartRate", MaxValue = 300, MinValue = 0 , unit= "BPM" },
                    PulseRate = new VirtualManAttributeItem() { Value = 16, Description = "脉搏频率 PULSE RATE", Name = "PulseRate", MaxValue = 32, MinValue = 1, unit = "BPM" },//16,
                    SPO2 = new VirtualManAttributeItem() { Value = 50, Description = "血氧饱和度 SPO2", Name = "SPO2", MaxValue = 100, MinValue = 0  , unit = "%" },// 50,
                    IBP = new VirtualManAttributeItem[] { new VirtualManAttributeItem { Value = 120, Description = "有创血压_收缩压  IBP_Systolic", Name = "IBP_Systolic", MaxValue = 300, MinValue = 0, unit = "mmHg" }, new VirtualManAttributeItem { Value = 80, Description = "有创血压_舒张压  IBP_Diastolic", Name = "IBP_Diastolic", MaxValue = 200, MinValue = 0, unit = "mmHg" } },//120,80,
                    NIBP = new VirtualManAttributeItem[] { new VirtualManAttributeItem { Value = 120, Description = "无创血压_收缩压  NIBP_Systolic", Name = "NIBP_Systolic", MaxValue = 300, MinValue = 0, unit = "mmHg" }, new VirtualManAttributeItem { Value = 80, Description = "无创血压_舒张压  NIBP_Diastolic", Name = "NIBP_Diastolic", MaxValue = 200, MinValue = 0, unit = "mmHg" } },//120,80,
                    Temp1 = new VirtualManAttributeItem() { Value = 22, Description = "周围温度 Temp1", Name = "Temp1", MaxValue = 45, MinValue = 0, unit = "℃" },//20,
                    Temp2 = new VirtualManAttributeItem() { Value = 22, Description = "血液温度 Temp2", Name = "Temp2", MaxValue = 45, MinValue = 0, unit = "℃" },//20,
                    PAP = new VirtualManAttributeItem() { Value = 50, Description = "肺动脉压 PAP", Name = "PAP", MaxValue = 100, MinValue = 0, unit = "mmHg" },//50,
                    CO = new VirtualManAttributeItem() { Value = 6, Description = "心输出量 C.O.", Name = "CO", MaxValue = 12, MinValue = 0, unit = "L/min" },//6,
                    ETCO2 = new VirtualManAttributeItem() { Value = 75, Description = "呼气末二氧化碳 ETCO2", Name = "ETCO2", MaxValue = 150, MinValue = 0, unit = "mmHg" },// 75,
                    RESP = new VirtualManAttributeItem() { Value = 20, Description = "呼吸率 RESP", Name = "RESP", MaxValue = 40, MinValue = 0, unit = "RPM" },//20,
                    N2O = new VirtualManAttributeItem() { Value = 99, Description = "AG麻醉气体一氧化二氮 N2O", Name = "N2O", MaxValue = 999, MinValue = 0, unit = "%" },// 99,
                    O2 = new VirtualManAttributeItem() { Value = 99, Description = "AG麻醉气体 O2", Name = "O2", MaxValue = 999, MinValue = 0, unit = "%" } //99
                };
            }
           
            index = i;
            if (index == 5 || index == 6)
            {
                items = index == 5 ? model.Custom.IBP : model.Custom.NIBP;
                (sp_para.Children[2] as TextBlock).Text = items[0].MinValue.ToString() + "-" + items[0].MaxValue.ToString() + " " + items[0].unit;
                sp_para.DataContext = items[0];              
                BuildControl(items[1]);             
            }
            else
            {
               item = model.Custom[index];             
               (sp_para.Children[2] as TextBlock).Text = item.MinValue.ToString() + "-" + item.MaxValue.ToString() + " " + item.unit;
               sp_para.DataContext = item;
            }
          // (sp_para.Children[1] as TextBox).Focus();
        }

        public T CloneControl<T>(T control) where T : DependencyObject, new()
        {
            string xaml = System.Windows.Markup.XamlWriter.Save(control);
            T rtb2 = System.Windows.Markup.XamlReader.Parse(xaml) as T;
            return rtb2;
        }

        private void BuildControl(VirtualManAttributeItem item)
        {
            StackPanel panel = CloneControl<StackPanel>(sp_para);
            panel.Name = "sp_para_2";
            sp_container.Children.Add(panel);

            Binding valueBinding = new Binding();
            valueBinding.Mode = BindingMode.TwoWay;
            valueBinding.Path = new PropertyPath("Value");
            TextBox tb = panel.Children[1] as TextBox;
            tb.Width = 100;
            tb.Height = 30;
            tb.Margin = new Thickness(0,5,0,5);
            tb.VerticalContentAlignment = VerticalAlignment.Center;
            tb.SetBinding(TextBox.TextProperty, valueBinding);
            (panel.Children[0] as TextBlock).Text = item.Description;
            (panel.Children[2] as TextBlock).Text = item.MinValue.ToString() + "-" + item.MaxValue.ToString() + " " + item.unit;
            panel.DataContext = item;
               
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string value;

            if (index == 5 || index == 6)
            {
               
                 value = (sp_para.Children[1] as TextBox).Text.Trim();
                if (!ValidateInteger(value, items[0].MinValue, items[0].MaxValue))
                {
                    MessageBox.Show("填写数据不合规范");
                    return;
                }

                value =  ((sp_container.Children[1]  as StackPanel).Children[1] as TextBox).Text.Trim();
                if (!ValidateInteger(value, items[1].MinValue, items[1].MaxValue))
                {
                    MessageBox.Show("填写数据不合规范");
                    return;
                }
            }else
            {

                 value = (sp_para.Children[1] as TextBox).Text.Trim();
                if (!ValidateInteger(value, item.MinValue, item.MaxValue))
                {
                    MessageBox.Show("填写数据不合规范");
                    return;
                }
            }          
            Setting.Save(model);
            this.Close();                
        }

        private bool ValidateTextBox(TextBox tb, VirtualManAttributeItem item)
        {
          string  value = tb.Text.Trim();
            if (!ValidateInteger(value, item.MinValue, item.MaxValue))
            {
                MessageBox.Show("填写数据不合规范");
                return false;
            }
            return true;
        }

        private bool ValidateInteger(string value, int min, int max)
        {
            int outValue;
            if (int.TryParse(value, out outValue))
            {
                if (outValue >= min && outValue <= max)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
