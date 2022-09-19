using System;
using System.IO;
using System.Diagnostics;
using System.Security.AccessControl;
using System.Windows.Forms;

namespace groundward
{
    internal class Program
    {
        public static void GroundWardDiagnostic()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Diagnosticando Procesos...");
            Process[] process = Process.GetProcesses();
            foreach (var proc in process)
            {
                if (proc.ProcessName.Contains("Ground"))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Ground Kill!");
                    proc.Kill();
                }
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Procesos Seguros!");
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void GroundWardFiles(string path)
        {
            try
            {
                DirectoryInfo di = new DirectoryInfo(path);
                var files = di.GetFiles();
                foreach (var fi in files)
                {
                    try
                    {
                        string fixfilepath = $"{fi.Directory.FullName}\\g{fi.Name}";
                        if (File.Exists(fixfilepath))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"Ground {fi.Name}...");
                            //Fix Ground
                            fi.Delete();
                            File.SetAttributes(fixfilepath, FileAttributes.Archive);
                            File.Move(fixfilepath,fi.FullName);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"Ground {fi.Name} Fix...");
                        }
                    }
                    catch (Exception e){}
                }
                foreach (var dinf in di.GetDirectories())
                {
                    try
                    {
                        GroundWardFiles(dinf.FullName);
                    }catch(Exception ex){}
                }
            }catch(Exception ex){}  
        }
        public static void GroundWard(string path)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Diagnosticando Archivos...");
            GroundWardFiles(path);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Archivos Seguros!");
            Console.ForegroundColor = ConsoleColor.White;
        }
        
        public static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Iniciando GroundWard... @Copyrigth ObisoftDev");
            var currentdir = Environment.CurrentDirectory;
            GroundWardDiagnostic();
            GroundWard(currentdir);
            MessageBox.Show("Diagnostico Terminado Correctamente!\n@Copyrigth ObisoftDev");
        }
    }
}