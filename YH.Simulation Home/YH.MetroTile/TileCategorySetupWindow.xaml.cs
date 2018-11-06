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
using System.Windows.Shapes;

namespace YH.MetroTile
{
    /// <summary>
    /// TileSetupWindow.xaml 的交互逻辑
    /// </summary>
    public partial class TileCategorySetupWindow : Window
    {
        private string _displayname;
        private int _columns;
        private int _tilesizes;

        public TileCategorySetupWindow()
        {
            InitializeComponent();

            _displayname = "Display Name";
            _columns = 2;
            _tilesizes = 100;
        }

        public string displayName
        {
            get
            {
                return _displayname;
            }
            set
            {
                _displayname = value;
            }
        }
        public int Columns
        {
            get
            {
                return _columns;
            }
            set
            {
                _columns = value;
            }
        }
             
        public int TileSizes
        {
            get
            {
                return _tilesizes;
            }
            set
            {
                _tilesizes = value;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string path = "/YH.MetroTile;component/Skin/BlueSkin.xaml";
            ResourceDictionary newDictionary = new ResourceDictionary();
            newDictionary.Source = new Uri(path, UriKind.Relative);
            this.Resources.MergedDictionaries.Clear();
            this.Resources.MergedDictionaries.Add(newDictionary);
        }

        private void RedSkinButton_Click(object sender, RoutedEventArgs e)
        {
            ChangeSkin("RedSkin.xaml");
            SaveSkin("RedSkin.xaml");
        }

        private void GreenSkinButton_Click(object sender, RoutedEventArgs e)
        {
            ChangeSkin("GreenSkin.xaml");
            SaveSkin("GreenSkin.xaml");
        }

        private void BlueSkinButton_Click(object sender, RoutedEventArgs e)
        {
            ChangeSkin("BlueSkin.xaml");
            SaveSkin("BlueSkin.xaml");
        }

        private void PurpleSkinButton_Click(object sender, RoutedEventArgs e)
        {
            ChangeSkin("PurpleSkin.xaml");
            SaveSkin("PurpleSkin.xaml");
        }
        private void ChangeSkin(string path)
        {
            ResourceDictionary skinRD = new ResourceDictionary();
            skinRD.Source = new Uri(path, UriKind.Relative);
            this.Resources.MergedDictionaries.Clear();
            this.Resources.MergedDictionaries.Add(skinRD);
        }

        private void SaveSkin(string path)
        {
            //Properties.Settings.SkinPath = path;
            //Properties.Settings.Save();
        }

        private void MainBgrndRct_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void button_OK_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
