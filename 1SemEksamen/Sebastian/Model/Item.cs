using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1SemEksamen.Sebastian.Model
{
    abstract class Item
    {
        private int _amount =1;

        public int Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        private int _id = 0;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }


        private double _price;

        public double Price
        {
            get { return _price; }
            set { _price = value; }
        }

        public Item()
        {
            
        }
        protected Item(string name, double price)
        {
            _id++;
            _name = name;
            _price = price;
        }

    }
}
