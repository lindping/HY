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
    /// SetLayoutName.xaml 的交互逻辑
    /// </summary>
    public partial class SetLayoutName : Window
    {
        public List<LayoutSettingModel> Layouts;
        public string LayoutName;
      //  public string InitLayoutName;
        public SetLayoutName(string initLayoutName,List<LayoutSettingModel> layouts)
        {
            InitializeComponent();
            this.tb_layoutName.Text = initLayoutName+"-";
            this.tb_layoutName.Select(tb_layoutName.Text.Length, 0); 
           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string layoutName = tb_layoutName.Text.Trim();
            if (string.IsNullOrWhiteSpace(layoutName))
            {
                MessageBox.Show("版面名称不能为空");
                return;
            }

            if (Layouts.Exists(p => p.Name == layoutName))
            {
                MessageBox.Show("版面名称不能重复");
                return;
            }
            LayoutName = layoutName;
            DialogResult = true;
        }
    }
}
