using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace NotCMDLOL
{
    internal class Program
    {
        static List<string> extractString(string s)
        {
            List<string> salida = new List<string>();
            foreach (Match match in Regex.Matches(
            s, "\"(.*?)\""))
            {
                salida.Add(match.Value);
                salida.Add(match.Value.Replace(' ', '+'));
            }
            return salida;
        }
        static void Main(string[] args)
        {
            string path = "C:\\";
            while (true)
            {
                Console.Write(path + "> ");
                string com = Console.ReadLine();
                if (com.StartsWith("cd .."))
                {
                    string[] rutas = path.Split('\\');
                    if (rutas.Length > 2)
                    {
                        path = "";
                        for (int i = 0; i < rutas.Length - 1; i++)
                        {
                            if (i == rutas.Length - 2)
                            {
                                path += rutas[i];
                            }
                            else 
                            {
                                path += rutas[i] + "\\";
                            }
                        }
                    }
                }
                else if (com.StartsWith("dir"))
                {
                    if (com.Split(' ').Length == 1)
                    {
                        foreach (string d in Directory.GetDirectories(path))
                        {
                            Console.WriteLine("Directory: " + Path.GetFileName(d));
                        }
                        foreach (string f in Directory.GetFiles(path))
                        {
                            Console.WriteLine("File: " + Path.GetFileName(f));
                        }
                    }
                    else if (com.Split(' ').Length == 2)
                    {
                        if (Directory.Exists(com.Split(' ')[1]))
                        {
                            foreach (string d in Directory.GetDirectories(com.Split(' ')[1]))
                            {
                                Console.WriteLine("Directory: " + Path.GetFileName(d));
                            }
                            foreach (string f in Directory.GetFiles(com.Split(' ')[1]))
                            {
                                Console.WriteLine("File: " + Path.GetFileName(f));
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("The system cannot find the path specified.");
                    }
                }
                else if (com.StartsWith("cd C:") || com.StartsWith("cd D:") || com.StartsWith("cd E:") || com.StartsWith("cd F:") || com.StartsWith("cd M:") || com.StartsWith("cd H:") || com.StartsWith("cd I:"))
                {
                    string dir = com.Split(' ')[1];
                    if (com.Contains('"')) 
                    {
                        //DIRTY parser
                        List<string> extra = extractString(com);
                        for (int i = 0; i < extra.Count; i++)
                        {
                            com = com.Replace(extra[i], extra[i + 1]);
                            i++;
                        }
                        dir = com.Split(' ')[1];
                        dir = dir.Replace("+", " ").Replace("\"","");
                    }
                    if (Directory.Exists(dir))
                    {
                        path = dir;
                    }
                    else
                    {
                        Console.WriteLine("The system cannot find the path specified.");
                    }
                }
                else if (com.StartsWith("cd \\"))
                {
                    string dir = com.Split(' ')[1];
                    if (com.Contains('"'))
                    {
                        //DIRTY parser
                        List<string> extra = extractString(com);
                        for (int i = 0; i < extra.Count; i++)
                        {
                            com = com.Replace(extra[i], extra[i + 1]);
                            i++;
                        }
                        dir = com.Split(' ')[1];
                        dir = dir.Replace("+", " ").Replace("\"", "");
                    }
                    if (Directory.Exists(dir))
                    {
                        path = dir;
                    }
                    else
                    {
                        Console.WriteLine("The system cannot find the path specified.");
                    }
                }
                else if (com.StartsWith("cd"))
                {

                    string dir = com.Split(' ')[1];
                    if (com.Contains('"'))
                    {
                        //DIRTY parser
                        List<string> extra = extractString(com);
                        for (int i = 0; i < extra.Count; i++)
                        {
                            com = com.Replace(extra[i], extra[i + 1]);
                            i++;
                        }
                        dir = com.Split(' ')[1];
                        dir = dir.Replace("+", " ").Replace("\"", "");
                    }
                    if (Directory.Exists(path + "\\" + dir))
                    {
                        path += '\\' + dir;
                    }
                    else
                    {
                        Console.WriteLine("The system cannot find the path specified.");
                    }
                }
                else if (com.StartsWith("type"))
                {
                    string dir = com.Split(' ')[1];
                    if (com.Contains('"'))
                    {
                        //DIRTY parser
                        List<string> extra = extractString(com);
                        for (int i = 0; i < extra.Count; i++)
                        {
                            com = com.Replace(extra[i], extra[i + 1]);
                            i++;
                        }
                        dir = com.Split(' ')[1];
                        dir = dir.Replace("+", " ").Replace("\"", "");
                    }
                    if (com.StartsWith("type C:\\") || com.StartsWith("type D:\\") || com.StartsWith("type E:\\") || com.StartsWith("type F:\\") || com.StartsWith("type M:\\") || com.StartsWith("type H:\\") || com.StartsWith("type I:\\")) 
                    {
                        if (File.Exists(dir)) 
                        {
                            Console.WriteLine(System.IO.File.ReadAllText(dir));
                        }
                        else
                        {
                            Console.WriteLine("The system cannot find the file specified.");
                        }
                    }
                    else if (File.Exists(path + "\\" + dir))
                    {
                        Console.WriteLine(System.IO.File.ReadAllText(path + "\\" + dir));
                    }
                    else
                    {
                        Console.WriteLine("The system cannot find the file specified.");
                    }
                }
                else if (com.StartsWith("exit"))
                {
                    Environment.Exit(0);
                }
                else if (com.StartsWith("copy"))
                {
                    if (com.Split(' ').Length != 3)
                    {
                        Console.WriteLine("Syntax is: copy FILE ABSOULTE_PATH_FILE");
                    }
                    else
                    {
                        string[] rutas = com.Split(' ')[2].Split('\\');
                        string path_destino = "";
                        for (int i = 0; i < rutas.Length - 1; i++)
                        {
                            path_destino += rutas[i] + "\\";
                        }
                        if (File.Exists(path + "\\" + com.Split(' ')[1]) && Directory.Exists(path_destino))
                        {
                            try
                            {
                                File.Copy(path + "\\" + com.Split(' ')[1], com.Split(' ')[2], true);
                            }
                            catch
                            {
                                Console.WriteLine("Syntax is: copy FILE ABSOULTE_PATH_FILE");
                            }
                        }
                        else if (File.Exists(com.Split(' ')[1]) && Directory.Exists(path_destino))
                        {
                            try
                            {
                                File.Copy(com.Split(' ')[1], com.Split(' ')[2], true);
                            }
                            catch
                            {
                                Console.WriteLine("Syntax is: copy FILE ABSOULTE_PATH_FILE");
                            }
                        }
                        else
                        {
                            Console.WriteLine("The system cannot find the file specified.");
                        }
                    }
                }
                else
                {
                    string fichero = com.Split(' ')[0];
                    if (!fichero.EndsWith(".exe"))
                    {
                        fichero += ".exe";
                    }
                    string pathfile = Environment
                        .GetEnvironmentVariable("PATH")
                        .Split(';').Append(path)
                        .Where(s => File.Exists(Path.Combine(s, fichero)))
                        .FirstOrDefault();
                    string exec = "";
                    if (pathfile != null)
                    {
                        exec = pathfile + "\\" + fichero;
                    }
                    if (com.StartsWith("C:") || com.StartsWith("D:") || com.StartsWith("E:") || com.StartsWith("F:") || com.StartsWith("M:") || com.StartsWith("H:") || com.StartsWith("I:"))
                    {
                        if (File.Exists(com)) 
                        { 
                            exec = com;
                            string[] rutas = com.Split('\\');
                            path = "";
                            for (int i = 0; i < rutas.Length - 1; i++)
                            {
                                path += rutas[i] + "\\";
                            }
                        }
                    }
                    string argss = "";
                    if (com.Split(' ').Length > 1) 
                    {
                        //Arguments
                        string[] arguments = com.Split(' ');
                        for (int i = 1; i < arguments.Length; i++)
                        {
                            argss += arguments[i] + " ";
                        }
                    }

                    if (pathfile != null)
                    {
                        Process p = new Process();
                        p.StartInfo.UseShellExecute = false;
                        p.StartInfo.RedirectStandardOutput = true;
                        p.StartInfo.FileName = exec;
                        p.StartInfo.Arguments = argss;
                        p.StartInfo.WorkingDirectory = path;
                        p.Start();
                        string output = p.StandardOutput.ReadToEnd();
                        p.WaitForExit();
                        Console.WriteLine(output);
                    }
                    else
                    {
                        Console.WriteLine(com + " is not recognized as an internal or external command operable program or batch file.");
                    }
                }
            }
        }
    }
}