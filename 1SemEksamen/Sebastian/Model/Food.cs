using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1SemEksamen.Sebastian.Model
{
    class Food : Item
    {
        private string _image;

        public string Image
        {
            get { return _image; }
            set { _image = value; }
        }

        public Food(string image,string name, double price):base(name, price)
        {
            _image = image;
        }

        public Food(string name, double price):base(name, price)
        {
            
        }

    }
}
