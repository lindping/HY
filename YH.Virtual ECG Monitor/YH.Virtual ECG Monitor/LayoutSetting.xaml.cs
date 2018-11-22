using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace YH.Virtual_ECG_Monitor
{
    /// <summary>
    /// LayoutSetting.xaml 的交互逻辑
    /// </summary>
    public partial class LayoutSetting : Window
    {
        // SolidColorBrush selected = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#0089e1"));
        // SolidColorBrush selected = Brushes.LightCyan;
        SolidColorBrush selected = new SolidColorBrush(Color.FromRgb(190,230,253));
        SolidColorBrush unSelected = Brushes.LightGray;

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

        private void InitializeData()
        {
            model = GetData();
            BindData(model,0);
        }
        private LayoutSettingData GetData()
        {
            LayoutSettingData data = Setting.Get<LayoutSettingData>();
            if (data == null || data.Layouts == null)
            {
                //没有数据则构建数据
                data = new LayoutSettingData()
                {
                    Layouts = new List<LayoutSettingModel>() {
                    new LayoutSettingModel() {  IsDefault=true, Name="5波形版面", GridModel=0},
                    new LayoutSettingModel(){  IsDefault=true, Name="4波形版面",GridModel=1 },
                    new LayoutSettingModel(){  IsDefault=true, Name="3波形版面",GridModel=2},
                    new LayoutSettingModel(){  IsDefault=true, Name="大数字版面",GridModel=3},
                }
                };

                data.Layouts[0].MainWaveCategories = Constants.MainWaveCategories.Take(5).ToList();
                data.Layouts[0].OtherWaveCategories = Constants.otherWaveCategories.Take(8).ToList();
                data.Layouts[0].NIBPWaveCategory = Constants.NIBPWaveCategories.FirstOrDefault();

                data.Layouts[1].MainWaveCategories = Constants.MainWaveCategories.Skip(2).Take(4).ToList();
                data.Layouts[1].OtherWaveCategories = Constants.otherWaveCategories.Skip(2).Take(7).ToList();
                data.Layouts[1].NIBPWaveCategory = Constants.NIBPWaveCategories.FirstOrDefault();

                data.Layouts[2].MainWaveCategories = Constants.MainWaveCategories.Skip(4).Take(3).ToList();
                data.Layouts[2].OtherWaveCategories = Constants.otherWaveCategories.Skip(8).Take(3).ToList();
                data.Layouts[2].NIBPWaveCategory = Constants.NIBPWaveCategories.FirstOrDefault();

                data.Layouts[3].MainWaveCategories = Constants.MainWaveCategories.Skip(5).Take(3).ToList();
                data.Layouts[3].OtherWaveCategories = Constants.otherWaveCategories.Skip(11).Take(1).ToList();
                data.Layouts[3].NIBPWaveCategory = Constants.NIBPWaveCategories.FirstOrDefault();
            }
            return data;

        }

        private void BindData(LayoutSettingData model,int selectedIndex)
        {
            layoutPanel.Children.Clear();
            Button selectedButton = null;
            for (int i = 0; i < model.Layouts.Count; i++)
            {
                Button button = new Button();
                button.Name = "btnLayout_" + i.ToString();
                button.Height = 40;
                button.Width = 140;
                button.Margin = new Thickness(0, 10, 0, 10);

                button.Click += Button_Click;

                TextBlock textBlock = new TextBlock();
                textBlock.Height = 30;
                textBlock.Text = model.Layouts[i].Name;
                button.Content = textBlock;



                if (i == selectedIndex)
                {
                    selectedButton = button;
                }

                DockPanel panel = new DockPanel();
                panel.Children.Add(button);
                if (i >= 4)
                {
                    button.Width = 85;
                    Button delBtn = new Button();
                    delBtn.Height = 40;
                    delBtn.Width = 50;
                    delBtn.Margin = new Thickness(5, 0, 0, 0);
                    delBtn.Content = "删除";
                    delBtn.Name = "delBtnLayout_" + i.ToString();
                    delBtn.Tag = model.Layouts[i].Name;
                    delBtn.Click += Button_Click_delete;
                    panel.Children.Add(delBtn);
                }

                layoutPanel.Children.Add(panel);
            }

            if (selectedButton != null)
            {
                RoutedEventArgs e = new RoutedEventArgs();
                e.RoutedEvent = Button.ClickEvent;
                selectedButton.RaiseEvent(e);
            }

        }
        /// <summary>
        /// 版面选择按钮响应事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            string layoutName = (button.Content as TextBlock).Text;
            for (int i = 0; i < model.Layouts.Count; i++)
            {
                if (layoutName == model.Layouts[i].Name)
                {
                    layoutSelectedIndex = i;
                    break;
                }
            }

            //显示当前版面对应的grid
            for (int i = 0; i < 4; i++)
            {
                var grid = this.FindName("grid_" + i.ToString()) as Grid;
                grid.Visibility = i == model.Layouts[layoutSelectedIndex].GridModel ? Visibility.Visible : Visibility.Hidden;

                //清除非所有button的选中颜色并显示对应的波形信息
                foreach (var child in grid.Children)
                {
                    if (child is StackPanel)
                    {
                        StackPanel panel = child as StackPanel;
                        int index = panel.Children.Count > 2 ? 1 : 0;
                        Button buttonItem = panel.Children[index] as Button;
                        if (buttonItem!=null && buttonItem.Background == selected)
                        {
                            buttonItem.Background=unSelected;
                        }
                    }
                }
            }

            spInitStatus.Opacity = 0;
            selectedWave = null;
            waveCategories = null;
            lbWaveCategory.ItemsSource = waveCategories;

            layoutName = model.Layouts[layoutSelectedIndex].Name;
            tbLayoutName.Text = layoutName;

            //设置选中颜色
            foreach (var child in layoutPanel.Children)
            {
                if (child is DockPanel)
                {
                    Button buttonItem = (child as DockPanel).Children[0] as Button;
                    buttonItem.Background = buttonItem == button ? selected : unSelected;
                }
            }         

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

            //设置选中颜色
            foreach (var child in ((button.Parent as StackPanel).Parent as Grid).Children)
            {
                if (child is StackPanel)
                {
                    Button buttonItem = (child as StackPanel).Children[0] as Button;
                    buttonItem.Background = buttonItem == button ? selected : unSelected;
                }
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
            MessageBox.Show("保存成功");
        }

        /// <summary>
        /// 更改版面位置对应波形响应事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbWaveCategory_Selected(object sender, RoutedEventArgs e)
        {
            if (lbWaveCategory.SelectedValue == null || selectedWave == null)
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
            if (selectedWave != null)
            {
                selectedWave.Status = (sender as RadioButton).Content.ToString();
            }
        }

        /// <summary>
        /// 版面名称的修改,手工同步数据源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbLayoutName_TextChanged(object sender, TextChangedEventArgs e)
        {
            string text = (sender as TextBox).Text;
            if (layoutSelectedIndex > 3 && !string.IsNullOrWhiteSpace(text))
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
            for (int i = 0; i < model.Layouts.Count; i++)
            {
                model.Layouts[layoutSelectedIndex].IsDefault = i == layoutSelectedIndex;
            }
            Setting.Save(model);
        }

        private void Button_Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_add(object sender, RoutedEventArgs e)
        {
            string layoutName = tbLayoutName.Text;
            if (string.IsNullOrWhiteSpace(layoutName))
            {
                MessageBox.Show("版面名称不能为空");
                return;
            }
            if (model.Layouts.ToList().Exists(p => p.Name == layoutName))
            {
                MessageBox.Show("版面名称不能重复");
                return;
            }

            //复制当前版面
            LayoutSettingModel layout = model.Layouts[layoutSelectedIndex];
            LayoutSettingModel newLayout = layout.Clone();
            newLayout.Name = layoutName;
            model.Layouts.Add(newLayout);
            BindData(model,model.Layouts.Count-1);
        }

        private void Button_Click_delete(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            string layoutName = button.Tag.ToString();
            LayoutSettingModel item = model.Layouts.Find(p => p.Name == layoutName);
            int delIndex = model.Layouts.IndexOf(item);
            model.Layouts.Remove(item);

            if (layoutSelectedIndex == delIndex)
            {
                BindData(model, 0);
            }
            else
            {
                BindData(model, -1);
            }
          

        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Setting.Save(model);
            MessageBox.Show("保存成功");
            this.Close();
        }
    }
}
