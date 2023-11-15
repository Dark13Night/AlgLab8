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