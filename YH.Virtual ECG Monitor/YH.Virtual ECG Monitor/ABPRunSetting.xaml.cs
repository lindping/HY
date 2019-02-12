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

            txtSystolic.Value = ABP_Paras.Systolic;
            txtDiastolic.Value = ABP_Paras.Diastolic;
             int  selectIndex= cbPlot.Items.IndexOf(abp_Paras.Plot);
            if (selectIndex >= 0)
            {
                cbPlot.SelectedIndex = selectIndex;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {            
            ABP_Paras = new ABP_Paras()
            {
                Systolic = txtSystolic.Value,
                Diastolic = txtDiastolic.Value,
                Plot = cbPlot.SelectedItem==null?200: Convert.ToInt32(((ComboBoxItem)cbPlot.SelectedItem).Content)
            };
            this.DialogResult = true;
            Close();
        }
    }
}
