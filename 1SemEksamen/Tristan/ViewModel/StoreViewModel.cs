using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using _1SemEksamen.Annotations;
using _1SemEksamen.Common;
using _1SemEksamen.Tristan.Model;

namespace _1SemEksamen.Tristan.ViewModel
{
    public class StoreViewModel:INotifyPropertyChanged
    {
        private StoreIndkøbskurv _indkøbskurv;
        private StoreSingleton _store;
        private Valgmulighed _valgtValgmulighed;
        private ICommand _addCommand;


        public StoreIndkøbskurv IndkøbskurvSingleton
        {
            get { return _indkøbskurv; }
            set { _indkøbskurv = value; }
        }

        public ICommand AddCommand
        {
            get { return _addCommand; }
            set { _addCommand = value; }
        }

        public StoreSingleton Store
        {
            get { return _store; }
            set { _store = value; }
        }

        public Valgmulighed ValgtValgmulighed
        {
            get { return _valgtValgmulighed; }
            set
            {
                _valgtValgmulighed = value;
                OnPropertyChanged();
                ((RelayCommand)_addCommand).RaiseCanExecuteChanged();
            }
        }

        public StoreViewModel()
        {
            _indkøbskurv = StoreIndkøbskurv.Instance;
            _store = StoreSingleton.Instance;
            _addCommand = new RelayCommand(Add, VareErValgt);
        }

        public bool VareErValgt()
        {
            return ValgtValgmulighed != null;
        }

        public void Add()
        {

            Store.Add(ValgtValgmulighed);
            ValgtValgmulighed = null;
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
