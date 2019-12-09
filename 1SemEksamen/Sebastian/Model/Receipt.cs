using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1SemEksamen.Sebastian.Model
{
    class Receipt
    {
        private int _id;

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

        private DateTime _timeStamp;

        public DateTime TimeStamp
        {
            get { return _timeStamp; }
            set { _timeStamp = value; }
        }

        public Receipt(int id, double total)
        {
            _id = id;
            _total = total;
            _timeStamp = DateTime.Now;
        }


    }
}
