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
         SolidColorBrush selected = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#BEE6FD"));
        // SolidColorBrush selected = Brushes.LightCyan;
     
        SolidColorBrush unSelected = Brushes.LightGray;
        SolidColorBrush unCellSelected = Brushes.Black;
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
            BindData(model, 0);
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

        /// <summary>
        /// 初始化左边--选择版面的按钮导航
        /// </summary>
        /// <param name="model"></param>
        /// <param name="selectedIndex"></param>
        private void BindData(LayoutSettingData model, int selectedIndex)
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
                button.VerticalContentAlignment = VerticalAlignment.Center;
                button.Click += Button_Click;

                TextBlock textBlock = new TextBlock();            
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
            //记录旧的layoutSelectedIndex
            int old_layoutSelectedIndex = layoutSelectedIndex;

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
            if (old_layoutSelectedIndex == layoutSelectedIndex)
            {
                return;
            }
            if (old_layoutSelectedIndex < 0 || old_layoutSelectedIndex>=model.Layouts.Count)
            {
                old_layoutSelectedIndex = layoutSelectedIndex;
            }

            //显示当前版面对应的grid
            for (int i = 0; i < 4; i++)
            {
                //非新旧交替的两个grid,不需要处理
                if (i != model.Layouts[old_layoutSelectedIndex].GridModel && i != model.Layouts[layoutSelectedIndex].GridModel)
                {
                    continue;
                }
                var grid = this.FindName("grid_" + i.ToString()) as Grid;
                grid.Visibility = i == model.Layouts[layoutSelectedIndex].GridModel ? Visibility.Visible : Visibility.Hidden;



                //清除非所有button的选中颜色并显示对应的波形信息
                foreach (var child in grid.Children)
                {
                    if (child is Grid)
                    {
                        Grid cell = child as Grid;
                        Button buttonItem = cell.Children[0] as Button;
                        buttonItem.Background = unCellSelected;                      
                        if (i == model.Layouts[layoutSelectedIndex].GridModel)
                        {
                            string[] result = GetCellInfoFromCoordinate(buttonItem.Tag.ToString());
                            (cell.Children[1] as TextBlock).Text = result[0];
                            (buttonItem.Content as TextBlock).Text = result[1];
                            (cell.Children[2] as TextBlock).Text = result[2];
                          
                        }
                    }
                }
            }

            spInitStatus.Opacity = 0;
            selectedWave = null;
            waveCategories = null;
            lbWaveCategory.ItemsSource = waveCategories;
            lbWaveCategory.Visibility = Visibility.Hidden;

            layoutName = model.Layouts[layoutSelectedIndex].Name;
            tbLayoutName.Text = layoutName;

            //设置当前点中按钮颜色
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
        /// 根据坐标位置返回对应的波形单元格显示信息
        /// </summary>
        private string[] GetCellInfoFromCoordinate(string tag)
        {
            string title = string.Empty;
            string status = string.Empty;
            string name = string.Empty;
            LayoutWave wave = null;
            int[] tags = tag.ToString().Split(',').Select(p => int.Parse(p)).ToArray();
            switch (tags[0])
            {
                case 0: title = "波形"; wave = model.Layouts[layoutSelectedIndex].MainWaveCategories[tags[1]]; break;
                case 1: title = "视窗"; wave = model.Layouts[layoutSelectedIndex].OtherWaveCategories[tags[1]]; break;
                case 2: title = "NIBP"; wave = model.Layouts[layoutSelectedIndex].NIBPWaveCategory; break;
            }
            title = title + (tags[1] + 1).ToString();
            status = wave.Status;
            name = wave.Name;
            return new string[3] { title, name, status };

        }
        /// <summary>
        /// 波形位置按钮响应事件处理
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

            //设置点中按钮为选中颜色
            foreach (var child in ((button.Parent as Grid).Parent as Grid).Children)
            {
                if (child is Grid)
                {
                    Button buttonItem = (child as Grid).Children[0] as Button;
                    buttonItem.Background = buttonItem == button ? selected : unCellSelected;
                    (buttonItem.Content as TextBlock).Foreground = buttonItem == button ? Brushes.Black : Brushes.Gainsboro;
                }
            }
            // 刷新界面,显示波形的初始化状态选择控件
            lbWaveCategory.ItemsSource = waveCategories;
            lbWaveCategory.SelectedValue = selectedWave.Name;
            lbWaveCategory.Visibility = Visibility.Visible;

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
            if (CheckLayoutName())
            {
                Setting.Save(model);
                MessageBox.Show("保存成功");
                BindData(model,layoutSelectedIndex);
            }
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

            SetLayoutName setlayName = new SetLayoutName(tbLayoutName.Text,model.Layouts);           
            if (setlayName.ShowDialog()==true)
            {
                //复制当前版面
                LayoutSettingModel layout = model.Layouts[layoutSelectedIndex];
                LayoutSettingModel newLayout = layout.Clone();
                newLayout.Name = setlayName.LayoutName;
                model.Layouts.Add(newLayout);
                BindData(model, model.Layouts.Count - 1);
            }
            else
            {
                return;
            }         
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
            if (CheckLayoutName())
            {
                Setting.Save(model);
                MessageBox.Show("保存成功");
                this.Close();
            }
        }

        private bool CheckLayoutName()
        {
            if (layoutSelectedIndex > 3)
            {
                string layoutName = tbLayoutName.Text.Trim();
                if (string.IsNullOrWhiteSpace(layoutName))
                {
                    MessageBox.Show("版面名称不能为空");
                    return false;
                }
                if (model.Layouts.ToList().Exists(p => p.Name == layoutName))
                {
                    MessageBox.Show("版面名称不能重复");
                    return false;
                }
                model.Layouts[layoutSelectedIndex].Name = layoutName;
            }
            return true;
        }
    }
}
