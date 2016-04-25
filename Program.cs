using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace IPLIST2UFW
{
    class Program
    {
        static void Main(string[] args)
        {
            int loop = 1;
            while (loop == 1)
            {
                Console.WriteLine("+----------------------------------------------------------+");
                Console.WriteLine("|--------------IPLIST TO UFW RULE GENERATOR----------------|");
                Console.WriteLine("|-----------FILE NAMES MUST BE GIVEN WITH EXTENSION--------|");
                Console.WriteLine("|--------------------(EX: IPLIST.TXT)----------------------|");
                Console.WriteLine("|-------------IPs MUST BE SEPARATED BY LINES---------------|");
                Console.WriteLine("|------------------NET MASKS ARE ALLOWED-------------------|");
                Console.WriteLine("|-------------------(EX: 127.0.0.1/24)---------------------|");
                Console.WriteLine("+----------------------------------------------------------+");
                string localpath = Environment.CurrentDirectory;
                Console.Write("Insert INPUT file name: ");
                string fileInput = Console.ReadLine();

                string inputPath = (localpath + "\\" + fileInput);
                if (File.Exists(inputPath))
                {

                }
                else
                {
                    Console.WriteLine("OOOoooops. File not found.");
                    return;
                }
                Console.WriteLine("+----------------------------+");
                Console.WriteLine("|------CHOOSE OPERATION------|");
                Console.WriteLine("| 1 FOR DROP OR 2 FOR ACCEPT |");
                Console.WriteLine("+----------------------------+");
                int operation = Int32.Parse(Console.ReadLine());
                Console.Write("Insert OUTPUT file name: ");
                string fileOutput = Console.ReadLine();
                string outputPath = (localpath + "\\" + fileOutput);
                if (operation == 1)
                {
                    string[] lines = File.ReadLines(inputPath).ToArray();

                    using (StreamWriter writetext = new StreamWriter(outputPath))
                    {
                        for (int i = 0; i < lines.Length; i++)
                        {
                            writetext.WriteLine("### tuple ### deny any any 0.0.0.0/0 any " + lines[i]);
                            writetext.WriteLine("-A ufw-user-input -s " + lines[i] + " -j DROP");
                            writetext.WriteLine("");
                            Console.WriteLine("RULE" + i + " CREATED.");
                        }
                        Console.WriteLine("DONE");
                        Console.Write("Type 'X' to exit or any other key to convert another list: ");
                        string doRepeat = Console.ReadLine();
                        if (doRepeat == "x" || doRepeat == "X")
                        {
                            Environment.Exit(0);
                        }
                        else
                        {
                            Console.Clear();
                        }
                    }
                }

                if (operation == 2)
                {
                    string[] lines = File.ReadLines(inputPath).ToArray();

                    using (StreamWriter writetext = new StreamWriter(outputPath))
                    {
                        for (int i = 0; i < lines.Length; i++)
                        {
                            writetext.WriteLine("### tuple ### allow any any 0.0.0.0/0 any " + lines[i] + " in");
                            writetext.WriteLine("-A ufw-user-input -s " + lines[i] + " -j ACCEPT");
                            writetext.WriteLine("");
                            Console.WriteLine("RULE" + i + " CREATED.");
                        }
                        Console.WriteLine("DONE");
                        Console.Write("Type 'X' to exit or any other key to convert another list: ");
                        string doRepeat = Console.ReadLine();
                        if (doRepeat == "x" || doRepeat == "X")
                        {
                            Environment.Exit(0);
                        }
                        else
                        {
                            Console.Clear();
                        }
                    }
                }
            }
        }
    }
}
