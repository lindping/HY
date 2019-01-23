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
    /// PLETHRunSetting.xaml 的交互逻辑
    /// </summary>
    public partial class PLETHRunSetting : Window
    {

        public PLETH_Paras PLETH_Paras { get; set; }
        public PLETHRunSetting(PLETH_Paras pleth_Paras)
        {
            InitializeComponent();
            PLETH_Paras = pleth_Paras;


            txtSpo2.Text = PLETH_Paras.Spo2.ToString();


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int spo2;        
            if (!int.TryParse(txtSpo2.Text, out spo2))
            {
                MessageBox.Show("舒张压填写不正确");
                return;
            }


            PLETH_Paras = new PLETH_Paras()
            {
                Spo2 = spo2,
                Plot = cbPlot.SelectedItem == null ? 200 : Convert.ToInt32(((ComboBoxItem)cbPlot.SelectedItem).Content)
            };
            this.DialogResult = true;
            Close();
        }
    }
}

