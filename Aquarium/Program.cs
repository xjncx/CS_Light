using System;
using System.Collections.Generic;

namespace Aquarium
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Fish> fish = new List<Fish>() { new Fish(2, 4), new Fish(1, 3), new Fish(4, 6) };
            Aquarium aquarium = new Aquarium(fish, 4);
            aquarium.ShowMenu();
        }
    }

    class Fish
    {
        public int CurrentAge { get; private set; }
        public int MaximunAge { get; private set; }
        public bool IsAlive { get; private set; }
        Random _random = new Random();
        private int _maxFishAge = 10;
        private int _minFishAge = 1;

        public Fish(int age = 1, int maximunAge = 1)
        {
            CurrentAge = age;
            MaximunAge = maximunAge;
            IsAlive = true;
        }

        public void ShowStats()
        {
            Console.WriteLine($"Статус живости рыбки: {IsAlive}. Возраст рыбки: {CurrentAge}");
        }

        public Fish Create()
        {
            int age = _random.Next(_minFishAge, _maxFishAge);
            int maximunAge = _random.Next(_maxFishAge, 15);
            Fish fish = new Fish(age, maximunAge);
            return fish;
        }

        public void AddDay()
        {
            CurrentAge++;
        }

        public void Kill()
        {
            IsAlive = false;
        }
    }

    class Aquarium
    {
        private List<Fish> _fish;
        private int _capacity;

        public Aquarium(List<Fish> fish, int capacity)
        {
            _fish = fish;
            _capacity = capacity;
        }

        public void ShowMenu()
        {
            bool isRunning = true;

            while (isRunning)
            {
                CheckFishAge();
                ShowFishStats();
                Console.WriteLine("Выберите действие с рыбками:\n1. Добавить рыбку.\n2.Удалить рыбку\n3. Прожить день");
                int userInput;
                if (Int32.TryParse(Console.ReadLine(), out userInput))
                {
                    switch (userInput)
                    {
                        case 1:
                            AddFish();
                            break;
                        case 2:
                            DeleteFish();
                            break;
                        case 3:
                            AddDay();
                            break;
                    }
                }
            }
        }

        public void AddFish()
        {
            Fish fish = new Fish();

            if (_fish.Count < _capacity)
            {
                _fish.Add(fish.Create());
            }
            else
            {
                Console.WriteLine("Превышено количество рыб в аквариуеме");
            }
        }

        public void DeleteFish()
        {
            Console.WriteLine("Выберите номер рыбки:");
            int userInput;
            if (Int32.TryParse(Console.ReadLine(), out userInput))
            {
                _fish.RemoveAt(_fish.Count - 1);
                Console.WriteLine("Удалили рыбу");
            }
            else
            {
                Console.WriteLine("Такой рыбы нет");
            }

        }

        public void AddDay()
        {
            foreach (Fish fish in _fish)
            {
                fish.AddDay();
            }
        }

        public void ShowFishStats()
        {
            foreach (Fish fish in _fish)
            {
                fish.ShowStats();
            }
        }

        public void CheckFishAge()
        {
            foreach (Fish fish in _fish)
            {
                if (fish.CurrentAge >= fish.MaximunAge)
                    fish.Kill();
            }
        }
    }
}
