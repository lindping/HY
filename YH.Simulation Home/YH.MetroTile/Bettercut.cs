using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YH.MetroTile
{
    public class Bettercut
    {
        public Dictionary<string, string[]> BettercutDi = new Dictionary<string, string[]> { };

        public Dictionary<string, string[]> GetBettercutDi()
        {

            AddToDictionary("1.模拟人教学程序", "Application", "", "");
            AddToDictionary("2.模拟心电监护仪", "Application", "", "");
            AddToDictionary("3.评估系统查看器", "Application", "", "");
            AddToDictionary("4.模拟语音系统", "Application", "", "");
            AddToDictionary("5.摄像监控系统", "Application", "", "");


            AddToDictionary("6.病例编辑", "Editor", "", "");
            AddToDictionary("7.趋势编辑", "Editor", "", "");
            AddToDictionary("8.处理措施项编辑", "Editor", "", "");
            AddToDictionary("9.声音编辑", "Editor", "", "");
            AddToDictionary("10.内容转换器", "Editor", "", "");

            AddToDictionary("11.语言设置", "Setup", "", "");
            AddToDictionary("12.模拟人硬件", "Setup", "", "");
            AddToDictionary("13.文件路径设置", "Setup", "", "");
            AddToDictionary("14.配置文件设置", "Setup", "", "");
            AddToDictionary("15.连接设置", "Setup", "", "");
            AddToDictionary("16.自动启动设置", "Setup", "", "");

            AddToDictionary("17.快操手册", "Help", "", "");
            AddToDictionary("18.使用说明", "Help", "", "");
            AddToDictionary("19.许可证", "Help", "", "");
            AddToDictionary("20.更新", "Help", "", "");
            AddToDictionary("21.网站", "Help", "", "");
            AddToDictionary("22.资源", "Help", "", "");


            return BettercutDi;
        }
        public void AddToDictionary(string displayName, string catetory,string tileIconPath, string exePath)
        {
            if (!BettercutDi.ContainsKey(displayName))
                BettercutDi.Add(displayName, new string[] { catetory, tileIconPath, exePath });
        }
    }
}
