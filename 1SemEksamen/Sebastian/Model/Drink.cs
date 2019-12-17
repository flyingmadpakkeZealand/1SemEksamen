using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1SemEksamen.Sebastian.Model
{
    class Drink:Item
    {
        private string _size;

        public string Size
        {
            get { return _size; }
            set { _size = value; }
        }

        public Drink(string size, string name, double price):base(name, price)
        {
            _size = size;
        }

        public override string ToString()
        {
            return $"{Name}         {Size}          {Price.ToString()} kr.";
        }
    }
}
