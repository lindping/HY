using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YH.Language
{
    public class LanguageExtend
    {
        public static string[] Array_LanguageCode = new string[] { "zh-CN", "en-US" };            // "zh-CN";//"en-US, "ru-RU"
        public static string[] Array_LanguageName = new string[] { "中文(中国)", "English(US)" };  //语言列表, "Russian(Russia)"  

        public static string strPathFile;
        public static string LanguCode = "zh-CN";
        public static string FontName = "宋体";
        public static string LuanguPathName = "";        //语言路径名称
        public static string LuanguFileName = "";        //语言文件名称
        public static LanguageBase XMLLangu = new LanguageBase();
        public static SortedList<string, string> languagelist = new SortedList<string, string>();



        public static void SetLanguage(string strPathFile)
        {
            XMLLangu = new LanguageBase();
            //获取语言列表项
            LuanguPathName = strPathFile;
            languagelist = XMLLangu.getDirXml(LuanguPathName);
        }

        public static void SetLanguageCode(string strLanguCode)
        {
            LanguCode = strLanguCode;
            GetLanguageFileName();
            XMLLangu.Load(LuanguFileName);
            FontName = GetLanguageName("ffontname");
        }

        public static void GetLanguageFileName()
        {
            if (languagelist != null)
            {
                for (int i = 0; i < languagelist.Count; i++)
                {
                    string strKey = languagelist.Keys[i].Trim();
                    string localLanguCode = strKey.Substring(0, strKey.IndexOf("("));
                    if (localLanguCode.ToUpper() == LanguCode.ToUpper())
                    {
                        LuanguFileName = languagelist.Values[i];
                        return;
                    }
                }
            }
        }

        public static string GetLanguageName(string strFID)
        {
            return XMLLangu.getElementValue(strFID);
        }


        /// <summary>
        /// 语言代码检索语言名称
        /// </summary>
        /// <param name="LuanguageCode">语言代码</param>
        /// <returns>返回语言名称</returns>
        public static string LanguageNameOfIndex(string LuanguageCode)
        {
            if (LuanguageCode == Array_LanguageCode[0])
            {
                return Array_LanguageName[0];
            }

            if (LuanguageCode == Array_LanguageCode[1])
            {
                return Array_LanguageName[1];
            }

            return null;
        }

        /// <summary>
        /// 语言名称检索语言代码
        /// </summary>
        /// <param name="LuanguageName">语言名称</param>
        /// <returns>返回语言代码</returns>
        public static string LanguageCodeOfIndex(string LuanguageName)
        {
            if (LuanguageName == Array_LanguageName[0])
            {
                return Array_LanguageCode[0];
            }

            if (LuanguageName == Array_LanguageName[1])
            {
                return Array_LanguageCode[1];
            }

            return null;
        }
    }
}
