using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService
{
    class Car
    {
        public Detail Detail { get; private set; }

        public Car(Detail detail)
        {
            Detail = detail;
        }

        public void RepairDetail()
        {
            Detail.Repair();
        }
    }
}
