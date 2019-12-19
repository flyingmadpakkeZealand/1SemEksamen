using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Storage;
using _1SemEksamen.Annotations;
using _1SemEksamen.Common;
using _1SemEksamen.Tristan.Model;
using Newtonsoft.Json;

namespace _1SemEksamen.Tristan.ViewModel
{
    public class StoreViewModel:INotifyPropertyChanged
    {
        private StoreIndkøbskurv _indkøbskurv;
        private StoreSingleton _store;
        private Valgmulighed _valgtValgmulighed;
        private ICommand _addCommand;
        private ICommand _jaCommand;
        private ICommand _nejCommand;

        private static string jsonFileName = "Kvitteringer.dat";
        public StoreIndkøbskurv IndkøbskurvSingleton
        {
            get { return _indkøbskurv; }
            set { _indkøbskurv = value; }
        }

        public ICommand JaCommand
        {
            get { return _jaCommand; }
            set { _jaCommand = value; }
        }

        public ICommand NejCommand
        {
            get { return _nejCommand; }
            set { _nejCommand = value; }
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
            _jaCommand = new RelayCommand(Ja);
            _nejCommand = new RelayCommand(Nej);
        }

        public bool VareErValgt()
        {
            return ValgtValgmulighed != null;
        }

        public void Add()
        {

            IndkøbskurvSingleton.Add(ValgtValgmulighed);
            IndkøbskurvSingleton.Totalprice = IndkøbskurvSingleton.Totalprice + ValgtValgmulighed.Price;
            ValgtValgmulighed = null;
        }

        public async void Ja()
        {
            await SaveStore(IndkøbskurvSingleton);
            IndkøbskurvSingleton.Indkøbskurv = new ObservableCollection<Valgmulighed>();
            IndkøbskurvSingleton.Totalprice = 0;
            OnPropertyChanged(nameof(IndkøbskurvSingleton));
        }

        public async Task SaveStore(StoreIndkøbskurv kviteringer)
        {
            await PersistencyFacade.SaveObjectsAsync(kviteringer, ProgramSaveFiles.Kvitteringer,SaveMode.Continuous);
        }


        public void Nej()
        {
            IndkøbskurvSingleton.Indkøbskurv = new ObservableCollection<Valgmulighed>();
            IndkøbskurvSingleton.Totalprice = 0;
            OnPropertyChanged(nameof(IndkøbskurvSingleton));

        }

        //async void SaveStore(StoreIndkøbskurv kvittering)
        //{
        //    //await PersistencyFacade.SaveObjectsAsync(kvittering, ProgramSaveFiles.kvitteringer, SaveMode.Continuous);

        //}
   

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
