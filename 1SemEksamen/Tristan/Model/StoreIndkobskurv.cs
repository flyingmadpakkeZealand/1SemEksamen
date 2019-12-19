using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using _1SemEksamen.Annotations;

namespace _1SemEksamen.Tristan.Model
{
    public class StoreIndkobskurv:INotifyPropertyChanged
    {
        public ObservableCollection<Valgmulighed> Indkøbskurv { get; set; }

        private int totalPrice;

        public int Totalprice
        {
            get { return totalPrice; }
            set { totalPrice = value; OnPropertyChanged(); }
        }


        private static StoreIndkobskurv _instance = new StoreIndkobskurv();

        public static StoreIndkobskurv Instance
        {
            get { return _instance; }
        }

        private StoreIndkobskurv()
        {
            Indkøbskurv = new ObservableCollection<Valgmulighed>();
        }

        public void Add(Valgmulighed vare)
        {
            Indkøbskurv.Add(vare);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
