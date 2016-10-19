using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace ConsoleApplication1
    {
        class Program
        {

            static void Main(string[] args)
            {
               

                string Exe = (@"C:\Program Files\TronSolution\TSTTFrente.exe");
                string Exe64 = (@"C:\Program Files (x86)\TronSolution\TSTTFrente.exe");
                string sourceDir = @"C:\DarumaFrameWork.xml";
                string soucerDirLocal = @"C:\Program Files\TronSolution\";
                string BackupDir = @"C:\Program Files\TronSolution\DarumaFrameWork.xml";
                string BackupDir64 = @"C:\Program Files (x86)\TronSolution\DarumaFrameWork.xml";

            if (Directory.Exists(soucerDirLocal))
                {                
                 File.Copy(sourceDir, BackupDir, true);
                 File.SetAttributes(BackupDir, FileAttributes.Normal);              
                 Process.Start(Exe);
                }
                else
                {
                File.Copy(sourceDir, BackupDir64, true);
                File.SetAttributes(BackupDir, FileAttributes.Normal);
                Process.Start(Exe64);
                }

            }
        }
    }

