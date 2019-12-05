using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1SemEksamen.Tristan.Model
{
    public class StoreIndkøbskurv
    {
        public ObservableCollection<Valgmulighed> Indkøbskurv { get; set; }

        private static StoreIndkøbskurv _instance = new StoreIndkøbskurv();

        public static StoreIndkøbskurv Instance
        {
            get { return _instance; }
        }

        private StoreIndkøbskurv()
        {
            Indkøbskurv = new ObservableCollection<Valgmulighed>();
            Indkøbskurv.Add(new Valgmulighed("Kage", 20));
            Indkøbskurv.Add(new Valgmulighed(" Kage", 25));
            Indkøbskurv.Add(new Valgmulighed(" Kage", 30));
        }

        public static void Add(Valgmulighed vare)
        {
            StoreIndkøbskurv.Add(vare);
        }
    }
}
