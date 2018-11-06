using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YH.Simulation_Application
{
    static public class PublicFuncs
    {
        public static void LoadLanguage()
        {
            PublicString.FontName = YH.Language.LanguageExtend.GetLanguageName("fFontName");                     //字体
            PublicString.ProgramFullName = YH.Language.LanguageExtend.GetLanguageName("fProgramFullName");       //程序名称
            PublicString.ProgramShortName = YH.Language.LanguageExtend.GetLanguageName("fProgramShortName");     //程序简称
            PublicString.CopyrightAll = YH.Language.LanguageExtend.GetLanguageName("fCopyRightAll");             //版权所有
            PublicString.CompanyName = YH.Language.LanguageExtend.GetLanguageName("fCompany");                   //公司名称

            PublicString.Language = YH.Language.LanguageExtend.GetLanguageName("fLanguage");                     // "语言";
        }
    }
}
