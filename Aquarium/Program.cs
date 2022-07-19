//Есть аквариум, в котором плавают рыбы. В этом аквариуме может быть максимум определенное кол-во рыб. 
//Рыб можно добавить в аквариум или рыб можно достать из аквариума. (программу делать в цикле для того, чтобы рыбы могли “жить”)
//Все рыбы отображаются списком, у рыб также есть возраст. За 1 итерацию рыбы стареют на определенное кол-во жизней и могут умереть. 
//Рыб также вывести в консоль, чтобы можно было мониторить показатели.

using System;
using System.Collections.Generic;

namespace Aquarium
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }

    class Fish
    {
        public int CurrentAge { get; private set; }
        public int MaximunAge { get; private set; }
        public bool IsAlive { get; private set; }

        public Fish(int age, int maximunAge)
        {
            CurrentAge = age;
            MaximunAge = maximunAge;
            IsAlive = true;
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

        public void AddFish(Fish fish)
        {
            if(_fish.Count < _capacity)
            {
                _fish.Add(fish);
            }
            else
            {
                Console.WriteLine("Превышено количество рыб в аквариуеме");
            }            
        }

        public void DeleteFish()
        {
            _fish.RemoveAt(_fish.Count - 1);
            Console.WriteLine("Удалили рыбу");
        }


    }
}
