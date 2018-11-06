using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace YH.MetroTile
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {

        private double initMouseX;
        private double finalMouseX;
        private double x;
        private double newX;
        private DispatcherTimer timer;                 //动画处理计时器
        private DoubleAnimationUsingKeyFrames anim;    //动画处理器
        public MainWindow()
        {
            InitializeComponent();

            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 1000);    //1000ms
            timer.Tick += Timer_Tick;

            anim = new DoubleAnimationUsingKeyFrames();
            anim.Duration = TimeSpan.FromMilliseconds(1800);


        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            double mspWidth = MetroStackPanel.ActualWidth;

            if (newX > 200)
            {
                anim.KeyFrames.Add(new SplineDoubleKeyFrame(45, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1)), new KeySpline(0.161, 0.079, 0.008, 1)));
                anim.FillBehavior = FillBehavior.HoldEnd;
                MetroStackPanel.BeginAnimation(Canvas.LeftProperty, anim);
                anim.KeyFrames.Clear();
            }
            else if ((newX + mspWidth) < 500)
            {
                double widthX = 500 - (newX + mspWidth);
                double shiftX = newX + widthX;
                anim.KeyFrames.Add(new SplineDoubleKeyFrame(shiftX, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1)), new KeySpline(0.161, 0.079, 0.008, 1)));
                anim.FillBehavior = FillBehavior.HoldEnd;
                MetroStackPanel.BeginAnimation(Canvas.LeftProperty, anim);
                anim.KeyFrames.Clear();
            }
            timer.Stop();
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            // Move MetroStackPanel so that the WrapPanel with the 
            // required alphabetical group is displayed first.
            QuickJumper.ShiftStackPanel(e.Key.ToString(), ref MetroStackPanel);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Metrolizer metro = new Metrolizer();
            metro.MyPreviewMouseDoubleClick += Metro_MyPreviewMouseDoubleClick;
            metro.DisplayTiles(ref MetroStackPanel);

            string path = "BlueSkin.xaml";
            ResourceDictionary newDictionary = new ResourceDictionary();
            newDictionary.Source = new Uri(path, UriKind.Relative);
            this.Resources.MergedDictionaries.Clear();
            this.Resources.MergedDictionaries.Add(newDictionary);
        }

        private void Metro_MyPreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //throw new NotImplementedException();
            Tile tile = (Tile)sender;
            if (tile != null)
            {
                string ExecutablePath = tile.ExecutablePath;
                try
                {
                    Process.Start(ExecutablePath);
                }
                catch (Win32Exception ex)
                {
                }
            }
        }

        private void MainCanvas_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            initMouseX = e.GetPosition(MainCanvas).X;
            x = Canvas.GetLeft(MetroStackPanel);
        }

        private void MainCanvas_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            finalMouseX = e.GetPosition(MainCanvas).X;
            double diff = Math.Abs(finalMouseX - initMouseX);

            //Make sure the diff is substantial so that tiles 
            // don't scroll on double-click.
            if (diff > 5)
            {
                if (finalMouseX < initMouseX)
                    newX = x - (diff * 2);
                else if (finalMouseX > initMouseX)
                    newX = x + (diff * 2);

                anim.KeyFrames.Add(new SplineDoubleKeyFrame(newX, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1)), new KeySpline(0.161, 0.079, 0.008, 1)));
                anim.FillBehavior = FillBehavior.HoldEnd;
                MetroStackPanel.BeginAnimation(Canvas.LeftProperty, anim);
                anim.KeyFrames.Clear();
                timer.Start();
            }
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

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private bool isOptionSetup = false;



        private void OptionButton_Click(object sender, RoutedEventArgs e)
        {
            

            if (!isOptionSetup)
            {
                isOptionSetup = true;
            }
            else
            {
                isOptionSetup = false;
            }

            SetTileOptionSetup(isOptionSetup);

        }

        private void SetTileOptionSetup(bool isOptionSetup)
        {
            foreach (WrapPanel tileWrapPanel in MetroStackPanel.Children)
            {
                foreach (Control ctl in tileWrapPanel.Children)
                {
                    if (ctl is Tile)
                    {
                        ((Tile)ctl).IsOptionSetup = isOptionSetup;
                    }
                    if (ctl is TileCategory)
                    {
                        ((TileCategory)ctl).IsOptionSetup = isOptionSetup;
                    }
                }
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MainBgrndRct_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
