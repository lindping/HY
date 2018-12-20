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
    public partial class SettingDialog : Window
    {
        TextBox tb;
        int index;
        VirtualManAttributeData model;
        int[,] validateRange = new int[,] {
            { 1,32},
            { 1,32},
            { 0,100},
            { 0,999},
            { 0,45},
            { 0,45},
            { 0,100},
            { 0,12},
            { 0,150},
            { 0,40},
            { 0,999},
            { 0,999}
        };
        public SettingDialog(int i)
        {
            InitializeComponent();
            model = Setting.Get<VirtualManAttributeData>();
            if (model == null)
            {
                model = new VirtualManAttributeData();
                model.Default = new VirtualManAttributeModel()
                {
                    HeartRate = 16,
                    PulseRate = 16,
                    SPO2 = 50,
                    IBP = 99,
                    Temp1 = 20,
                    Temp2 = 20,
                    PAP = 50,
                    CO = 6,
                    ETCO2 = 75,
                    RESP = 20,
                    N2O = 99,
                    O2 = 99
                };
            }
           
            index = i;
            int sum = 12;
            if (i <= 0)
            {
                grid.Children.RemoveRange(2, sum - 1);
                grid.RowDefinitions.RemoveRange(2, sum - 1);
            }
            else if (i >= sum)
            {
                grid.Children.RemoveRange(1, sum - 2);
                grid.RowDefinitions.RemoveRange(1, sum - 2);
            }
            else
            {
                grid.Children.RemoveRange(1, i);
                grid.RowDefinitions.RemoveRange(1, i - 1);

                grid.Children.RemoveRange(2, sum - i - 1);
                grid.RowDefinitions.RemoveRange(2, sum - i);
            }

            Grid.SetRow(grid.Children[1], 1);
            Height = 200;

            tb = VisualTreeHelper_ex.FindVisualChildren<TextBox>(grid.Children[1])[0];
            tb.Text = model.Custom[index].ToString();
            tb.Focus();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string value = tb.Text.Trim();
            if (!ValidateInteger(value, validateRange[index,0], validateRange[index,1]))
            {
                MessageBox.Show("填写数据不合规范");
                return;
            }
            model.Custom[index] = int.Parse(value);
            Setting.Save(model);
            this.Close();
                
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
