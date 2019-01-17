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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace YH.Virtual_ECG_Monitor
{
    /// <summary>
    /// UserControl_Wave_Base.xaml 的交互逻辑
    /// </summary>
    public partial class UserControl_Wave_Base : UserControl
    {


        public UserControl_Wave_Base()
        {
            InitializeComponent();
        }
    }

    public class ECG_Wave_Paras
    {
        public Rhythm eRhythm { get; set; }
        int nHeartRate { get; set; }
        float baseAMP { get; set; }
    }

    public class  _Wave_Paras
    {
        public Rhythm eRhythm { get; set; }
        int nHeartRate { get; set; }
        float baseAMP { get; set; }
    }
}
