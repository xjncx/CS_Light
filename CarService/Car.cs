using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService
{
    class Car
    {
        public string BrokenPart { get; private set; }

        public Car(string brokenPart)
        {
            BrokenPart = brokenPart;
        }
    }
}
