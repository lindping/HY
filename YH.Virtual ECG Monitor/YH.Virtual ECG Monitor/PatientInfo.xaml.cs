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
    /// PatientInfo.xaml 的交互逻辑
    /// </summary>
    public partial class PatientInfo : Window
    {
        PatientInfoData model;
        public PatientInfo()
        {
            InitializeComponent();
            InitializeData();
        }

        public void InitializeData()
        {
            model = Setting.Get<PatientInfoData>();
            if (model == null)
            {
                model = new PatientInfoData()
                {
                    Default = new PatientInfoModel()
                    {
                        Age = 28,
                        BedNo = "YH001",
                        MedRecNo = "YHCC201812150008",
                        Name = "王小明",
                        Sex = Sex.女
                    }
                };
            }
            DataContext = model.Custom;
        }

        //关闭窗口
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(model.Custom.Name))
            {
                MessageBox.Show("姓名不能为空");
                return;
            }

            int age;
            if (!int.TryParse(tbAge.Text, out age) || age < 1)
            {
                MessageBox.Show("年龄不正确");
            }
            if (string.IsNullOrWhiteSpace(model.Custom.MedRecNo))
            {
                MessageBox.Show("病历号不能为空");
                return;
            }
            if (string.IsNullOrWhiteSpace(model.Custom.BedNo))
            {
                MessageBox.Show("床位号不能为空");
                return;
            }

            Setting.Save(model);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            model.Custom = (PatientInfoModel)model.Default.Clone();
            Setting.Save(model);
            DataContext = model.Custom;
        }
    }
}
