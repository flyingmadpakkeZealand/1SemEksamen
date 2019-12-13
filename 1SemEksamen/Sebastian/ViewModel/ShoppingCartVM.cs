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

        private Receipt _receipt;

        public Receipt Receipt
        {
            get { return _receipt; }
            set { _receipt = value; }
        }

        private ICommand _payCommand;

        public ICommand PayCommand
        {
            get { return _payCommand; }
            set { _payCommand = value; }
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
        private string _creditCardNumber;

        public string CreditCardNumber
        {
            get { return _creditCardNumber; }
            set { _creditCardNumber = value; }
        }
        private string _cvvNumber;

        public string CvvNumber
        {
            get { return _cvvNumber; }
            set { _cvvNumber = value; }
        }
        private string _buyerName;

        public string BuyerName
        {
            get { return _buyerName; }
            set { _buyerName = value; }
        }

        private bool _isInt;

        public bool Isint
        {
            get { return _isInt; }
            set { _isInt = value; }
        }

        








        public ShoppingCart ShoppingCart { get; set; }

        public ShoppingCartVM()
        { 
            ShoppingCart = ShoppingCart.Instance;
            _removeItemCommand = new RelayCommand(RemoveItem, CartIsNotEmpty);
            _removeAllCommand = new RelayCommand(RemoveAll, CartIsNotEmpty);
            _payCommand = new RelayCommand(Pay, CartIsNotEmpty);
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

        public bool AlwaysTrue()
        {
            return true;
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

        public void Pay()
        {
            /*  forsøg på at parse til int fra string mangler exception handling
            try
            {
                int numVal = Int32.Parse(CreditCardNumber);
            }
            catch (FormatException e)
            {
                Console.WriteLine(e.Message);
                return;
            }
            try
            {
                int numVal = Int32.Parse(CvvNumber);
            }
            catch (FormatException e)
            {
                Console.WriteLine(e.Message);
                return;
            }
            */

            //lav kvittering og afslut køb
            _receipt = new Receipt(ShoppingCart.TotalPrice, BuyerName);
            foreach (Item item in ShoppingCart.Cart)
            { 
                _receipt.AddToReceipt(item);
            }
            RemoveAll();
            
        }






      


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
