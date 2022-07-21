using System;
using System.Collections.Generic;

namespace Zoo
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Animal> cats = new List<Animal>() { new Animal("Тигр", "Рык", "М"), new Animal("Лев", "Роар", "Ж") };
            List<Animal> monkeys = new List<Animal>() { new Animal("Бабуин", "Ууу", "Ж"), new Animal("Макака", "Ааа", "Ж") };
            List<Animal> bears = new List<Animal>() { new Animal("Белый медведь", "Уаа", "М"), new Animal("Гризли", "Грр", "М") };
            List<Animal> snakes = new List<Animal>() { new Animal("Анаконда", "Ссс", "Ж"), new Animal("Королевская кобра", "Шшш", "Ж") };
            List<Cage> cages = new List<Cage>() { new Cage("Кошки", cats), new Cage("Обезъяны", monkeys), new Cage("Медведи", bears), new Cage("Серпентарий", snakes) };
            Zoo zoo = new Zoo(cages);
            zoo.ShowMenu();
        }
    }

    class Zoo
    {
        private List<Cage> _cages = new List<Cage>();
        private bool _isZooOpening = true;

        public Zoo(List<Cage> cages)
        {
            _cages = cages;
        }

        public void ShowMenu()
        {
            while (_isZooOpening)
            {
                string userInput;
                Console.WriteLine("Добро пожаловать в Зоопарк. Выберите вольер и узнайте что там за животные. 1 - кошки, 2 - обезъяны, 3 - медведи, 4 - змеи.");
                userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "1":
                        ShowCageInfo(0);
                        break;
                    case "2":
                        ShowCageInfo(1);
                        break;
                    case "3":
                        ShowCageInfo(2);
                        break;
                    case "4":
                        ShowCageInfo(3);
                        break;
                }
            }
        }

        public void ShowCageInfo(int cageNumber)
        {
            for (int i = 0; i < _cages.Count; i++)
            {
                if (i == cageNumber)
                {
                    _cages[i].ShowCategoryInfo();
                }
            }
        }
    }

    class Cage
    {
        private List<Animal> _animals = new List<Animal>();
        public string Category { get; private set; }

        public Cage(string category, List<Animal> animals)
        {
            _animals = animals;
            Category = category;
        }

        public void ShowCategoryInfo()
        {
            Console.WriteLine($"Это вольер с {Category}. Здесь {_animals.Count} животных");

            foreach (Animal animal in _animals)
            {
                animal.ShowInfo();
            }
        }
    }

    class Animal
    {
        public string Name { get; private set; }
        public string Sound { get; private set; }
        public string Male { get; private set; }

        public Animal(string name, string sound, string male)
        {
            Name = name;
            Sound = sound;
            Male = male;
        }

        public void ShowInfo()
        {
            Console.WriteLine($"Животное: {Name}. Пол: {Male}. Издает звук {Sound}");
        }
    }
}
