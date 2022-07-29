using System;
using System.Collections.Generic;
using System.Linq;

namespace CarService
{
    class Program
    {
        static void Main(string[] args)
        {
            int serviceBudget = 1000;
            Detail motor = new Detail("Мотор");
            Detail wheel = new Detail("Руль");
            Detail headlight = new Detail("Фара");
            Detail brokenMotor = new Detail("Мотор");
            Detail brokenWheel = new Detail("Руль");
            Detail brokenHeadlight = new Detail("Фара");
            Dictionary<Detail, int> serviceDetails = new Dictionary<Detail, int>();
            Dictionary<Detail, int> servicePrice = new Dictionary<Detail, int>();
            servicePrice.Add(wheel, 200);
            servicePrice.Add(motor, 450);
            servicePrice.Add(headlight, 300);
            serviceDetails.Add(wheel, 3);
            serviceDetails.Add(motor, 2);
            serviceDetails.Add(headlight, 5);
            Warehouse warehouse = new Warehouse(serviceDetails);
            Car Masseratti = new Car(brokenMotor);
            Car Ferrari = new Car(brokenHeadlight);
            Car LADA = new Car(brokenWheel);
            CarService pimpByRide = new CarService(warehouse, serviceBudget, servicePrice);
            pimpByRide.InspectCar(LADA);
        }
    }

    class CarService
    {
        private Warehouse _serviceWarehouse;
        private Dictionary<Detail, int> _priceList;
        public int Budget { get; private set; }

        public CarService(Warehouse warehouse, int budget, Dictionary<Detail, int> priceList)
        {
            _serviceWarehouse = warehouse;
            _priceList = priceList;
            Budget = budget;
        }

        public void InspectCar(Car car)
        {
            Console.WriteLine($"Приехала новая машина. С поломкой: {car.Detail.Name}");
            Detail detailtoRepair = _serviceWarehouse.FindByName(car.Detail.Name);

            if (detailtoRepair.Name != "-" && _serviceWarehouse.IsDetailInStock(detailtoRepair))
            {
                RepairCar(detailtoRepair,car);
                Console.WriteLine($"Xzibit прокачал твою тачку. Теперь статус поломки запчасти {car.Detail.Name} - {car.Detail.IsBroken}");
                TakeMoneyFromCustomer(detailtoRepair);
            }
            else
            {
                Console.WriteLine("Деталей больше нет. Попали на бабки");
                PayFine(detailtoRepair);
            }
        }

        public void RepairCar(Detail detail, Car car)
        {
            _serviceWarehouse.TakeDetail(detail);
            car.RepairDetail();
        }

        public void TakeMoneyFromCustomer(Detail detail)
        {
            Budget += _priceList[detail] + detail.Price;
            Console.WriteLine($"Клиент оплатил ремонт. В кассе {Budget} аден");
        }

        public void PayFine(Detail detail)
        {
            int fine = _priceList[detail];
            if (Budget >= fine)
            {
                Budget -= fine;
            }
            else
            {
                Console.WriteLine("Нет денег на штраф, мы банкроты");
            }
        }
    }

    class Warehouse
    {
        private Dictionary<Detail, int> _detailsCount = new Dictionary<Detail, int>();

        public Warehouse(Dictionary<Detail, int> details)
        {
            _detailsCount = details;
        }

        public int GetAllDetailsCount()
        {
            return _detailsCount.Count();
        }

        public bool IsDetailInStock(Detail detail)
        {
            if (_detailsCount[detail] > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void TakeDetail(Detail detail)
        {
            _detailsCount[detail]--;
        }

        public Detail FindByName(string name)
        {
            Detail emptyDetail = new Detail("-");
            foreach (var detail in _detailsCount)
            {
                if (detail.Key.Name == name)
                {
                    return detail.Key;
                }
            }
            return emptyDetail;
        }
    }

    class Detail
    {
        public string Name { get; private set; }
        public int Price { get; private set; }
        public bool IsBroken { get; private set; }

        public Detail(string name, bool isBroken = false)
        {
            Name = name;
            IsBroken = isBroken;
        }

        public void Repair()
        {
            IsBroken = false;
        }
    }
}
