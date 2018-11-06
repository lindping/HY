using IWshRuntimeLibrary;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YH.MetroTile
{
    public class IconsAndPaths
    {
        public Dictionary<string, string[]> IconsPathsDi = new Dictionary<string, string[]> { };

        public Dictionary<string, string[]> GetIconsAndPaths()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu) + @"\Programs";
            DirectoryInfo startMenuProgDir = new DirectoryInfo(path);

            if (startMenuProgDir.Exists != true)
            {
                string dirPath = Environment.GetFolderPath(Environment.SpecialFolder.StartMenu) + @"\Programs";
                startMenuProgDir = new DirectoryInfo(dirPath);
            }

            WshShell shell = new WshShell();

            CreateIconsDirectory();

            foreach (FileInfo fi in startMenuProgDir.GetFiles())
            {
                if (fi.Extension == ".lnk")
                {
                    //' The length of the file's name alone minus .lnk
                    int nameLength = fi.Name.Length - 4;
                    //' Name to display in UserControl
                    string displayName = fi.Name.Substring(0, nameLength);
                    //' Copy of shortcut
                    IWshShortcut link = (IWshShortcut)shell.CreateShortcut(fi.FullName);

                    string potentialExePath = link.TargetPath;
                    FileInfo potentialExe = new FileInfo(potentialExePath);

                    if (potentialExe.Extension == ".exe")
                    {
                        string tileIconPath = Environment.CurrentDirectory + @"\WPF Metro Icons\" + displayName + @".png";
                        try
                        {
                            Icon ico = Icon.ExtractAssociatedIcon(potentialExePath);
                            ico.ToBitmap().Save(tileIconPath, System.Drawing.Imaging.ImageFormat.Png);

                            AddToDictionary(displayName, tileIconPath, potentialExePath);
                        }
                        catch (FileNotFoundException ex)
                        {

                        }
                    }
                }
            }

            foreach (DirectoryInfo di in startMenuProgDir.GetDirectories())
            {
                foreach (FileInfo fi in di.GetFiles())
                {
                    if (fi.Extension == ".lnk")
                    {
                        //' The length of the file's name alone minus .lnk
                        int nameLength = fi.Name.Length - 4;
                        //' Name to display in UserControl
                        string displayName = fi.Name.Substring(0, nameLength);
                        //' Avoid install and uninstall files
                        if (displayName.Contains("install") != true)
                        {
                            IWshShortcut link = (IWshShortcut)shell.CreateShortcut(fi.FullName);
                            string potentialExePath = link.TargetPath;


                            if (potentialExePath.Contains(".exe"))
                            {
                                string tileIconPath = Environment.CurrentDirectory + @"\WPF Metro Icons\" + displayName + @".png";
                                try
                                {
                                    Icon ico = Icon.ExtractAssociatedIcon(potentialExePath);
                                    ico.ToBitmap().Save(tileIconPath, System.Drawing.Imaging.ImageFormat.Png);


                                    CheckMSOfficeApps(displayName, tileIconPath);


                                    AddToDictionary(displayName, tileIconPath, potentialExePath);

                                }
                                catch (FileNotFoundException ex)
                                {
                                    //Exit Try
                                }
                                catch (ArgumentException ex)
                                {
                                    //Exit Try
                                }
                            }
                        }
                    }
                }
            }

            return IconsPathsDi;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="displayName"></param>
        /// <param name="tileIconPath"></param>
        /// <param name="exePath"></param>
        public void AddToDictionary(string displayName, string tileIconPath, string exePath)
        {
            if (!IconsPathsDi.ContainsKey(displayName))
                IconsPathsDi.Add(displayName, new string[] { tileIconPath, exePath });
        }

        private void CheckMSOfficeApps(string app, string tileIconPath)
        {
            if (app.Contains("Microsoft Office Access"))
                AddToDictionary(app, tileIconPath, "MSACCESS.EXE");
            else if (app.Contains("Microsoft Office Excel"))
                AddToDictionary(app, tileIconPath, "EXCEL.EXE");
            else if (app.Contains("Microsoft Office InfoPath"))
                AddToDictionary(app, tileIconPath, "INFOPATH.EXE");
            else if (app.Contains("Microsoft Office OneNote"))
                AddToDictionary(app, tileIconPath, "ONENOTEM.EXE");
            else if (app.Contains("Microsoft Office Outlook"))
                AddToDictionary(app, tileIconPath, "OUTLOOK.EXE");
            else if (app.Contains("Microsoft Office PowerPoint"))
                AddToDictionary(app, tileIconPath, "POWERPNT.EXE");
            else if (app.Contains("Microsoft Office Publisher"))
                AddToDictionary(app, tileIconPath, "MSPUB.EXE");
            else if (app.Contains("Microsoft Office Word"))
                AddToDictionary(app, tileIconPath, "WINWORD.EXE");
            else
                return;

        }

        private void CreateIconsDirectory()
        {
            string dir = Environment.CurrentDirectory + @"\WPF Metro Icons";
            if (Directory.Exists(dir))
            {
                DirectoryInfo di = new DirectoryInfo(dir);
                foreach (FileInfo fi in di.GetFiles())
                {
                    fi.Delete();
                }
            }
            else
                Directory.CreateDirectory(dir);
        }
    }

}
