using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Threading;
using YH.Splash;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Reflection;

namespace YH
{
    class App : Application
    {

        /// <summary> 
        /// 该函数设置由不同线程产生的窗口的显示状态。 
        /// </summary> 
        /// <param name="hWnd">窗口句柄</param> 
        /// <param name="cmdShow">指定窗口如何显示。查看允许值列表，请查阅ShowWlndow函数的说明部分。</param> 
        /// <returns>如果函数原来可见，返回值为非零；如果函数原来被隐藏，返回值为零。</returns> 
        [DllImport("User32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int cmdShow);
        /// <summary> 
        /// 该函数将创建指定窗口的线程设置到前台，并且激活该窗口。键盘输入转向该窗口，并为用户改各种可视的记号。系统给创建前台窗口的线程分配的权限稍高于其他线程。 
        /// </summary> 
        /// <param name="hWnd">将被激活并被调入前台的窗口句柄。</param> 
        /// <returns>如果窗口设入了前台，返回值为非零；如果窗口未被设入前台，返回值为零。</returns> 
        [DllImport("User32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);
        private const int WS_SHOWNORMAL = 1;
        public static Process instance;

        /// <summary>
        /// 
        /// </summary>
        [STAThread()]
        static void Main(string[] args)
        {

            if (instance == null)
            {
                Splasher.Splash = new YH.Splash.SplashScreen();
                Splasher.ShowSplash();

                for (int i = 0; i < 100; i += 10)
                {
                    MessageListener.Instance.SetMessage(string.Format("Load module {0}", i));
                    Thread.Sleep(10);
                }

                Splasher.CloseSplash();

                new App();

            }
            else
            {

                HandleRunningInstance(instance);
            }

        }
        /// <summary>
        /// 
        /// </summary>
        public App()
        {
            StartupUri = new System.Uri("MainWindow.xaml", UriKind.Relative);

            Run();
        }

        public static Process RunningInstance()
        {
            Process current = Process.GetCurrentProcess();
            Process[] processes = Process.GetProcessesByName(current.ProcessName);
            foreach (Process process in processes)
            {
                if (process.Id != current.Id)
                {
                    if (Assembly.GetExecutingAssembly().Location.Replace("/", "\\") == current.MainModule.FileName)
                    {
                        return process;
                    }
                }
            }
            return null;
        }

        /// <summary> 
        /// 显示已运行的程序。 
        /// </summary> 
        public static void HandleRunningInstance(Process instance)
        {
            ShowWindowAsync(instance.MainWindowHandle, WS_SHOWNORMAL); //显示，可以注释掉 
            SetForegroundWindow(instance.MainWindowHandle);            //放到前端 
        }
    }
}
