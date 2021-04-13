using System;
using System.IO;

namespace MyFileManager
{
    class Program
    {
        static void Main(string[] args)
        {
            FileManager.CommandsList(out string userInput);
            FileManager.UserCommands(userInput);
            Console.WriteLine();
            
            


           




        }


    }
}
