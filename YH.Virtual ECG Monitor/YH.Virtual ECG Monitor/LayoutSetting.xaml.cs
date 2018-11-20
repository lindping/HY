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
    /// LayoutSetting.xaml 的交互逻辑
    /// </summary>
    public partial class LayoutSetting : Window
    {
        List<ListItem> waveCategories;
        LayoutSettingData model;
        public LayoutSetting()
        {
            InitializeComponent();
            model = Setting.Get<LayoutSettingData>();
            // lbWaveCategory.DataContext = waveCategories;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            for (int i = 1; i <=4; i++)
            {
                var grid = this.FindName("grid_" + i.ToString()) as Grid;
                grid.Visibility = button.Tag.ToString() == i.ToString() ? Visibility.Visible : Visibility.Hidden;
            }


        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            string btnText = (button.Content as TextBlock).Text;
            if (btnText.StartsWith("波形"))
            {
                waveCategories = Constants.MainWaveCategories;
            }
            else if (btnText.StartsWith("视窗"))
            {
                waveCategories = Constants.otherWaveCategories;
            }
            else
            {
                waveCategories = Constants.NIBPWaveCategories;
            }

            lbWaveCategory.ItemsSource = waveCategories;
            lbWaveCategory.SelectedValue = model.Layouts[0].MainWaveCategories[1];
        }
    }
}
