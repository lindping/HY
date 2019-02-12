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

            int selectIndex = cbPlot.Items.IndexOf(resp_Paras.Plot);
            if (selectIndex >= 0)
            {
                cbPlot.SelectedIndex = selectIndex;
            }
       //     cbPlot.SelectedValue = resp_Paras.Plot;
            cbType.SelectedIndex = (int)resp_Paras.RespType;

            txtRespRate.Value = resp_Paras.RespRate;
            txtCapacity.Value = resp_Paras.Capacity;
            txtRespRatio.Value = resp_Paras.RespRatio;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int respRate= txtRespRate.Value;
            int capacity= txtCapacity.Value;
            int respRation= txtRespRatio.Value;          

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
         
        }
    }
}
