using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace BackupSourceCodes
{
    class Program
    {
        private const string RarPath = @"C:\Program Files\WinRAR\WinRAR.exe";
        private const string src = @"""D:\Programming\Error Control System\ErrorControlSystem\*.*"""; // directory , not the file itself
        private static string des = string.Format(@"""D:\Programming\Error Control System\Backup\ErrorControlSystem {0}.rar""", DateTime.Now.ToString("yyyy.MM.dd.HHmm"));

        public static void RarFilesAdd()
        {
            var winrar = new Process
            {
                StartInfo = { FileName = RarPath, CreateNoWindow = true, WindowStyle = ProcessWindowStyle.Normal },
                EnableRaisingEvents = true
            };
            winrar.Exited += winrar_Exited;


            winrar.StartInfo.Arguments = string.Format("A -R -m5 -mt{2} -ep1 {0} {1}", String.Format(@"{0}", des), src, Environment.ProcessorCount);

            winrar.Start();
            Console.WriteLine("Source Path to Zip: " + src);
            Console.WriteLine("Districts Path to Zip: " + des);
            Console.WriteLine("Please wait to complete the zip process . . .");
            winrar.WaitForExit();
        }

        static void winrar_Exited(object sender, EventArgs e)
        {
            Console.Clear();
            Console.WriteLine("Zipping Process Completed Successfully");
            Thread.Sleep(1000);
            var desDirectory = des.Substring(0, des.LastIndexOf(@"\") );

            Process.Start("explorer.exe", desDirectory);
            Environment.Exit(0);
        }



        static void Main(string[] args)
        {
            RarFilesAdd();
            Console.ReadKey();
        }
    }
}
