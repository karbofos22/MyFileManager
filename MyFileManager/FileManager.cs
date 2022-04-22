using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MyFileManager
{
    public class FileManager
    {

        private static string Path { get; set; }

       
        private static void ShowDrives()
        {
            DriveInfo[] allDrives = DriveInfo.GetDrives();
            foreach (DriveInfo drives in allDrives)
            {
                Console.WriteLine("Drive {0}", drives.Name);
            }
            Console.WriteLine();
            
        }
        private static void InfoList()
        {
            Console.WriteLine(@"Список команд и справочная информация для работы с файловым менеджером:
              1. Для просмотра доступных физических дисков наберите dcs и нажмите Enter
              2. Для просмотра содержимого папки/диска используйте ключевое слово see + имя папки/диска. Пример: see temp или see d
                     Троеточие( ... ) после названия папки, означет, что папка не пуста
                     Под списком папок расположен список файлов текущей папки
              3. Для создания новой папки используйте ключевое слово crt + имя новой папки
              4. Для удаления папки используйте ключевое слово del + имя папки
              5. Для просмотра данной инормации еще раз, наберите inf и нажмите Enter");
            Console.WriteLine();
        }
        private static void UserCommands()
        {
            while (true)
            {
                try
                {
                    string userInput = Console.ReadLine();
                    //string path = "";
                    Console.Clear();
                    Console.WriteLine("\t\t\t\tДля просмотра справочной информации еще раз, наберите inf и нажмите Enter");
                    Console.WriteLine();
                    if (userInput.Length > 3 && userInput.Length < 6)
                    {
                        Path = userInput.Substring(4) + @":\";
                    }
                    else if (userInput.Length > 5)
                    {
                        Path = Directory.GetCurrentDirectory() + @"\" + userInput.Substring(4) + @"\";
                    }

                    int startIndex = 0;
                    int length = 3;
                    userInput = userInput.Substring(startIndex, length);

                    switch (userInput)
                    {
                        case "inf":
                            InfoList();
                            break;
                        case "dcs":
                            ShowDrives();
                            break;
                        case "see":
                        case "See":
                        case "SEE":
                            DirTree("");
                            FileTree();
                            Console.WriteLine();
                            break;
                        case "crt":
                        case "Crt":
                        case "CRT":
                            Create();
                            break;
                        case "del":
                        case "Del":
                        case "DEL":
                            Delete();
                            break;
                        default:
                            Console.WriteLine("Команда не распознана, попробуйте еще раз. Для просмотра справки о командах еще раз, наберите inf и нажмите Enter ");
                            break;
                    }
                }
                catch (ArgumentOutOfRangeException)
                {
                    Console.WriteLine("Команда не распознана, попробуйте еще раз. Для просмотра справки о командах еще раз, наберите inf и нажмите Enter ");
                }
            }
        }

        private static void DirTree(string sidestep)
        {
            try
            {
                Console.WriteLine(Path);
                Console.WriteLine("│");
                Directory.SetCurrentDirectory(Path);
                DirectoryInfo input = new(Path);
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
                Console.WriteLine("Ошибка: Вы не можете это увидеть(недостаточно доступа)");
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
        private static void FileTree()
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
                DirectoryInfo input = new(Path);
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
            Console.WriteLine();
        }
        private static void Create()
        {
            try
            {
                Directory.CreateDirectory(Path);
                Path = Path.Substring(0,1) + @":\";
                DirTree("");
                FileTree();

            }
            catch (IOException)
            {
                Console.WriteLine("Вы не ввели название новой папки");
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Вы не ввели название новой папки");
            }
        }
        private static void Delete()
        {
            
            try
            {
                Directory.Delete(Path, true);
                Path = Path.Substring(0, 1) + @":\";
                DirTree("");
                FileTree();

            }
            catch (IOException)
            {
                Console.WriteLine("Папка не найдена");
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Папка не найдена");
            }

        }

        public static void ManagerStart()
        {
            InfoList();
            UserCommands();
        }

    }
}


