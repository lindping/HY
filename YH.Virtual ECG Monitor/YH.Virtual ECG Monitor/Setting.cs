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

        public static T Get<T>()
        {
            T t = default(T);
            try
            {
                string json = JsonHelper.ReadFileJson("SettingData\\" + typeof(T).Name + ".json");
                if (!string.IsNullOrWhiteSpace(json))
                {
                    t = JsonConvert.DeserializeObject<T>(json);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return t;
        }

        public static bool Save<T>(T settingModel)
        {
            if (settingModel != null)
            {
                try
                {
                    JsonSerializerSettings setting = new JsonSerializerSettings();
                    string json = JsonConvert.SerializeObject(settingModel);
                    JsonHelper.SaveFileJson(json, "SettingData\\" + typeof(T).Name + ".json");
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            return false;
        }


    }
}
