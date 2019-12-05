using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using _1SemEksamen.Common;
using _1SemEksamen.Tristan.Model;


namespace _1SemEksamen.Tristan.ViewModel
{
    public class CreateStoreViewModel
    {
        private ICommand _addCommand;
        
        public StoreSingleton VarerSingleton { get; set; }

        public StoreIndkøbskurv VarerIndkøbskurv { get; set; }

        public CreateStoreViewModel()
        {
            VarerSingleton = StoreSingleton.Instance;
            _addCommand = new RelayCommand(Add);
        }

        public ICommand AddCommand
        {
            get { return _addCommand; }
            set { _addCommand = value; }
        }

        public string Name { get; set; }

        public int Price { get; set; }

        public void Add()
        {

            VarerSingleton.Add(new Valgmulighed(Name,Price));
        }
    }
}
