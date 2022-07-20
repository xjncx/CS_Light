using System;
using System.Collections.Generic;

namespace OOP_War
{
    class Program
    {
        static void Main()
        {
            List<Soldier> orcsSoldiers = new List<Soldier>();
            List<Soldier> humanSoldiers = new List<Soldier>();
            Squad orcsArmy = new Squad(orcsSoldiers, "Орки");
            Squad humanArmy = new Squad(humanSoldiers, "Люди");
            orcsArmy.Assemble(5);
            humanArmy.Assemble(5);
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
        private Random _random = new Random();
        private int _soldierMinHP = 3;
        private int _soldierMaxHP = 8;
        private int _soldierMinDamage = 1;
        private int _soldierMaxDamage = 4;
        public Soldier(int health = 0, int damage = 0)
        {
            Health = health;
            Damage = damage;
        }

        public Soldier Fill()
        {
            Soldier recruit = new Soldier();
            recruit.Health = _random.Next(_soldierMinHP, _soldierMaxHP);
            recruit.Damage = _random.Next(_soldierMinDamage, _soldierMaxDamage);
            return recruit;
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
        public string Name { get; private set; }
        public Squad(List<Soldier> soldiers, string name)
        {
            _soldiers = soldiers;
            Name = name;
        }

        public void Assemble(int squadCount)
        {
            Soldier recruit = new Soldier();

            for (int i = 0; i < squadCount; i++)
            {
                _soldiers.Add(recruit.Fill());
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
            Soldier soldier;
            soldier = _soldiers[number];
            return soldier;
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
                CheckHealth(ref leftSoldier);

                if (_leftArmy.GetSoldiersNumber() > 0)
                {
                    Console.WriteLine($"Атакуют {_leftArmy.Name}");
                    rightSoldier.TakeDamage(leftSoldier.Damage);
                    CheckHealth(ref rightSoldier);
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

        public void CheckHealth(ref Soldier soldier)
        {
            if (soldier.Health <= 0)
            {
                _leftArmy.SendToHospital((_leftArmy.GetSoldiersNumber() - 1));
                Console.WriteLine($"Солдата убили!");
                if (_leftArmy.GetSoldiersNumber() > 0)
                {
                    soldier = _leftArmy.SendToBattle((_leftArmy.GetSoldiersNumber() - 1));
                }
                else
                {
                    Console.WriteLine("Солдаты кончились взводе!");
                }
            }
        }
    }
}
