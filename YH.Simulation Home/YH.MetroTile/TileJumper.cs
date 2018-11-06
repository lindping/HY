using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace YH.MetroTile
{
    public class QuickJumper
    {
        public static Dictionary<string, double> WrapPanelDi = new Dictionary<string, double>();

        public static void ShiftStackPanel(string letter, ref StackPanel metroStackPanel)
        {
            if (WrapPanelDi.ContainsKey(letter.ToLower()))
            {
                DoubleAnimationUsingKeyFrames doubleAnim = new DoubleAnimationUsingKeyFrames();

                Double newX = WrapPanelDi[letter.ToLower()];

                doubleAnim.Duration = TimeSpan.FromMilliseconds(1800);


                doubleAnim.KeyFrames.Add(new SplineDoubleKeyFrame(-newX, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1)), new KeySpline(0.161, 0.079, 0.008, 1)));

                doubleAnim.FillBehavior = FillBehavior.HoldEnd;

                metroStackPanel.BeginAnimation(Canvas.LeftProperty, doubleAnim);

                doubleAnim.KeyFrames.Clear();
            }
        }
    }  
    
}
