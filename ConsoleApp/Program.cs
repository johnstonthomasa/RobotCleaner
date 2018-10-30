using System;
using System.IO;
using Robot;

namespace ConsoleApp
{
    class Program
    {    
        public static string nl = Environment.NewLine;
        static void Main(string[] args)
        {
            var robotImpl= new UnsafeRobot();

            var commands= GetCommands(args);

            robotImpl.IngestCommands(commands);

            Console.WriteLine("=> Cleaned: {0}",robotImpl.RunCommands());
        }

        public static string GetCommands(string[] args){
            if(null == args || args.Length ==0){
                Terminate();
            }
            var commands="";
            switch(args[0]){
                case "-c":
                    commands= args[1];
                    break;
                case "-f":
                    if(args.Length < 2 || null==args[1]){
                        Console.WriteLine("No file specified.");
                        Environment.Exit(-1);
                    }
                    commands= File.ReadAllText(args[1]);
                    break;
                case "--help":
                    Console.WriteLine("-f <file-path>: Reads input from a file."+Environment.NewLine+"-c <commands>: Take commands directly from the terminal.");
                    Environment.Exit(0);
                    break;
                case "":
                default:
                    Terminate();
                    break;
            }
            return commands;
        }

        public static void Terminate(){
            Console.WriteLine("Use --help for more information on how use this program");
            Environment.Exit(-1);
        }
    }
}
