using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using _1SemEksamen.Annotations;
using _1SemEksamen.Common;
using _1SemEksamen.Tristan.Model;

namespace _1SemEksamen.MainViewModel
{
    public class UserPageVM : INotifyPropertyChanged
    {
        private bool _menuVisibility;

        public bool MenuVisibility
        {
            get { return _menuVisibility; }
            set
            {
                _menuVisibility = value;
                OnPropertyChanged();
            }
        }

        public UserPageVM()
        {
            _menuVisibility = false;
            _pressToggleMenuCommand = new RelayCommand(ToggleMenu);
            Ticket testObject1 = new Ticket(1,2,3,4,123456789);
            Ticket testObject2 = new Ticket(5,55,555,5555,909090909);
            List<Ticket> list1 = new List<Ticket>(){testObject1,testObject2};
            //PersistencyFacade.SaveObjectsAsync(list1, ProgramSaveFiles.Example, SaveMode.Continuous);
            Test();
        }

        private async void Test()
        {
            object test = await PersistencyFacade.LoadObjectsAsync(ProgramSaveFiles.Example,typeof(List<List<Ticket>>));

            List<List<Ticket>> listTest = (List<List<Ticket>>) test;
        }

        private RelayCommand _pressToggleMenuCommand;

        public ICommand PressToggleMenuCommand
        {
            get { return _pressToggleMenuCommand; }
        }


        public void ToggleMenu()
        {
            MenuVisibility = !MenuVisibility;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
