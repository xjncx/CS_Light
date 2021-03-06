using System;
using System.Collections.Generic;

namespace OOP_War
{
    class Program
    {
        static void Main()
        {
            Squad orcsArmy = new Squad("Орки", 5);
            Squad humanArmy = new Squad("Люди", 5);
            orcsArmy.ShowStats();
            humanArmy.ShowStats();
            BattleField arena = new BattleField(orcsArmy, humanArmy);
            arena.Battle();
        }
    }

    class Soldier
    {
        public int Health { get; private set; }
        public int Damage { get; private set; }

        public Soldier(int health = 0, int damage = 0)
        {
            Health = health;
            Damage = damage;
            Fill();
        }

        public void Fill()
        {
            Random _random = new Random();
            int _soldierMinHealth = 3;
            int _soldierMaxHealth = 8;
            int _soldierMinDamage = 1;
            int _soldierMaxDamage = 4;
            Health = _random.Next(_soldierMinHealth, _soldierMaxHealth);
            Damage = _random.Next(_soldierMinDamage, _soldierMaxDamage);
        }

        public void ShowInfo()
        {
            Console.WriteLine($"Здоровье - {Health}, Урон - {Damage}");
        }

        public void TakeDamage(int damage)
        {
            Health -= damage;
        }
    }

    class Squad
    {
        private List<Soldier> _soldiers;
        private int _size;
        public string Name { get; private set; }

        public Squad(string name, int size)
        {
            _soldiers = new List<Soldier>();
            _size = size;
            Name = name;
            Recruit();
        }

        public void Recruit()
        {
            for (int i = 0; i < _size; i++)
            {
                _soldiers.Add(new Soldier());
            }
        }

        public void ShowStats()
        {
            Console.WriteLine(Name);

            foreach (Soldier soldier in _soldiers)
            {
                soldier.ShowInfo();
            }
        }

        public int GetSoldiersNumber()
        {
            return _soldiers.Count;
        }

        public Soldier SendToBattle(int number)
        {
            return _soldiers[number];
        }

        public void SendToHospital(int soldierNumber)
        {
            _soldiers.RemoveAt(soldierNumber);
        }
    }

    class BattleField
    {
        private Squad _leftArmy;
        private Squad _rightArmy;

        public BattleField(Squad leftArmy, Squad rightArmy)
        {
            _leftArmy = leftArmy;
            _rightArmy = rightArmy;
        }

        public void Battle()
        {
            Soldier leftSoldier = _leftArmy.SendToBattle((_leftArmy.GetSoldiersNumber() - 1));
            Soldier rightSoldier = _rightArmy.SendToBattle((_rightArmy.GetSoldiersNumber() - 1));

            while (_leftArmy.GetSoldiersNumber() > 0 && _rightArmy.GetSoldiersNumber() > 0)
            {
                Console.WriteLine($"Атакуют {_rightArmy.Name}");
                leftSoldier.TakeDamage(rightSoldier.Damage);
                ShowSoloAttackResult(ref leftSoldier, _leftArmy);

                if (_leftArmy.GetSoldiersNumber() > 0)
                {
                    Console.WriteLine($"Атакуют {_leftArmy.Name}");
                    rightSoldier.TakeDamage(leftSoldier.Damage);
                    ShowSoloAttackResult(ref rightSoldier, _rightArmy);
                }
            }
            AnnounceWinner(_leftArmy, _rightArmy);
        }

        public void AnnounceWinner(Squad left, Squad right)
        {
            if (left.GetSoldiersNumber() == 0)
            {
                Console.WriteLine($"Выиграли {right.Name}!");
            }
            else
            {
                Console.WriteLine($"Выиграли {left.Name}!");
            }
        }

        public void ShowSoloAttackResult(ref Soldier soldier, Squad squad)
        {
            if (soldier.Health <= 0)
            {
                squad.SendToHospital((squad.GetSoldiersNumber() - 1));
                Console.WriteLine($"Солдата {squad.Name} убили!");
                if (squad.GetSoldiersNumber() > 0)
                {
                    soldier = squad.SendToBattle((squad.GetSoldiersNumber() - 1));
                }
                else
                {
                    Console.WriteLine($"Солдаты кончились в взводе {squad.Name}!");
                }
            }
        }
    }
}
