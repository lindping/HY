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
    /// Main.xaml 的交互逻辑
    /// </summary>
    public partial class Main : Window
    {
        public Main()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (sender.GetType() != typeof(Button))
                return;
            Button button = sender as Button;

            string number = button.Content.ToString().Substring(0,4).Trim();
            switch(number)
            {
                case "1.1": 
                    PatientInfo patientInfo = new PatientInfo();
                    patientInfo.Show();
                    break;
                case "1.2": break;
                case "1.3":
                    DisplaySetting displaySetting = new DisplaySetting();
                    displaySetting.Show();
                    break;
                case "1.4": 
                    VolumnSetting volumnSetting = new VolumnSetting();
                    volumnSetting.Show();
                        break;
                case "2.1": 
                    ECGSetting frm = new ECGSetting();
                    frm.Show();
                    break;
                case "2.2": break;
                case "2.3": break;
                case "2.4": break;
                case "2.5": break;
                case "3.1": break;
                case "3.2": break;
                case "3.3": break;
                case "3.4": break;
                case "3.5": break;
                case "3.6": break;
                case "3.7": break;
                case "3.8": break;
                case "3.9": break;
                case "3.10":break;
                case "4.1": break;
                case "4.2": break;
                case "5.1": break;
                case "5.2": break;

            }
            

            
        }
    }
}
