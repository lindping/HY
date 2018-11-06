﻿using System;
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
using System.Windows.Shapes;

namespace YH.Language
{
    /// <summary>
    /// SelectorWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SelectorWindow : Window
    {

        private string _languageCode = "zh-CN";  //默认中文

        public SelectorWindow()
        {
            InitializeComponent();
        }

        public string LanguageCode
        {
            get { return _languageCode; }
            set { _languageCode = value; }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LanguageSelector.LanguageCode = LanguageCode; // "en-US";
        }

        private void button_OK_Click(object sender, RoutedEventArgs e)
        {
            LanguageCode = LanguageSelector.LanguageCode;
            this.DialogResult = true;
        }

        private void button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }


    }
}
