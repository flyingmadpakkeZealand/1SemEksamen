using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using _1SemEksamen.Annotations;

namespace _1SemEksamen.Sebastian.Model
{
    class Receipt
    {
        private int _id = 0;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private double _total;

        public double Total
        {
            get { return _total; }
            set { _total = value; }
        }

        private string _buyerName;

        public string BuyerName
        {
            get { return _buyerName; }
            set { _buyerName = value; }
        }


        private DateTime _timeStamp;

        public DateTime TimeStamp
        {
            get { return _timeStamp; }
            set { _timeStamp = value; }
        }

        private ObservableCollection<Item> _itemList;

        public ObservableCollection<Item> BoughtItems
        {
            get { return _itemList; }
            set { _itemList = value; }
        }


        public Receipt(double total, string buyer)
        {
            _id = _id++;
            _total = total;
            _timeStamp = DateTime.Now;
            _buyerName = buyer;
            BoughtItems = new ObservableCollection<Item>();
        }


        public void AddToReceipt(Item incomingItem)
        {
            _itemList.Add(incomingItem);
        }

    }
}
