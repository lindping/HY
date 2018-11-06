using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace YH.Splash
{
    class Program : Application
    {
        /// <summary>
        /// 
        /// </summary>
        [STAThread()]
        static void Main()
        {
            Splasher.Splash = new SplashWindow();
            Splasher.ShowSplash();
            //MessageListener.Instance.SetTitle(string.Format("BLS"));
            for (int i = 0; i < 1000; i += 10)
            {
                //MessageListener.Instance.SetMessage(string.Format("Load module {0}", i));
                Thread.Sleep(10);
            }

            Splasher.CloseSplash();

            new Program();
        }
        /// <summary>
        /// 
        /// </summary>
        public Program()
        {
            StartupUri = new System.Uri("MainWindow.xaml", UriKind.Relative);

            Run();
        }
    }
}
