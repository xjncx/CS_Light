using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            arena.StartBattle();
        }
    }

    class Soldier
    {
        public int Health { get; private set; }
        public int Damage { get; private set; }

        public Soldier(int health, int damage)
        {
            Health = health;
            Damage = damage;
        }

        public Soldier Fill()
        {
            Soldier recruit = new Soldier(0, 0);
            Random random = new Random();
            int soldierMinHP = 3;
            int soldierMaxHP = 8;
            int soldierMinDamage = 1;
            int soldierMaxDamage = 4;
            recruit.Health = random.Next(soldierMinHP, soldierMaxHP);
            recruit.Damage = random.Next(soldierMinDamage, soldierMaxDamage);
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
        private Soldier _recruit = new Soldier(0, 0);
        public string Name { get; private set; }
        public Squad(List<Soldier> soldiers, string name)
        {
            _soldiers = soldiers;
            Name = name;
        }

        public void Assemble(int squadCount)
        {
            for (int i = 0; i < squadCount; i++)
            {
                _soldiers.Add(_recruit.Fill());
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

        public void StartBattle()
        {
            Soldier leftSoldier = _leftArmy.SendToBattle((_leftArmy.GetSoldiersNumber() - 1));
            Soldier rightSoldier = _rightArmy.SendToBattle((_rightArmy.GetSoldiersNumber() - 1));

            while (_leftArmy.GetSoldiersNumber() > 0 && _rightArmy.GetSoldiersNumber() > 0)
            {
                leftSoldier.TakeDamage(rightSoldier.Damage);

                if (leftSoldier.Health <= 0)
                {
                    _leftArmy.SendToHospital((_leftArmy.GetSoldiersNumber() - 1));
                    Console.WriteLine("Солдата левого взвода убили!");
                    if (_leftArmy.GetSoldiersNumber() > 0)
                    {
                        leftSoldier = _leftArmy.SendToBattle((_leftArmy.GetSoldiersNumber() - 1));
                    }
                    else
                    {
                        Console.WriteLine("Солдаты кончились в левом взводе!");
                    }
                }
                if (_leftArmy.GetSoldiersNumber() > 0)
                {
                    rightSoldier.TakeDamage(leftSoldier.Damage);

                    if (rightSoldier.Health <= 0)
                    {
                        _rightArmy.SendToHospital((_rightArmy.GetSoldiersNumber() - 1));
                        Console.WriteLine("Солдата правого взвода убили!");
                        if (_rightArmy.GetSoldiersNumber() > 0)
                        {
                            rightSoldier = _rightArmy.SendToBattle((_rightArmy.GetSoldiersNumber() - 1));
                        }
                        else
                        {
                            Console.WriteLine("Солдаты кончились в левом взводе!");
                        }
                    }
                }               
            }
            AnnounceWinner(_leftArmy, _rightArmy);
        }

        public void AnnounceWinner(Squad left, Squad right)
        {
            if (left.GetSoldiersNumber() == 0)
            {
                Console.WriteLine("Выиграли правые!");
            }
            else
            {
                Console.WriteLine("Выиграли левые!");
            }
        }
    }
}