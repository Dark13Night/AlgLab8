using System;
using System.IO;
using System.IO.Compression;

class Program
{
    static void Main()
    {
        Console.Write("Введите путь, в котором нужно найти файлы: ");
        string directoryPath = Console.ReadLine();
        Console.Write("Введите имя файла, которое нужно найти: ");
        string fileName = Console.ReadLine();

        SearchFiles(directoryPath, fileName);
    }

    static void SearchFiles(string directoryPath, string fileName)
    {
        try
        {
            string[] files = Directory.GetFiles(directoryPath, fileName, SearchOption.AllDirectories);

            if (files.Length == 0)
            {
                Console.WriteLine("Файлы не найдены.");
                return;
            }

            Console.WriteLine("Найденные файлы:");

            foreach (string filePath in files)
            {
                Console.WriteLine(filePath);
                PrintFileContent(filePath);
                CompressFile(filePath);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }

    static void PrintFileContent(string filePath)
    {
        try
        {
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
            {
                using (StreamReader streamReader = new StreamReader(fileStream))
                {
                    Console.WriteLine(streamReader.ReadToEnd());
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при чтении файла: {ex.Message}");
        }
    }

    static void CompressFile(string filePath)
    {
        try
        {
            string compressedFilePath = filePath + ".gz";

            using (FileStream inputFileStream = new FileStream(filePath, FileMode.Open))
            {
                using (FileStream compressedFileStream = File.Create(compressedFilePath))
                {
                    using (GZipStream compressionStream = new GZipStream(compressedFileStream, CompressionMode.Compress))
                    {
                        inputFileStream.CopyTo(compressionStream);
                    }
                }
            }

            Console.WriteLine($"Сжатый файл создан: {compressedFilePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при сжатии файла: {ex.Message}");
        }
    }
}