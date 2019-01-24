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
    public partial class RESPRunSetting : Window
    {

        public Resp_Paras Resp_Paras { get; set; }
        public RESPRunSetting(Resp_Paras resp_Paras)
        {

            InitializeComponent();
            Resp_Paras = resp_Paras;

            cbPlot.SelectedValue = resp_Paras.Plot;
            cbType.SelectedIndex = (int)resp_Paras.RespType;

            txtRespRate.Text = resp_Paras.RespRate.ToString();
            txtCapacity.Text = resp_Paras.Capacity.ToString();
            txtRespRatio.Text = resp_Paras.RespRatio.ToString();


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int respRate;
            int capacity;
            int respRation;

            if (!int.TryParse(txtRespRate.Text, out respRate))
            {
                MessageBox.Show("呼吸率填写不正确");
                return;
            }
            if (!int.TryParse(txtCapacity.Text, out capacity))
            {
                MessageBox.Show("吸气量填写不正确");
                return;
            }
            if (!int.TryParse(txtRespRatio.Text, out respRation))
            {
                MessageBox.Show("吸气比填写不正确");
                return;
            }

            RespType respType = (RespType)cbType.SelectedIndex;
            int plot = cbPlot.SelectedItem==null?200:  Convert.ToInt32(((ComboBoxItem)cbPlot.SelectedItem).Content);


            Resp_Paras = new Resp_Paras
            {
                Capacity = capacity,
                Plot = plot,
                RespRate = respRate,
                RespRatio = respRation,
                RespType = respType
            };
            this.DialogResult = true;
            Close();
            //ABP_Paras = new ABP_Paras()

            //int systolic;
            //int diastolic;
            //if (!int.TryParse(txtSystolic.Text, out systolic))
            //{
            //    MessageBox.Show("舒张压填写不正确");
            //    return;
            //}

            //if (!int.TryParse(txtDiastolic.Text, out diastolic))
            //{
            //    MessageBox.Show("收缩压填写不正确");
            //    return;
            //}
            //ABP_Paras = new ABP_Paras()
            //{
            //    Systolic = systolic,
            //    Diastolic = diastolic,
            //    Plot = cbPlot.SelectedItem==null?200: Convert.ToInt32(((ComboBoxItem)cbPlot.SelectedItem).Content)
            //};
            //this.DialogResult = true;
            //Close();
        }
    }
}
