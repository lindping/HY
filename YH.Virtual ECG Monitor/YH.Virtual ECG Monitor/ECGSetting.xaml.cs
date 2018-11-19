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
        public ECGSettingData settingModel;
        public ECGSetting()
        {
            InitializeComponent();
            InitializeData();
        }
        private void InitializeData()
        {          
            settingModel = Setting.Get<ECGSettingData>();        
            DataContext = settingModel.Custom;
        }

        //关闭窗口
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
     

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (settingModel.Custom.Min >= settingModel.Custom.Max)
            {
                MessageBox.Show("低限不能大于等于高限");
                return;
            }
            Setting.Save(settingModel);
            this.Close();
        } 
     
    }


  }