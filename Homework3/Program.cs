using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataSystemHomework3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var Folder = string.Empty;
            Folder = "C:\\Users\\Stanislav\\Desktop\\Size";
            int diff1 = FolderMng(new DirectoryInfo(Folder));
            long diff2 = FolderSize(new DirectoryInfo(Folder));
            Console.WriteLine($"Исходный размер папки: {FolderSize(new DirectoryInfo(Folder))} байт");
            DeleteInDelay(Folder);
            Console.WriteLine($"Удалено {diff1} файлов");
            Console.WriteLine($"Освобождено {diff2} байт");
            Console.WriteLine($"Текущий размер папки: {FolderSize(new DirectoryInfo(Folder))} байт");
            Console.ReadKey();
        }

        static void DeleteInDelay(string Path)
        {
            try
            {
                if (Directory.Exists(Path))
                {
                    DirectoryInfo directory = new DirectoryInfo(Path); // Создаем объект класса DirectoryInfo
                    FileInfo fileInfo = new FileInfo(Path); // Создаем объект класса FileInfo
                    TimeSpan delay = TimeSpan.FromMinutes(30); // Указываем временной интервал
                    foreach (FileInfo file in directory.GetFiles())
                    {
                        file.Delete();
                    }
                    foreach (DirectoryInfo directoryInfo in directory.GetDirectories())
                    {
                        directoryInfo.Delete(true);
                    }
                }
                // Если папка не была найдена, то выведем сообщение об этом
                if (!Directory.Exists(Path))
                {
                    Console.WriteLine("Папка по указанному адресу не существует");
                }
            }
            // обработка исключений
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static long FolderSize(DirectoryInfo Path)
        {
            try
            {
                long size = 0;
                DirectoryInfo[] directoryInfo = Path.GetDirectories();
                FileInfo[] fileInfo = Path.GetFiles();
                foreach (FileInfo file in fileInfo)
                {
                    size += file.Length;
                }
                foreach (DirectoryInfo directoryInfo1 in directoryInfo)
                {
                    size += FolderSize(directoryInfo1);
                }
                return size;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e}");
                return 0;
            }
        }

        static int FolderMng(DirectoryInfo Path)
        {
            try
            {
                DirectoryInfo[] directorys = Path.GetDirectories();
                FileInfo[] files = Path.GetFiles();
                int Mng = 0;
                Mng = files.Length;
                foreach (DirectoryInfo directory in directorys)
                {
                    Mng += FolderMng(directory);
                }
                return Mng;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }

        }
    }
}
