using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using _1SemEksamen.Annotations;

namespace _1SemEksamen.Sebastian.Model
{
    abstract class Item: INotifyPropertyChanged
    {
        private int _amount =0;

        public int Amount
        {
            get { return _amount; }
            set { _amount = value; OnPropertyChanged(); }
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

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
