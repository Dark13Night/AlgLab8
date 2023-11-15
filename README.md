# AlgLab8
# Лабораторная работа №8
«Файловая система. Сериализация»

Цели работы:
Научиться работать с механизмом сериализации средствами языка C#.
Научиться работать с файловой системой средствами языка C#.


# Задание№1
Используя диаграмму классов из лабораторной работы №7, реализуйте поддержку сериализации и десериализации класса Animal.

Создайте экземпляр класса Animal, инициализируйте все поля и выполните его Xml-сериализацию.
	Добавьте в решение новый консольный проект, в котором десериализуйте класс Animal и выведите полученный объект на консоль.

 Class
 ```
using System;
using System.Diagnostics.Metrics;
using System.Runtime.Serialization;
using System.Xml.Linq;

namespace ClassLibrary1
{
    [Serializable]
    public abstract class Animal : ISerializable
    {
        public string Name { get; set; }
        public string Country { get; set; }

        public abstract string GetFavouriteFood();
        public virtual void SayHello()
        {
            Console.WriteLine($"Hello from {Name}!");
        }

        public Animal()
        { }

        protected Animal(SerializationInfo info, StreamingContext context)
        {
            Name = info.GetString("Name");
            Country = info.GetString("Country");
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Name", Name);
            info.AddValue("Country", Country);
        }
    }
    public enum ClassificationAnimal
    {
        Herbivores,
        Carnivores,
        Omnivores
    }

    // Перечисление FavouriteFood
    public enum FavouriteFood
    {
        Meat,
        Plants,
        Everything
    }

    [Serializable]
    public class Cow : Animal
    {
        public ClassificationAnimal Classification { get; set; }

        public override string GetFavouriteFood()
        {
            return FavouriteFood.Plants.ToString();
        }
    }

    [Serializable]
    public class Lion : Animal
    {
        public ClassificationAnimal Classification { get; set; }

        public override string GetFavouriteFood()
        {
            return FavouriteFood.Meat.ToString();
        }
    }

    [Serializable]
    public class Pig : Animal
    {
        public ClassificationAnimal Classification { get; set; }

        public override string GetFavouriteFood()
        {
            return FavouriteFood.Everything.ToString();
        }
    }
   
}
```

Program
```
using ClassLibrary1;
using System;
using System.IO;
using System.Xml.Serialization;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            // Создаем экземпляр класса Animal
            Animal animal = new Cow
            {
                Name = "Bessie",
                Country = "USA",
                Classification = ClassificationAnimal.Herbivores
            };

            // Выполняем Xml-сериализацию
            XmlSerializer serializer = new XmlSerializer(typeof(Animal), new Type[] { typeof(Cow), typeof(Lion), typeof(Pig) });
            using (TextWriter writer = new StreamWriter("animal.xml"))
            {
                serializer.Serialize(writer, animal);
            }

            // Десериализуем класс Animal и выводим полученный объект на консоль
            Animal deserializedAnimal;
            using (TextReader reader = new StreamReader("animal.xml"))
            {
                deserializedAnimal = (Animal)serializer.Deserialize(reader);
            }

            Console.WriteLine($"Name: {deserializedAnimal.Name}");
            Console.WriteLine($"Country: {deserializedAnimal.Country}");
            Console.WriteLine($"Favourite Food: {deserializedAnimal.GetFavouriteFood()}");

            Console.ReadLine();
        }
    }
}
```
# Задание№2
Напишите приложение для поиска заданного файла во всех поддиректориях указанного пользователем пути.
Используйте FileStream для вывода содержимого файла на консоль.
Добавьте возможность сжатия найденного файла стандартными средствами .Net.
```
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
```

