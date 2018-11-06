using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YH.Virtual_Simulator_Engine
{
    public class GlobalFuncs
    {
        public static void LoadLanguage()
        {
            GlobalLabel.FontName = YH.Language.LanguageExtend.GetLanguageName("fFontName");                     //字体
            GlobalLabel.ProgramFullName = YH.Language.LanguageExtend.GetLanguageName("fProgramFullName");       //程序名称
            GlobalLabel.ProgramShortName = YH.Language.LanguageExtend.GetLanguageName("fProgramShortName");     //程序简称
            GlobalLabel.CopyrightAll = YH.Language.LanguageExtend.GetLanguageName("fCopyRightAll");             //版权所有
            GlobalLabel.CompanyName = YH.Language.LanguageExtend.GetLanguageName("fCompany");                   //公司名称

            GlobalLabel.Language = YH.Language.LanguageExtend.GetLanguageName("fLanguage");                     // "语言";
        }
    }
}
