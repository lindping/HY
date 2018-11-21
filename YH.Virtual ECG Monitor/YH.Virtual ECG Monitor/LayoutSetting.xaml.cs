using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;


namespace YH.Virtual_ECG_Monitor
{
    /// <summary>
    /// LayoutSetting.xaml 的交互逻辑
    /// </summary>
    public partial class LayoutSetting : Window
    {
        List<LayoutWave> waveCategories;
        LayoutSettingData model;
        LayoutWave selectedWave = null;
        string layoutName = string.Empty;

        int layoutSelectedIndex = -1;
        int waveSelectedIndex = -1;
        int waveCategoryIndex = -1;

        public LayoutSetting()
        {
            InitializeComponent();
            InitializeData();
        }

        public void InitializeData()
        {
            model = Setting.Get<LayoutSettingData>();
            if (model == null || model.Layouts == null)
            {
                //没有数据则构建数据
                model = new LayoutSettingData()
                {
                    Layouts = new List<LayoutSettingModel>() {
                    new LayoutSettingModel() {  IsDefault=true, Name="5波形版面"},
                    new LayoutSettingModel(){  IsDefault=true, Name="4波形版面"},
                    new LayoutSettingModel(){  IsDefault=true, Name="3波形版面"},
                    new LayoutSettingModel(){  IsDefault=true, Name="大数字版面"},
                }
                };


                model.Layouts[0].MainWaveCategories = Constants.MainWaveCategories.Take(5).ToList();
                model.Layouts[0].OtherWaveCategories = Constants.otherWaveCategories.Take(8).ToList();
                model.Layouts[0].NIBPWaveCategory = Constants.NIBPWaveCategories.FirstOrDefault();

                model.Layouts[1].MainWaveCategories = Constants.MainWaveCategories.Skip(2).Take(4).ToList();
                model.Layouts[1].OtherWaveCategories = Constants.otherWaveCategories.Skip(2).Take(7).ToList();
                model.Layouts[1].NIBPWaveCategory = Constants.NIBPWaveCategories.FirstOrDefault();

                model.Layouts[2].MainWaveCategories = Constants.MainWaveCategories.Skip(4).Take(3).ToList();
                model.Layouts[2].OtherWaveCategories = Constants.otherWaveCategories.Skip(8).Take(3).ToList();
                model.Layouts[2].NIBPWaveCategory = Constants.NIBPWaveCategories.FirstOrDefault();

                model.Layouts[3].MainWaveCategories = Constants.MainWaveCategories.Skip(5).Take(3).ToList();
                model.Layouts[3].OtherWaveCategories = Constants.otherWaveCategories.Skip(11).Take(1).ToList();
                model.Layouts[3].NIBPWaveCategory = Constants.NIBPWaveCategories.FirstOrDefault();
            }

            lbCustomLayout.ItemsSource = model.Layouts;


        }

        /// <summary>
        /// 版面选择按钮响应事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            for (int i = 1; i <= 4; i++)
            {
                var grid = this.FindName("grid_" + i.ToString()) as Grid;
                grid.Visibility = button.Tag.ToString() == i.ToString() ? Visibility.Visible : Visibility.Hidden;
            }
            layoutSelectedIndex = int.Parse(button.Tag.ToString()) - 1;

            spInitStatus.Opacity = 0;
            selectedWave = null;

            layoutName = model.Layouts[layoutSelectedIndex].Name;
            tbLayoutName.Text = layoutName;
        }

        /// <summary>
        /// 版面位置按钮响应事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            //读出按钮的坐标信息
            string[] tags = button.Tag.ToString().Split(',');
            waveCategoryIndex = int.Parse(tags[0]);
            waveSelectedIndex = int.Parse(tags[1]);

            //根据按钮的坐标信息,找到对应的波形数据以及 可供选择的波形类别
            if (waveCategoryIndex == 0)
            {
                waveCategories = Constants.MainWaveCategories;
                selectedWave = model.Layouts[layoutSelectedIndex].MainWaveCategories[waveSelectedIndex];
            }
            else if (waveCategoryIndex == 1)
            {
                waveCategories = Constants.otherWaveCategories;
                selectedWave = model.Layouts[layoutSelectedIndex].OtherWaveCategories[waveSelectedIndex];
            }
            else
            {
                waveCategories = Constants.NIBPWaveCategories;
                selectedWave = model.Layouts[layoutSelectedIndex].NIBPWaveCategory;
            }
            
            // 刷新界面,显示波形的初始化状态选择控件
            lbWaveCategory.ItemsSource = waveCategories;
            lbWaveCategory.SelectedValue = selectedWave.Name;      
            
            spInitStatus.Opacity = 1;
            rbInitStatus.DataContext = selectedWave;
        }

        /// <summary>
        /// 保存设定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {           
            Setting.Save(model);
        }

        /// <summary>
        /// 更改版面位置对应波形响应事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbWaveCategory_Selected(object sender, RoutedEventArgs e)
        {
            if (lbWaveCategory.SelectedValue == null || selectedWave==null)
            {
                return;
            }         
            selectedWave.Name = lbWaveCategory.SelectedValue.ToString();
            
        }

        /// <summary>
        /// 选择波形的初始化状态修改,手工同步数据源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            selectedWave.Status = (sender as RadioButton).Content.ToString();
        }

        /// <summary>
        /// 版面名称的修改,手工同步数据源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbLayoutName_TextChanged(object sender, TextChangedEventArgs e)
        {
            string text = (sender as TextBox).Text;
            if (!string.IsNullOrWhiteSpace(text))
            {
                model.Layouts[layoutSelectedIndex].Name = text;
            }
        }

        /// <summary>
        /// 当前选择版面设置为默认版面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (layoutSelectedIndex < 0)
            {
                MessageBox.Show("请先选择一种版面");
            }
            for (int i = 0; i < model.Layouts.Count;i++)
            {
                model.Layouts[layoutSelectedIndex].IsDefault = i == layoutSelectedIndex;
            }
            Setting.Save(model);
        }

        private void Button_Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
