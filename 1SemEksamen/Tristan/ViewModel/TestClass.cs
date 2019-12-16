using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1SemEksamen.Tristan.Model;

namespace _1SemEksamen.Tristan.ViewModel
{
    public class TestClass
    {
        public ObservableCollection<Valgmulighed> CollectionValgmulighed { get; set; }
        public int TotalPrice { get; set; }

        public TestClass(int totalPrice)
        {
            CollectionValgmulighed = new ObservableCollection<Valgmulighed>();
            TotalPrice = totalPrice;
        }

        public void Add(Valgmulighed valgmulighed)
        {
            CollectionValgmulighed.Add(valgmulighed);
        }
    }
}
