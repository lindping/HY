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
    public partial class TileCategory : UserControl
    {
        private string _displayname;
        private bool _isoptionsetup;

        public TileCategory()
        {
            InitializeComponent();

            _displayname = "Display Name";       
            _isoptionsetup = false;
                     
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
            TileCategorySetupWindow tilecategorysetupWindow = new MetroTile.TileCategorySetupWindow();
            if (tilecategorysetupWindow.ShowDialog() == true)
            {

            }
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
