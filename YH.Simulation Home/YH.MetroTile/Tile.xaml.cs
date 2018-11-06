using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace YH.MetroTile
{
    /// <summary>
    /// Tile.xaml 的交互逻辑
    /// </summary>
    public partial class Tile : UserControl
    {
        private string _displayname;
        private string _catetory;
        private string _tileiconpath;
        private string _executablepath;
        private TileSize _tilesize;
        private bool _isoptionsetup;

        public Tile()
        {
            InitializeComponent();

            _displayname = "Display Name";
            _catetory = "Catetory";
            _tileiconpath = "";
            _executablepath = "";
            _tilesize = new TileSize();
            _isoptionsetup = false;

            SetTileSize();
            SetOptionSetup();
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
        public string Catetory
        {
            get
            {
                return _catetory;
            }
            set
            {
                _catetory = value;
            }
        }
        
        public string TileIconPath
        {
            get
            {
                return _tileiconpath;
            }
            set
            {
                _tileiconpath = value;
            }
        }

        public string ExecutablePath
        {
            get
            {
                return _executablepath;
            }
            set
            {
                _executablepath = value;
            }
        }

        public TileSize TileSize
        {
            get
            {
                return _tilesize;
            }
            set
            {
                _tilesize = value;
                SetTileSize();
            }
        }

        public bool IsOptionSetup
        {
            get
            {
                return _isoptionsetup;
            }
            set
            {
                _isoptionsetup = value;
                SetOptionSetup();
            }
        }

        int i = 0;

        private void SetIcon_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            TileSetupWindow tilesetupWindow = new MetroTile.TileSetupWindow();
            if (tilesetupWindow.ShowDialog() == true)
            {

            }
            //i++;
            //if (i >= 5)
            //    i = 0;

            //switch (i)
            //{
            //    case 1:
            //        tileSize =new TileSize_Small();
            //        break;
            //    case 2:
            //        tileSize = new TileSize_Medium();
            //        break;
            //    case 3:
            //        tileSize = new TileSize_Large();
            //        break;
            //    case 4:
            //        tileSize = new TileSize_X_Large();
            //        break;
            //    default:
            //        tileSize = new TileSize_Medium();
            //        break;
            //}

            SetTileSize();
        }

        private void SetTileSize()
        {
            this.Width = _tilesize.Width;
            this.Height = _tilesize.Height;
        }

        private void SetOptionSetup()
        {
            if (!_isoptionsetup)
            {
                LeftIcon.Visibility = Visibility.Hidden;
                RightIcon.Visibility = Visibility.Hidden;
                SetIcon.Visibility = Visibility.Hidden;
            }
            else
            {
                LeftIcon.Visibility = Visibility.Visible;
                RightIcon.Visibility = Visibility.Visible;
                SetIcon.Visibility = Visibility.Visible;
            }
        }
    }
}
