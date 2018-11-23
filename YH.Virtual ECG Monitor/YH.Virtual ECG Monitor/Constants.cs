using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YH.Virtual_ECG_Monitor
{
    public class Constants
    {
        public const string GeneralWaveFile = "sound\\du.wav";
        public const string AlarmWaveFile = "sound\\du2.wav";

        public static List<LayoutWave> MainWaveCategories = new List<LayoutWave>()
        {
            new LayoutWave(){ Name="次要心电图导联", Status="开"},
            new LayoutWave(){ Name="动脉血压",       Status="开"},
            new LayoutWave(){ Name="二氧化碳",       Status="开"},
            new LayoutWave(){ Name="肺动脉压",       Status="开"},
            new LayoutWave(){ Name="没有",           Status="开"},
            new LayoutWave(){ Name="血氧饱和度",     Status="开"},
            new LayoutWave(){ Name="中心静脉压",     Status="开"},
            new LayoutWave(){ Name="主要心电图导联", Status="开"}
        };

        public static List<LayoutWave> otherWaveCategories = new List<LayoutWave>()
      {
         new LayoutWave(){ Name="PH",          Status="开"},
         new LayoutWave(){ Name="PTC",         Status="开"},
         new LayoutWave(){ Name="TOF",         Status="开"},
         new LayoutWave(){ Name="颅内压",      Status="开"},
         new LayoutWave(){ Name="麻醉剂",      Status="开"},
         new LayoutWave(){ Name="脉搏",        Status="开"},
         new LayoutWave(){ Name="没有",        Status="开"},
         new LayoutWave(){ Name="气道呼吸率",  Status="开"},
         new LayoutWave(){ Name="笑气",        Status="开"},
         new LayoutWave(){ Name="心输出量",    Status="开"},
         new LayoutWave(){ Name="血温",        Status="开"},
         new LayoutWave(){ Name="血氧饱和度",  Status="开"},
         new LayoutWave(){ Name="氧气",        Status="开"},
         new LayoutWave(){ Name="周围温度",    Status="开"}
      };

        public static List<LayoutWave> NIBPWaveCategories = new List<LayoutWave>()
        {
            new LayoutWave() { Name="没有",Status="开" },
            new LayoutWave() { Name="无创血压",Status="开" }
        };

    }

    public enum Sex
    {
        男 = 0,
        女 = 1
    }

    public enum WaveInitStatus
    {
        开=0,
        关=1,
        空=2
    }



}
