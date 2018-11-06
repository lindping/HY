using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using YH.Library;
using YH.Splash;

namespace YH.Virtual_Simulator_Engine
{
    public static class Program
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
        /// Application Entry Point.
        /// </summary>
        [System.STAThreadAttribute()]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public static void Main(string[] args)
        {
            GlobalParams.AppName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name.ToString();
            GlobalParams.AppVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            GlobalParams.AppPath = AppDomain.CurrentDomain.BaseDirectory;
            GlobalParams.LanguagePathName = GlobalParams.AppPath + @"\Language\YH.Virtual Simulator Engine";
            instance = RunningInstance();

            if (instance == null)
            {
                Splasher.Splash = new YH.Splash.SplashWindow();
                Splasher.ShowSplash();

                MessageListener.Instance.SetTitle(GlobalParams.AppName);

                MessageListener.Instance.SetMessage("10%");
                //Thread.Sleep(500);

                GlobalParams.LanguageCode = "en-US";  //"zh-CN","en-US"

                if (args.Length > 0)
                    GlobalParams.LanguageCode = args[0];

                LoadLanguage();

                //MessageListener.Instance.ReceiveMessage("30%");
                //Thread.Sleep(500);

                //MessageListener.Instance.ReceiveMessage("50%");
                //Thread.Sleep(500);

                //MessageListener.Instance.ReceiveMessage("70%");
                //Thread.Sleep(500);

                //MessageListener.Instance.ReceiveMessage("90%");
                //Thread.Sleep(500);

                //MessageListener.Instance.ReceiveMessage("100%");
                //Thread.Sleep(500);

                //Splasher.Splash = new SplashScreen();
                //Splasher.ShowSplash();

                for (int i = 0; i < 100; i++)
                {
                    MessageListener.Instance.SetMessage(string.Format("Load module {0}", i));
                    Thread.Sleep(10);
                }

                Splasher.CloseSplash();


                DoStartup(args);
            }
            else
            {

                HandleRunningInstance(instance);
            }
        }

        static void DoStartup(string[] args)
        {
            //启动主界面
            Application app = new Application();//WPF项目的Application实例，用来启动WPF项目的  
            MainWindow windows = new MainWindow();
            app.MainWindow = windows;
            windows.Show();
            app.Run();
        }

        public static void LoadLanguage()
        {
            YH.Language.LanguageExtend.strPathFile = GlobalParams.LanguagePathName;
            YH.Language.LanguageExtend.SetLanguage(YH.Language.LanguageExtend.strPathFile);
            YH.Language.LanguageExtend.SetLanguageCode(GlobalParams.LanguageCode);


            GlobalFuncs.LoadLanguage();
        }

        /// <summary> 
        /// 获取正在运行的实例，没有运行的实例返回null; 
        /// </summary> 
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
