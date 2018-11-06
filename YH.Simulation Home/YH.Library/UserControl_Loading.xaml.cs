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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace YH.Library
{
    /// <summary>
    /// UserControl_Wait.xaml 的交互逻辑
    /// </summary>
    public partial class UserControl_Loading : UserControl
    {
        #region Data
        //private  DispatcherTimer animationTimer;
        private DoubleAnimation doubleAnimation = new DoubleAnimation();

        #endregion

        #region Constructor
        public UserControl_Loading()
        {
            InitializeComponent();

            doubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(90));
            doubleAnimation.CurrentTimeInvalidated += DoubleAnimation_CurrentTimeInvalidated;

            //animationTimer = new DispatcherTimer(
            //    DispatcherPriority.ContextIdle, Dispatcher);
            //animationTimer.Interval = new TimeSpan(0, 0, 0, 0, 90);   
            SpinnerRotate.BeginAnimation(RotateTransform.AngleProperty, doubleAnimation);
        }

        private void DoubleAnimation_CurrentTimeInvalidated(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            SpinnerRotate.Angle = (SpinnerRotate.Angle + 36) % 360;
            //doubleAnimation.From = SpinnerRotate.Angle;
            //doubleAnimation.To = (SpinnerRotate.Angle + 36) % 360;
        }

        #endregion

        #region Private Methods
        private void Start()
        {
            //animationTimer.Tick += HandleAnimationTick; 
            //animationTimer.Start();
        }
        
        private void Stop()
        {
            //animationTimer.Stop();
            //animationTimer.Tick -= HandleAnimationTick;
        }

        private void HandleAnimationTick(object sender, EventArgs e)
        {
            SpinnerRotate.Angle = (SpinnerRotate.Angle + 36) % 360;
        }

        private void HandleLoaded(object sender, RoutedEventArgs e)
        {
            const double offset = Math.PI;
            const double step = Math.PI * 2 / 10.0;

            SetPosition(C0, offset, 0.0, step);
            SetPosition(C1, offset, 1.0, step);
            SetPosition(C2, offset, 2.0, step);
            SetPosition(C3, offset, 3.0, step);
            SetPosition(C4, offset, 4.0, step);
            SetPosition(C5, offset, 5.0, step);
            SetPosition(C6, offset, 6.0, step);
            SetPosition(C7, offset, 7.0, step);
            SetPosition(C8, offset, 8.0, step);
        }

        private void SetPosition(Ellipse ellipse, double offset,
            double posOffSet, double step)
        {
            ellipse.SetValue(Canvas.LeftProperty, 50.0
                + Math.Sin(offset + posOffSet * step) * 50.0);

            ellipse.SetValue(Canvas.TopProperty, 50
                + Math.Cos(offset + posOffSet * step) * 50.0);
        }

        private void HandleUnloaded(object sender, RoutedEventArgs e)
        {
            Stop();
        }

        private void HandleVisibleChanged(object sender,
            DependencyPropertyChangedEventArgs e)
        {
            //bool isVisible = (bool)e.NewValue;

            //if (isVisible)
            //    Start();
            //else
            //    Stop();
        }
        #endregion
    }
}
