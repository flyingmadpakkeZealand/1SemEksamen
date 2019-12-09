using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1SemEksamen.Tristan.Model
{
    public class StoreSingleton
    {
        public ObservableCollection<Valgmulighed> store { get; set; }

        private static StoreSingleton _instance = new StoreSingleton();

        public static StoreSingleton Instance
        {
            get { return _instance; }
        }

        private StoreSingleton()
        {
            store = new ObservableCollection<Valgmulighed>();
            store.Add(new Valgmulighed("Kage",20));
            store.Add(new Valgmulighed("Gulerods Kage", 25));
            store.Add(new Valgmulighed("drømme Kage", 30));
        }

        public void Add(Valgmulighed vare)
        {
            StoreIndkøbskurv.Add(vare);
        }

    }
}
