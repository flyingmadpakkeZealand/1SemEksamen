using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1SemEksamen.Sebastian.Model
{
    class Catalog
    {
        
        //Katalog Property
        private ObservableCollection<Item> _menu;

        public ObservableCollection<Item> Menu
        {
            get { return _menu; }
            set { _menu = value; }
        }


        public Catalog()
        {
            Menu = new ObservableCollection<Item>();
        }

        public void AddFood(Food incomingFood)
        {
            Menu.Add(incomingFood);
        }

        public void AddDrink(Drink incomingDrink)
        {
            Menu.Add(incomingDrink);
        }

        

    }
}
