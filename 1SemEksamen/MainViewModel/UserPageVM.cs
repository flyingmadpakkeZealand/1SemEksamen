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
