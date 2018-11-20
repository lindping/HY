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

        public static List<ListItem> MainWaveCategories = new List<ListItem>()
        {
            new ListItem(){ Text="次要心电图导联", Value="次要心电图导联" },
            new ListItem(){ Text="动脉血压", Value="动脉血压" },
            new ListItem(){ Text="二氧化碳", Value="二氧化碳" },
            new ListItem(){ Text="肺动脉压", Value="肺动脉压" },
            new ListItem(){ Text="没有", Value="没有" },
            new ListItem(){ Text="血氧饱和度", Value="血氧饱和度" },
            new ListItem(){ Text="中心静脉压", Value="中心静脉压" },
            new ListItem(){ Text="主要心电图导联", Value="主要心电图导联" }
        };

        public static List<ListItem> otherWaveCategories = new List<ListItem>()
      {
         new ListItem(){ Text="PH",          Value="PH" },
         new ListItem(){ Text="PTC",         Value="PTC" },
         new ListItem(){ Text="TOF",         Value="TOF" },
         new ListItem(){ Text="颅内压",      Value="颅内压" },
         new ListItem(){ Text="麻醉剂",      Value="麻醉剂" },
         new ListItem(){ Text="脉搏",        Value="脉搏" },
         new ListItem(){ Text="没有",        Value="没有" },
         new ListItem(){ Text="气道呼吸率",  Value="气道呼吸率" },
         new ListItem(){ Text="笑气",        Value="笑气" },
         new ListItem(){ Text="心输出量",    Value="心输出量" },
         new ListItem(){ Text="血温",        Value="血温" },
         new ListItem(){ Text="血氧饱和度",  Value="血氧饱和度" },
         new ListItem(){ Text="氧气",        Value="氧气" },
         new ListItem(){ Text="周围温度",    Value="周围温度" },
      };

        public static List<ListItem> NIBPWaveCategories = new List<ListItem>()
        {
            new ListItem() { Text="没有",Value="没有" },
            new ListItem() { Text="无创血压",Value="无创血压" }
        };

    }

    public enum Sex
    {
        男 = 0,
        女 = 1
    }



}
