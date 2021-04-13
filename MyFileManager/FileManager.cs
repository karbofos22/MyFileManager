using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FileManager
{
    public class FileManager
    {
        public string path;

        public FileManager()
        {
        }
        public static void ShowDrives(string userInput)
        {
            DriveInfo[] allDrives = DriveInfo.GetDrives();
            foreach (DriveInfo drives in allDrives)
            {
                Console.WriteLine("Drive {0}", drives.Name);
            }
            Console.WriteLine();
            Console.WriteLine($"Список корневых папок диска: {userInput}");
            Console.WriteLine("│");
            DirTree(userInput, "");
        }
        public static void CommandsList(out string userInput)
        {
            Console.WriteLine(@"Список команд для работы с файловым менеджером:
                     1. Для просмотра доступных физических дисков наберите disc и нажмите Enter.
                     2. Для просмотра содержимого папки/диска используйте ключевое слово cd + имя папки/диска. Пример: cd temp
                     3. 
                     4.
                     5. Что вы хотите сделать?");

            Console.WriteLine();
            userInput = Console.ReadLine();
            Console.WriteLine();

            
        }
        public static void UserCommands(string userInput)
        {
            string path = userInput.Substring(3) + ":\\";
            int startIndex = 0;
            int length = 2;
            userInput = userInput.Substring(startIndex, length);

            switch (userInput)
            {
                case "disc":
                    ShowDrives(userInput);
                    break;
                case "cd":
                case "Cd":
                    DirTree(path, "");
                    FileTree(path);
                    break;
            }
            userInput = Console.ReadLine();
            UserCommands(userInput);
        }

        public static void DirTree(string path, string sidestep)
        {
            try
            {
                Directory.SetCurrentDirectory(path);
                DirectoryInfo input = new(path);
                DirectoryInfo[] dirList = input.GetDirectories(); //Список каталогов в папке

                foreach (DirectoryInfo dir in dirList)
                {
                    if (dir == dirList.Last())
                        Console.WriteLine(sidestep + "└──" + dir + dir.ToString().Length);
                    else
                        Console.WriteLine(sidestep + "├──" + dir);
                }
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("Simon says: You cant see this");
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine("Simon says: No such directory");
            }
        }
        public static void FileTree(string path)
        {
            int separatorCount = 135;
            char sym = '=';
            for (int i = 0; i < separatorCount; i++)
            {
                Console.Write(sym);
            }
            Console.WriteLine();

            try
            {
                DirectoryInfo input = new DirectoryInfo(path);
                FileInfo[] fileList = input.GetFiles();  //Список файлов в папке
                Console.WriteLine(string.Format("{0,-65}  {1, -44}  {2, 20}", "File name:", "File attributes:", "File size:"));
                Console.WriteLine();

                foreach (FileInfo file in fileList)
                {
                    Console.WriteLine("{0,-65} {1, -46} {2, 20}", file.Name, file.Attributes, file.Length);
                }
            }
            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine($"Ошибка: {e.Message}");
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine($"Ошибка: {e.Message}");
            }

        }
        public static void DirCreateMethod()
        {
            Console.WriteLine("Для создания папки используйте ключевое слово create");

            string userCommand = Console.ReadLine();

            Console.WriteLine("Укажите название папки");
            string name = Console.ReadLine();
            Directory.CreateDirectory(name);

            //switch ()
            //{
            //    case "create":
            //        FileManager.DirCreateMethod();
            //        break;
            //    case "see":
            //        FileManager.SeeFolder();
            //        break;
            //}
        }
        public static void FileCopyMethod()
        {

        }
    }
}


