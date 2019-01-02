using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
    /// OtherWave.xaml 的交互逻辑
    /// </summary>
    public partial class OtherWave : Window
    {
        public OtherWave()
        {
            InitializeComponent();

        }

        private void InitData()
        {
            string result = "e80b5017098950fc58aad83c8c14978e";
                              //    "E80B5017098950FC58AAD83C8C14978E"
            string str = "abcdef";
            string result2 = BitConverter.ToString(MD5.Create().ComputeHash(Encoding.Default.GetBytes(str))).Replace("-", "");
        }
    }
}
