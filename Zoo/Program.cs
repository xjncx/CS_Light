using System;
using System.Collections.Generic;

//1.zoo.ShowMenu(); -зоопарка не отвечает только за показ. Здесь полностью Work() 
//4. "Выберите вольер и узнайте что там за животные. 1 - кошки, 2 - обезъяны, 3 - медведи, 4 - змеи." - а откуда вы знаете что именно такая очередность будет получена в класс? И switch (userInput) { case "1": ShowCageInfo(0); -это большой дубляж кода.У вас есть список, перебрав через цикл у вольера можно узнать имя и его вывести. Дальше запросив индекс, проверить со списком, а есть ли такой элемент.И тогда методу и зоопарку всё равно сколько вольеров, хоть 1000, код будет всё тот же и так же работать.


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
            zoo.Work();
        }
    }

    class Zoo
    {
        private List<Cage> _cages = new List<Cage>();

        public Zoo(List<Cage> cages)
        {
            _cages = cages;
        }

        public void Work()
        {
            bool _isOpening = true;

            while (_isOpening)
            {
                string userInput;
                Console.WriteLine("Добро пожаловать в Зоопарк. Выберите вольер и узнайте что там за животные.");
                ShowCagesName();
                userInput = Console.ReadLine();

                if (Int32.TryParse(userInput, out int userChoice))
                {
                    if (userChoice < _cages.Count)
                    {
                        ShowCageInfo(userChoice);
                    }
                    else
                    {
                        Console.WriteLine("Такой клетки нет");
                    }
                }
                else
                {
                    Console.WriteLine("Вы ввели не число");
                }
            }
        }

        public void ShowCagesName()
        {
            for (int i = 0; i < _cages.Count; i++)
            {
                Console.WriteLine(i + 1 + " " + _cages[i].Category);
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
        public string Sex { get; private set; }

        public Animal(string name, string sound, string sex)
        {
            Name = name;
            Sound = sound;
            Sex = sex;
        }

        public void ShowInfo()
        {
            Console.WriteLine($"Животное: {Name}. Пол: {Sex}. Издает звук {Sound}");
        }
    }
}
