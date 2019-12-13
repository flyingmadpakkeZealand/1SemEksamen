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
            store.Add(new Valgmulighed("T-shirt",20));
            store.Add(new Valgmulighed("Shorts", 20));
            store.Add(new Valgmulighed("Sweater", 25));
            store.Add(new Valgmulighed("Banjo", 30));
            store.Add(new Valgmulighed("Fløjte", 30));
            store.Add(new Valgmulighed("Guitar", 25));
            store.Add(new Valgmulighed("Vand", 30));
            store.Add(new Valgmulighed("Slikkepind", 30));
            store.Add(new Valgmulighed("Mintpastiller", 30));
        }

        public void Add(Valgmulighed vare)
        {
            store.Add(vare);
        }

    }
}
