using System;
using System.Collections.Generic;

namespace Aquarium
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Fish> fishes = new List<Fish>() { new Fish(2, 4), new Fish(1, 3), new Fish(4, 6) };
            Aquarium aquarium = new Aquarium(fishes, 4);
            aquarium.ShowMenu();
        }
    }

    class Fish
    {
        public int CurrentAge { get; private set; }
        public int MaximunAge { get; private set; }
        public bool IsAlive => CurrentAge < MaximunAge;

        public Fish(int age = 1, int maximunAge = 1)
        {
            CurrentAge = age;
            MaximunAge = maximunAge;
        }

        public void ShowStats()
        {
            Console.WriteLine($"Статус живости рыбки: {IsAlive}. Возраст рыбки: {CurrentAge}");
        }

        public void AddDay()
        {
            CurrentAge++;
        }
    }

    class Aquarium
    {
        private List<Fish> _fishes;
        private int _capacity;
        private Random _random = new Random();
        private int _maxFishAge = 10;
        private int _minFishAge = 1;

        public Aquarium(List<Fish> fish, int capacity)
        {
            _fishes = fish;
            _capacity = capacity;
        }

        public void ShowMenu()
        {
            bool isRunning = true;

            while (isRunning)
            {
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
            if (_fishes.Count < _capacity)
            {
                _fishes.Add(new Fish(_random.Next(_minFishAge, _maxFishAge), _maxFishAge));
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
                _fishes.RemoveAt(_fishes.Count - 1);
                Console.WriteLine("Удалили рыбу");
            }
            else
            {
                Console.WriteLine("Такой рыбы нет");
            }
        }

        public void AddDay()
        {
            foreach (Fish fish in _fishes)
            {
                fish.AddDay();
            }
        }

        public void ShowFishStats()
        {
            foreach (Fish fish in _fishes)
            {
                fish.ShowStats();
            }
        }
    }
}
