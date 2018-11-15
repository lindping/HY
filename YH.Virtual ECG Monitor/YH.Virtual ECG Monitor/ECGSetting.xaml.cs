using HYS.Library;
using Newtonsoft.Json;
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
using YH.Theme;

namespace YH.Virtual_ECG_Monitor
{
    /// <summary>
    /// ECGSetting.xaml 的交互逻辑
    /// </summary>
    public partial class ECGSetting : Window
    {
        public ECGSettingModel settingModel;
        public ECGSetting()
        {
            InitializeComponent();
            InitializeData();
        }
        private void InitializeData()
        {
            string json = JsonHelper.ReadFileJson("ECGGetting.json");
            if (!string.IsNullOrWhiteSpace(json))
            {
                settingModel = JsonConvert.DeserializeObject<ECGSettingModel>(json);               
            }
            DataContext = settingModel;
        }

        //关闭窗口
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        //滑块控制
        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (settingModel.Min >= settingModel.Max)
            {
                MessageBox.Show("低限不能大于等于高限");
                return;
            }
            JsonSerializerSettings setting = new JsonSerializerSettings();         
            string json= JsonConvert.SerializeObject(settingModel);
            JsonHelper.SaveFileJson(json, "ECGGetting.json");
            this.Close();
        }
    }

    public class ObjectToBoolConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return parameter.Equals(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool isChecked = (bool)value;
            if (!isChecked)
            {
                return null;
            }
            return parameter;
        }
    }

  }