using HYS.Library;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YH.Virtual_ECG_Monitor
{
    public class Setting
    {
        public static ECGSettingModel GetSetting()
        {
            ECGSettingModel settingModel = null;
            string json = JsonHelper.ReadFileJson(Constants.ECGSettingJsonFilePath);
            if (!string.IsNullOrWhiteSpace(json))
            {
                settingModel = JsonConvert.DeserializeObject<ECGSettingModel>(json);
            }
            else
            {
                settingModel = new ECGSettingModel()
                {
                    ECGSwitch = true,
                    Lead = 1,
                    Gain = 10,
                    Level = "低",
                    Max = 130,
                    Min = 50,
                    QRSVolumn = 8,
                    Speed = 50,
                    Warning = false,
                };
            }
            return settingModel;
        }

        public static bool SaveSetting(ECGSettingModel settingModel)
        {
            if (settingModel != null)
            {
                try
                {
                    JsonSerializerSettings setting = new JsonSerializerSettings();
                    string json = JsonConvert.SerializeObject(settingModel);
                    JsonHelper.SaveFileJson(json, Constants.ECGSettingJsonFilePath);
                    return true;
                } catch
                {
                    return false;
                }
            }
            return false;
        }
    }
}
