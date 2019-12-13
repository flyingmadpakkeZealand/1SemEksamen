using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1SemEksamen.Sebastian.Model
{
    class ShoppingCart
    {
        private ObservableCollection<Item> _cart;
        public ObservableCollection<Item> Cart
        {
            get { return _cart; }
            set { _cart = value; }
        }

        private double _totalPrice;

        public double TotalPrice
        {
            get { return _totalPrice; }
            set { _totalPrice = value; }
        }


        private static ShoppingCart _instance;

        public static ShoppingCart Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ShoppingCart();
                }

                return _instance;
            }

        }

        public ShoppingCart()
        {
            Cart = new ObservableCollection<Item>();
        }

        public void AddItem(Item item)
        {
            _cart.Add(item);
        }

        public void RemoveItem(int index)
        {
            _cart.RemoveAt(index);
        }

        public void RemoveAll()
        {
            foreach (Item item in _cart)
            {
                _cart.Remove(item);
            }
        }
        public void NewTotalPrice(double incomingPrice)
        {
            _totalPrice = _totalPrice + incomingPrice;
        }
    }
}
