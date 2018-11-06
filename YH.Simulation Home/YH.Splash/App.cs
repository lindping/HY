using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Threading;

namespace YH.Splash
{
    class App : Application
    {
        /// <summary>
        /// 
        /// </summary>
        [STAThread()]
        static void Main()
        {
            Splasher.Splash = new SplashScreen();
            Splasher.ShowSplash();

            for (int i = 0; i < 100; i+=10)
            {
                MessageListener.Instance.SetMessage(string.Format("Load module {0}", i));
                Thread.Sleep(10);
            }

            Splasher.CloseSplash();

            new App();
        }
        /// <summary>
        /// 
        /// </summary>
        public App()
        {
            StartupUri = new System.Uri("MainWindow.xaml", UriKind.Relative);

            Run();
        }
    }
}
