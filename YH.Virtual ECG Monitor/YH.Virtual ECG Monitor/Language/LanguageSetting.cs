using HYS.Library;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace YH.Virtual_ECG_Monitor
{

    public static class MyContentControl
    {


        public static AppLanguage _curLanguage = AppLanguage.Chinese;
        public static AppLanguage CurLanguage
        {
            get
            {
                return _curLanguage;
            }
            set
            {
                _curLanguage = value;
            }
        }

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
        public static void ApplyLanguage(this ContentControl contentControl)
        {
            ApplyLanguage(contentControl, _curLanguage);
        }

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


    public class PageResource
    {
        public string PageName { get; set; }
        public List<ControlResoure> Controls { get; set; }
    }

    public class ControlResoure
    {
        public string ControlName { get; set; }
        public string ControlText { get; set; }
    }
    public enum AppLanguage
    {
        Chinese = 0,
        English = 1
    }
}
