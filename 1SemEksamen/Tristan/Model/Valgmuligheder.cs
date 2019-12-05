using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1SemEksamen.Tristan.Model
{
    public class Valgmulighed
    {
        private string _name;
        private int _price;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public int Price
        {
            get { return _price; }
            set { _price = value; }
        }

        public Valgmulighed(string name, int price)
        {
            _name = name;
            _price = price;
        }

        

    }
}
