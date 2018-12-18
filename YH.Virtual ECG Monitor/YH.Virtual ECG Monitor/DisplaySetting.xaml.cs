using HYS.Library;
using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace YH.Virtual_ECG_Monitor
{
    /// <summary>
    /// DisplaySetting.xaml 的交互逻辑
    /// </summary>
    public partial class DisplaySetting : Window
    {
       

        LayoutSettingData model;
      
        public DisplaySetting()
        {
            InitializeComponent();
            InitializeData();
        }

        public void InitializeData()
        {
            model = Setting.Get<LayoutSettingData>();
            List<LayoutSettingModel> list= model.Layouts;
            displayList.ItemsSource = list;       
      
        }
        //关闭窗口
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button selectButton = sender as Button;
            SetLayoutButtonSelected(selectButton.DataContext as LayoutSettingModel);
        }

        private void SetLayoutButtonSelected(LayoutSettingModel selectedModel)
        {
            foreach (var element in VisualTreeHelper_ex.FindVisualChildren<Button>(displayList))
            {
                if (element is Button)
                {
                    Button button = element as Button;
                    if (button.DataContext as LayoutSettingModel== selectedModel)
                    {
                        if (button.Background != SelectToColorConverter.Selected)
                        {
                            button.Background = SelectToColorConverter.Selected;
                        }
                     //   model.Custom.DefaultLayout = (button.DataContext as LayoutSettingModel).Name;

                    }
                    else
                    {
                        if (button.Background == SelectToColorConverter.Selected)
                        {
                            button.Background = SelectToColorConverter.UnSelected;
                        }
                    }
                }
            }
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Setting.Save(model);
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            LayoutSetting frm = new Virtual_ECG_Monitor.LayoutSetting();
            frm.Show();

        }
    }


   
}
