using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using YH.MetroTile;
namespace YH.Simulation_Home
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static string[] args;

        [DllImport("User32.dll")]
        public static extern int ShowWindowAsync(IntPtr hWnd, int nCmdShow);

        Mutex mutext; //used to prevent multiple instances of the application from running at the same time


        private void Application_Startup(object sender, StartupEventArgs e)
        {
            args = e.Args;
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            if (mutext != null)
                mutext.ReleaseMutex();

        }

        public void checkMutex()
        {
            mutext = new Mutex(false, "ImmersiveExplorer");
            if (!mutext.WaitOne(0))
            {
                Process[] runningProcesses = Process.GetProcesses();

                for (int i = 0; i < runningProcesses.Length; i++)
                {
                    if (runningProcesses[i].ProcessName.StartsWith("Immersive Explorer")) //search by process name
                    {
                        if (Process.GetCurrentProcess().Id != runningProcesses[i].Id) //if this is NOT the current process, then we can bring the window to front
                        {
                            if (runningProcesses[i].MainWindowHandle != IntPtr.Zero)
                            {
                                ShowWindowAsync(runningProcesses[i].MainWindowHandle, 6); //minimize
                                ShowWindowAsync(runningProcesses[i].MainWindowHandle, 1); //show
                            }
                        }
                    }
                }
                mutext = null;
                Application.Current.Shutdown(0);
            }
        }
    }
}
