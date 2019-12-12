using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        private List<Item> _itemList = new List<Item>();

        public List<Item> BoughtItems
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

        }
        
        public void AddToReceipt(Item incomingItem)
        {
            _itemList.Add(incomingItem);
        }

    }
}
