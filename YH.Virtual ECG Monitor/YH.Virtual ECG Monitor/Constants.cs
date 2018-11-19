using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YH.Virtual_ECG_Monitor
{
   public class Constants
    {
       // public const string ECGSettingJsonFilePath = "SettingData\\ECGSetting.json";
      //  public const string PatientInfoJsonFilePath = "SettingData\\PatientInfo.json";
        public const string GeneralWaveFile = "sound\\du.wav";
        public const string AlarmWaveFile = "sound\\du2.wav";
    }

    public enum Sex
    {
        男 = 0,
        女 = 1
    }
}
