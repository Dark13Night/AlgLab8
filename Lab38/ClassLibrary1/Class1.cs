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