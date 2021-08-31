using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LP_Updater.exe
{
    class Program
    {
        static string APPpath = AppDomain.CurrentDomain.BaseDirectory;
        static string LPpath = Path.Combine(APPpath, "Liberty Miner GUI.exe");
        static string DLL1 = Path.Combine(APPpath, "Plugin.Connectivity.Abstractions.dll");
        static string DLL2 = Path.Combine(APPpath, "Plugin.Connectivity.dll");
        static void Main(string[] args)
        {
            Console.WriteLine("Closing hazardous apps...");
            CloseProcesses();
            while (isLPrunning("Liberty Miner GUI"))
            {
                System.Threading.Thread.Sleep(2000);
            }
            Console.WriteLine("Deleting the old LP GUI...");
            File.Delete(LPpath);
            Console.WriteLine("Extracting fixed LP GUI...");
            File.WriteAllBytes(LPpath, Properties.Resources.Liberty_Miner_GUI);
            Console.WriteLine("Deleting unnecessary DLLs...");
            if (File.Exists(DLL1)) File.Delete(DLL1);
            if (File.Exists(DLL2)) File.Delete(DLL2);
            Console.WriteLine("Opening the new LP  GUI...");
            Process.Start(LPpath);
            Environment.Exit(0);
            Console.ReadLine();
        }
        static void CloseProcesses()
        {
            Process[] processes = Process.GetProcessesByName("Liberty Miner GUI");
            foreach (Process p in processes)
            {
                p.Kill();
            }
        }
        static bool isLPrunning(string name)
        {
            Process[] pname = Process.GetProcessesByName(name);
            if (pname.Length > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
