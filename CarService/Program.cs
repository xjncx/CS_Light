using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//У вас есть автосервис, в который приезжают люди, чтобы починить свои автомобили.
//У вашего автосервиса есть баланс денег и склад деталей.
//Когда приезжает автомобиль, у него сразу ясна его поломка, и эта поломка отображается у вас в консоли вместе с ценой за починку
//(цена за починку складывается из цены детали + цена за работу).
//Поломка всегда чинится заменой детали, но количество деталей ограничено тем, что находится на вашем складе деталей.
//Если у вас нет нужной детали на складе, то вы можете отказать клиенту, и в этом случае вам придется выплатить штраф.
//Если вы замените не ту деталь, то вам придется возместить ущерб клиенту.
//За каждую удачную починку вы получаете выплату за ремонт, которая указана в чек-листе починки.
//Класс Деталь не может содержать значение “количество”. Деталь всего одна, за количество отвечает тот, кто хранит детали. 
//При необходимости можно создать дополнительный класс для конкретной детали и работе с количеством.

namespace CarService
{
    class Program
    {
        static void Main(string[] args)
        {
            Detail motor = new Detail("Мотор");
            Detail wheel = new Detail("Руль");
            Detail headLight = new Detail("Фара");
            Dictionary<Detail, int> details = new Dictionary<Detail, int>();
            details.Add(wheel, 3);
            details.Add(motor, 2);
            details.Add(headLight, 5);
            Warehouse warehouse = new Warehouse(details);
            Car Masseratti = new Car("Руль");
            Car Ferrari = new Car("Фара");
        }
    }

    class CarService
    {
        private Car _car;
        private Warehouse _serviceWarehouse;
        public int Budget { get; private set; }

        public CarService(Warehouse warehouse, int budget)
        {
            _serviceWarehouse = warehouse;
            Budget = budget;
        }

        public void RepairCar(Car car)
        {
            Console.WriteLine($"Приехала новая машина. С поломкой: {car.BrokenPart}");
            Console.
        }
    }

    class Warehouse
    {
        // private List<Detail> _details = new List<Detail>();
        private Dictionary<Detail, int> _details = new Dictionary<Detail, int>();

        public Warehouse(Dictionary<Detail,int> details)
        {
            _details = details;
        }
    }

    class Detail
    {
        public string Name { get; private set; }

        public Detail (string name)
        {
            Name = name;
        }
    }
}
