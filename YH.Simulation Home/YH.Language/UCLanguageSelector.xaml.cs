using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace YH.Language
{
    /// <summary>
    /// UCLanguageSelector.xaml 的交互逻辑
    /// </summary>
    public partial class UCLanguageSelector : UserControl
    {
        private string _languageCode = "zh-CN";  //默认中文

        public UCLanguageSelector()
        {
            InitializeComponent();

            LanguageListBind();
        }

        /// <summary>
        /// 
        /// </summary>
        public string LanguageCode
        {
            get { return _languageCode; }
            set { _languageCode = value; }
        }

        private void LanguageListBind()
        {
            cmb_Language.Items.Clear();
            for (int i = 0; i < LanguageExtend.Array_LanguageName.Length; i++)
            {
                cmb_Language.Items.Add(new YH.Library.ListItem(LanguageExtend.Array_LanguageCode[i], LanguageExtend.Array_LanguageName[i], ""));
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            cmb_Language.Text = LanguageExtend.LanguageNameOfIndex(LanguageCode);
        }

        public delegate void SelectionChangedDelegate(object sender, SelectionChangedEventArgs e);
        public event SelectionChangedDelegate SelectionChanged;

        private void cmb_Language_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LanguageCode = LanguageExtend.LanguageCodeOfIndex(((YH.Library.ListItem)cmb_Language.SelectedItem).Text);
            if (SelectionChanged != null)
                SelectionChanged(sender, e);
        }


    }
}
