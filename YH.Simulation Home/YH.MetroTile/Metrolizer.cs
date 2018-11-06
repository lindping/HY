using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace YH.MetroTile
{
    public class Metrolizer
    {
        private Double wrapPanelX = 0;

        public void DisplayTiles(ref StackPanel metroStackPanel)
        {
            string[] catetory = new string[] { "Application", "Editor", "Setup", "Help" };

            //string[] alphabet = new string[] {"a", "b", "c", "d", "e", "f", "g", "h", "i",
            //                        "j", "k", "l", "m", "n", "o", "p", "q", "r",
            //                        "s", "t", "u", "v", "w", "x", "y", "z" };
            //string[] numbers = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };

            Dictionary<string, string[]> di = new Bettercut().GetBettercutDi();

            foreach (string s in catetory)
            {
                string letter = s;
                IEnumerable<KeyValuePair<string, string[]>> coll = di.Where(k => k.Value[0]== letter);
                if (coll.Count() > 0)
                {
                    AddTiles(coll, ref metroStackPanel, letter);
                }
            }

            //Dictionary<string, string[]> di = new IconsAndPaths().GetIconsAndPaths();

            //foreach (string s in alphabet)
            //{
            //    string letter = s;
            //    IEnumerable<KeyValuePair<string, string[]>> coll = di.Where(k => k.Key.StartsWith(letter, true, null));
            //    if (coll.Count() > 0)
            //    {
            //        AddTiles(coll, ref metroStackPanel, letter);
            //    }
            //}


            //foreach (string s in numbers)
            //{
            //    string letter = s;
            //    IEnumerable<KeyValuePair<string, string[]>> coll = di.Where(k => k.Key.StartsWith(letter, true, null));
            //    if (coll.Count() > 0)
            //    {
            //        AddTiles(coll, ref metroStackPanel, letter);
            //    }
            //}
        }

        public void AddTiles(IEnumerable<KeyValuePair<string, string[]>> coll, ref StackPanel metroStackPanel, string letter)
        {

            //WrapPanel tileCategoryWrapPanel = new WrapPanel();
            //tileCategoryWrapPanel.Orientation = Orientation.Vertical;
            //tileCategoryWrapPanel.Margin = new Thickness(0, 0, 20, 0);
            //// 3 tiles height-wise    
            //tileCategoryWrapPanel.Width = 300;
          



            //return;

            WrapPanel tileWrapPanel = new WrapPanel();
            tileWrapPanel.Orientation = Orientation.Horizontal;
            tileWrapPanel.Margin = new Thickness(0, 0, 20, 0);
            // 3 tiles height-wise    
            tileWrapPanel.Width  = 320;
            tileWrapPanel.Height = (110 * 6) + (6 * 6);

            TileCategory newTileCategory = new MetroTile.TileCategory();
            newTileCategory.TileTxtBlck.Text = letter;
            newTileCategory.Width = 306;
            newTileCategory.Margin = new Thickness(3, 3, 3, 0);
            tileWrapPanel.Children.Add(newTileCategory);

            int i = 1;

            foreach (KeyValuePair<string, string[]> kvp in coll)
            {         
                      
                Tile newTile = new Tile();
                newTile.ExecutablePath  = kvp.Value[1];
                //newTile.TileIcon.Source = new BitmapImage(new Uri(kvp.Value[0], UriKind.Absolute));
                newTile.TileTxtBlck.Text = kvp.Key;
                newTile.Margin = new Thickness(3, 3, 3, 3);

                if (i == 1)
                {
                    if (letter == "Application")
                        newTile.TileSize = new MetroTile.TileSize_X_Large(); 
                    else if (letter == "Editor")
                        newTile.TileSize = new MetroTile.TileSize_Large();
                    else
                        newTile.TileSize = new TileSize_Medium();
                }
                else
                    newTile.TileSize = new TileSize_Medium();
                newTile.PreviewMouseDoubleClick += NewTile_PreviewMouseDoubleClick;
                tileWrapPanel.Children.Add(newTile);
                i++;
            }

            WrapPanelLocation(letter, tileWrapPanel);
            metroStackPanel.Children.Add(tileWrapPanel);
        }

        public delegate void PreviewMouseDoubleClickDelegate(object sender, MouseButtonEventArgs e);

        public event PreviewMouseDoubleClickDelegate MyPreviewMouseDoubleClick;

        private void NewTile_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //throw new NotImplementedException();

            if (MyPreviewMouseDoubleClick != null)
                MyPreviewMouseDoubleClick(sender, e);

            //Tile tile = (Tile)sender;
            //if (tile != null)
            //{
            //    string ExecutablePath = tile.ExecutablePath;
            //    try
            //    {
            //        Process.Start(ExecutablePath);
            //    }
            //    catch (Win32Exception ex)
            //    {
            //    }
            //}
        }

        public void WrapPanelLocation(string letter, WrapPanel tileWrapPanel)
        {
            if (QuickJumper.WrapPanelDi.Count == 0)
                QuickJumper.WrapPanelDi.Add(letter, 0);
            else
                QuickJumper.WrapPanelDi.Add(letter, wrapPanelX);

            // Increase value of wrapPanelX as appropriate. 
            // 6 is right margin of a Tile. 
            if (tileWrapPanel.Children.Count <= 6)
                wrapPanelX += ((110 + 6) + 18);
            else
            {
                double numberOfColumns = Math.Ceiling((double)(tileWrapPanel.Children.Count / 6));
                double x = (numberOfColumns * 110) + (numberOfColumns * 6) + 18;
                wrapPanelX += x;
            }
        }
    }
}
