using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YH.Simulation_Home
{
    public class PublicFuncs
    {
        public static void LoadLanguage()
        {
            PublicString.FontName = YH.Language.LanguageExtend.GetLanguageName("fFontName");                     //字体
            PublicString.ProgramFullName = YH.Language.LanguageExtend.GetLanguageName("fProgramFullName");       //程序名称
            PublicString.ProgramShortName = YH.Language.LanguageExtend.GetLanguageName("fProgramShortName");     //程序简称
            PublicString.CopyrightAll = YH.Language.LanguageExtend.GetLanguageName("fCopyRightAll");             //版权所有
            PublicString.CompanyName = YH.Language.LanguageExtend.GetLanguageName("fCompany");                   //公司名称

            PublicString.Language = YH.Language.LanguageExtend.GetLanguageName("fLanguage");                     // "语言";

            PublicString.Application = YH.Language.LanguageExtend.GetLanguageName("fApplication");               //"应用";
            PublicString.SimApp = YH.Language.LanguageExtend.GetLanguageName("fSimApp");                         //"模拟应用";
            PublicString.ECGMonitor = YH.Language.LanguageExtend.GetLanguageName("fECGMonitor");                 //"心电监护仪";
            PublicString.EvaluationViewer = YH.Language.LanguageExtend.GetLanguageName("fEvaluationViewer");     //"评估查看器";
            PublicString.VoiceEngine = YH.Language.LanguageExtend.GetLanguageName("fVoiceEngine");               //"模拟语音";
            PublicString.VideoMonitor = YH.Language.LanguageExtend.GetLanguageName("fVideoMonitor");             //"摄像监控";

            PublicString.Editor = YH.Language.LanguageExtend.GetLanguageName("fEditor");                         //"编辑";
            PublicString.CaseEditor = YH.Language.LanguageExtend.GetLanguageName("fCaseEditor");                 //"病例编辑";
            PublicString.TrendEditor = YH.Language.LanguageExtend.GetLanguageName("fTrendEditor");               //"趋势编辑";
            PublicString.TreatmentEditor = YH.Language.LanguageExtend.GetLanguageName("fTreatmentEditor");       //"处理措施项编辑";
            PublicString.SoundEditor = YH.Language.LanguageExtend.GetLanguageName("fSoundEditor");               //"声音编辑";
            PublicString.ContentConverter = YH.Language.LanguageExtend.GetLanguageName("fContentConverter");     //"内容转换器";

            PublicString.Setup = YH.Language.LanguageExtend.GetLanguageName("fSetup");                           //"设置";
            PublicString.LanguageSelector = YH.Language.LanguageExtend.GetLanguageName("fLanguageSelector");     //"语言设置";
            PublicString.SimulatorFirmware = YH.Language.LanguageExtend.GetLanguageName("fSimulatorFirmware");   //"模拟人硬件设置";
            PublicString.FolderEditor = YH.Language.LanguageExtend.GetLanguageName("fFolderEditor");             //"文件路径设置";
            PublicString.ProfileEditor = YH.Language.LanguageExtend.GetLanguageName("fProfileEditor");           //"配置文件设置";
            PublicString.ModelConnector = YH.Language.LanguageExtend.GetLanguageName("fModelConnector");         //"连接设置";
            PublicString.AutoLaunch = YH.Language.LanguageExtend.GetLanguageName("fAutoLaunch");                 //"自动启动设置";

            PublicString.Help = YH.Language.LanguageExtend.GetLanguageName("fHelp");                             //"帮助";
            PublicString.UserQuickGuide = YH.Language.LanguageExtend.GetLanguageName("fUserQuickGuide");         //"快操手册";
            PublicString.UserGuide = YH.Language.LanguageExtend.GetLanguageName("fUserGuide");                   //"使用说明";
            PublicString.Licence = YH.Language.LanguageExtend.GetLanguageName("fLicence");                       //"许可证";
            PublicString.Update = YH.Language.LanguageExtend.GetLanguageName("fUpdate");                         //"更新";
            PublicString.Website = YH.Language.LanguageExtend.GetLanguageName("fWebsite");                       //"网站";
            PublicString.Store = YH.Language.LanguageExtend.GetLanguageName("fStore");                           //"资源";



        }
    }
}
