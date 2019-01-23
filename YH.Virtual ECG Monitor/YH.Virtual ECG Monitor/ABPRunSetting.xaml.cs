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
using YH.Simulator.Framework.DataDictionary;

namespace YH.Virtual_ECG_Monitor
{
    /// <summary>
    /// ECGRunSetting.xaml 的交互逻辑
    /// </summary>
    public partial class ABPRunSetting : Window
    {

        public ABP_Paras ABP_Paras { get; set; }
        public ABPRunSetting(ABP_Paras abp_Paras)
        {
            InitializeComponent();
            ABP_Paras = abp_Paras;

            txtSystolic.Text = ABP_Paras.Systolic.ToString();
            txtDiastolic.Text = ABP_Paras.Diastolic.ToString();
            cbPlot.SelectedValue = ABP_Paras.Plot;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int systolic;
            int diastolic;
            if (!int.TryParse(txtSystolic.Text, out systolic))
            {
                MessageBox.Show("舒张压填写不正确");
                return;
            }

            if (!int.TryParse(txtDiastolic.Text, out diastolic))
            {
                MessageBox.Show("收缩压填写不正确");
                return;
            }
            ABP_Paras = new ABP_Paras()
            {
                Systolic = systolic,
                Diastolic = diastolic,
                Plot = cbPlot.SelectedItem==null?200: Convert.ToInt32(((ComboBoxItem)cbPlot.SelectedItem).Content)
            };
            this.DialogResult = true;
            Close();
        }
    }
}
