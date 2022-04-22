using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MyFileManager
{
    public class FileManager
    {
        public string path;

        public FileManager()
        {
        }
        public static void ShowDrives()
        {
            DriveInfo[] allDrives = DriveInfo.GetDrives();
            foreach (DriveInfo drives in allDrives)
            {
                Console.WriteLine("Drive {0}", drives.Name);
            }
            Console.WriteLine();
            
        }
        public static void InfoList()
        {
            Console.WriteLine(@"Список команд и справочная информация для работы с файловым менеджером:
              1. Для просмотра доступных физических дисков наберите ds и нажмите Enter
              2. Для просмотра содержимого папки/диска используйте ключевое слово cd + имя папки/диска. Пример: cd temp или cd d
                     Троеточие( ... ) после названия папки, означет, что папка не пуста
                     Под списком папок расположен список файлов текущей папки
              3. Для просмотра данной инормации еще раз, наберите if и нажмите Enter. ");
            Console.WriteLine();
        }
        public static void UserCommands()
        {
            while (true)
            {
                try
                {
                    string userInput = Console.ReadLine();
                    string path = "";
                    Console.Clear();
                    Console.WriteLine("\t\t\t\tДля просмотра справочной информации еще раз, наберите if и нажмите Enter");
                    Console.WriteLine();
                    if (userInput.Length > 2 && userInput.Length < 5)
                    {
                        path = userInput.Substring(3) + ":\\";
                    }
                    else if (userInput.Length > 5)
                        path = Directory.GetCurrentDirectory() + "\\" + userInput.Substring(3) + "\\";
                    int startIndex = 0;
                    int length = 2;
                    userInput = userInput.Substring(startIndex, length);

                    switch (userInput)
                    {
                        case "if":
                            InfoList();
                            break;
                        case "ds":
                            ShowDrives();
                            break;
                        case "cd":
                        case "Cd":
                        case "CD":
                            Console.WriteLine(path);
                            Console.WriteLine("│");
                            DirTree(path, "");
                            FileTree(path);
                            Console.WriteLine();
                            break;
                        default:
                            Console.WriteLine("Команда не распознана, попробуйте еще раз. Для просмотра справки о командах еще раз, наберите if и нажмите Enter ");
                            break;
                    }
                }
                catch (ArgumentOutOfRangeException)
                {
                    Console.WriteLine("Команда не распознана, попробуйте еще раз. Для просмотра справки о командах еще раз, наберите if и нажмите Enter ");
                }
            }
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
                    if (dir == dirList.Last() && dir.GetDirectories().Length != 0)
                        Console.WriteLine(sidestep + "└──" + dir.Name + " ...");
                    else
                        Console.WriteLine(sidestep + "├──" + dir.Name + " ...");
                }
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("Ошибка: Вы не можете это увидеть(секретные данные системы))");
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine("Ошибка: такой директории нет");
            }
            catch
            {
                Console.WriteLine("Ошибка: не указан путь");
            }
            
        }
        public static void FileTree(string path)
        {
            int separatorCount = 136;
            char sym = '=';
            for (int i = 0; i < separatorCount; i++)
            {
                Console.Write(sym);
            }
            Console.WriteLine();

            try
            {
                DirectoryInfo input = new(path);
                FileInfo[] fileList = input.GetFiles();  //Список файлов в папке
                Console.WriteLine("{0,-69}  {1, -42}  {2, 20}", "File name:", "File attributes:", "File size:");
                Console.WriteLine();

                foreach (FileInfo file in fileList)
                {
                    if (file.Name.Length > 70)
                    {
                        int startIndex = 0;
                        int length = 60;
                        Console.WriteLine("{0,-70} {1, -43} {2, 20}", file.Name.Substring(startIndex, length) + "...", file.Attributes, file.Length);
                    }
                    else
                        Console.WriteLine("{0,-70} {1, -43} {2, 20}", file.Name, file.Attributes, file.Length);
                }
            }
            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine($"Ошибка: Указано неверное имя папки\n{e.Message} ");
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine($"Ошибка: {e.Message}");
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine($"Ошибка: {e.Message}");
            }
            catch
            {
                Console.WriteLine("Ошибка: не указан путь");
            }
        }
        public static void CopyMethod()
        {
           
           
           
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
        public static void DeleteMethod()
        {

        }
    }
}


