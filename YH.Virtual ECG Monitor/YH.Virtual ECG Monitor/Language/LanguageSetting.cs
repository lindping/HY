using HYS.Library;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace YH.Virtual_ECG_Monitor
{

    public static class MyContentControl
    {
        /// <summary>
        /// 默认值，表示空值
        /// </summary>
        private static AppLanguage _curLanguage = AppLanguage.Default;
        public static AppLanguage CurLanguage
        {
            get
            {
                if (_curLanguage == AppLanguage.Default)
                {
                    string language = ConfigurationManager.AppSettings["Language"].ToString();  //从配置文件读取当前语言环境
                    _curLanguage = (AppLanguage)Enum.Parse(typeof(AppLanguage), language);
                }
                if (_curLanguage == AppLanguage.Default)   //为空值时使用中文
                {
                    _curLanguage = AppLanguage.Chinese;
                }
                return _curLanguage;
            }
            set
            {
                _curLanguage = value;
            }
        }

        /// <summary>
        /// 获取当前语言对应的资源文件
        /// </summary>
        /// <returns></returns>
        public static List<PageResource> GetPageResource()
        {
            List<PageResource> pages = null;
            try
            {
                string json = JsonHelper.ReadFileJson("Language\\" + CurLanguage.ToString() + ".json");
                if (!string.IsNullOrWhiteSpace(json))
                {
                    pages = JsonConvert.DeserializeObject<List<PageResource>>(json);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return pages;
        }
        /// <summary>
        /// 对当前页面应用内置语言资源
        /// </summary>
        /// <param name="contentControl"></param>
        public static void ApplyLanguage(this ContentControl contentControl)
        {
            ApplyLanguage(contentControl, _curLanguage);
        }

        /// <summary>
        /// 对当前页面应用指定的语言资源
        /// </summary>
        /// <param name="contentControl"></param>
        /// <param name="language"></param>
        public static void ApplyLanguage(this ContentControl contentControl, AppLanguage language)
        {
            CurLanguage = language;
            string pageName = contentControl.GetType().Name;
            List<PageResource> pages = GetPageResource();
            if (pages != null)
            {
                PageResource page = pages.Find(p => p.PageName.ToLower() == pageName.ToLower());
                if (page != null && page.Controls != null)
                {
                    page.Controls.ForEach(p =>
                    {
                        object control = contentControl.FindName(p.ControlName);
                        if (control != null)
                        {
                            if (control is TextBlock)
                            {
                                (control as TextBlock).Text = p.ControlText;
                            }
                            else if (control is Label)
                            {
                                (control as Label).Content = p.ControlText;
                            }
                            else if (control is Button)
                            {
                                (control as Button).Content = p.ControlText;
                            }
                            else if (control is UserControl)
                            {
                                (control as ContentControl).ApplyLanguage(CurLanguage);
                            }
                        }
                    });
                }
            }
        }

    }

    /// <summary>
    /// 页面资源
    /// </summary>
    public class PageResource
    {
        public string PageName { get; set; }
        public List<ControlResoure> Controls { get; set; }
    }

    /// <summary>
    /// 控件资源
    /// </summary>
    public class ControlResoure
    {
        public string ControlName { get; set; }
        public string ControlText { get; set; }
    }

    /// <summary>
    /// 语言枚举
    /// </summary>
    public enum AppLanguage
    {
        /// <summary>
        /// 代表空值
        /// </summary>
        Default = 0,
        /// <summary>
        /// 中文
        /// </summary>
        Chinese = 1,
        /// <summary>
        /// 英语
        /// </summary>
        English = 2

    }
}
