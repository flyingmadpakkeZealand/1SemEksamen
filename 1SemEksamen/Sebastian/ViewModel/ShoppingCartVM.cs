using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;
using _1SemEksamen.Annotations;
using _1SemEksamen.Common;
using _1SemEksamen.Sebastian.Model;

namespace _1SemEksamen.Sebastian.ViewModel
{
    class ShoppingCartVM: INotifyPropertyChanged
    {
        private Item _selectedItem;

        public Item SelectedItem
        {
            get { return _selectedItem; }
            set { _selectedItem = value; OnPropertyChanged(); }
        }
        private int _selectedIndex;

        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set { _selectedIndex = value; }
        }


        private ICommand _removeItemCommand;

        public ICommand RemoveItemCommand
        {
            get { return _removeItemCommand; }
            set { _removeItemCommand = value; }
        }

        private ICommand _removeAllCommand;

        public ICommand RemoveAllCommand
        {
            get { return _removeAllCommand; }
            set { _removeAllCommand = value; }
        }

       


        public ShoppingCart ShoppingCart { get; set; }

        public ShoppingCartVM()
        { 
            ShoppingCart = ShoppingCart.Instance;
            _removeItemCommand = new RelayCommand(RemoveItem, CartIsNotEmpty);
            _removeAllCommand = new RelayCommand(RemoveAll, CartIsNotEmpty);
        }

        //func
        public bool ItemIsSelected()
        {

            return _selectedItem != null;
        }

        public bool CartIsNotEmpty()
        {
            return ShoppingCart != null;
        }

        //Actions
        public void RemoveItem()
        {
            ShoppingCart.RemoveItem(_selectedIndex);
        }



        public void RemoveAll()
        {
            for (int i = 0; i < ShoppingCart.Cart.Count;)
            {
                ShoppingCart.RemoveItem(i);
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
