using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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
            set
            {
                if (!TryParseToLong(value))
                {
                    MessageDialogHelper.Show("Kreditkort nummer må kun indeholde tal", "Ugyldigt input");
                    ((RelayCommand)_payCommand).RaiseCanExecuteChanged();
                }
                else if (value.Length < 16)
                {
                    MessageDialogHelper.Show("Kreditkort nummer er for kort. Prøv igen.", "For kort Kortnummer");
                    ((RelayCommand)_payCommand).RaiseCanExecuteChanged();
                }
                else
                {
                    _creditCardNumber = value; OnPropertyChanged(); ((RelayCommand)_payCommand).RaiseCanExecuteChanged();
                }
            }
        }
        private string _cvvNumber;

        public string CvvNumber
        {
            get { return _cvvNumber; }
            set {
                if (!TryParseToLong(value))
                {
                    MessageDialogHelper.Show("CVV nummer må kun indeholde tal", "Ugyldigt input");
                    ((RelayCommand)_payCommand).RaiseCanExecuteChanged();
                }
                else if (value.Length < 3)
                {
                    MessageDialogHelper.Show("CVV nummer er for kort. Prøv igen.", "For kort CVVnummer");
                    ((RelayCommand)_payCommand).RaiseCanExecuteChanged();
                }
                else
                {
                    _cvvNumber = value; OnPropertyChanged(); ((RelayCommand)_payCommand).RaiseCanExecuteChanged();
                }
               
                
            }
        }
        private string _buyerName;

        public string BuyerName
        {
            get { return _buyerName; }
            set { _buyerName = value; OnPropertyChanged();}
        }

        private double _totalPrice;

        public double TotalPrice
        {
            get { return _totalPrice; }
            set { _totalPrice = value; OnPropertyChanged(); }
        }

        private string _totalString;

        public string TotalPriceString
        {
            get { return _totalString; }
            set { _totalString = value; OnPropertyChanged(); }
        }



        public ShoppingCart ShoppingCart { get; set; }

        public ShoppingCartVM()
        { 
            ShoppingCart = ShoppingCart.Instance;
            _removeItemCommand = new RelayCommand(RemoveItem, ItemIsSelected);
            _removeAllCommand = new RelayCommand(RemoveAll, CartIsNotEmpty);
            _payCommand = new RelayCommand(Pay, CartIsNotEmptyAndBuyerInfoCorrect);
            UpdateTotalPrice();
            
        }




        public void UpdateTotalPrice()
        {
            TotalPrice = ShoppingCart.TotalPrice;
            TotalPriceString = $"{TotalPrice.ToString()} kr.";
        }







        //func
        public bool ItemIsSelected()
        {

            return _selectedItem != null;
        }

        public bool CartIsNotEmpty()
        {
            return ShoppingCart.Cart.Count > 0;
        }
        public static bool CartIsNotEmptyStatic()
        {
            return ShoppingCart.Instance.Cart.Count > 0;
        }

        public bool CartIsNotEmptyAndBuyerInfoCorrect()
        {

            return CartIsNotEmpty() && TryParseToLong(CvvNumber) && TryParseToLong(CreditCardNumber);
        }


        /* Forsøg på SplitviewPane manipulation
       static bool booltest = false;

       public static bool BoolTest
       {
           get { return booltest;}
           set { booltest = value; }
       }

       public static bool ExceptionThrown()
        {
            
            if (booltest == false)
            {
                booltest = true;
            }
            else 
            {
                booltest = false;
            }

            return booltest;

        }                                       */

        public bool TryParseToLong(string incomingString)
        {
           return long.TryParse(incomingString, out long number);
        }
        

        public void ErrorMessage(FormatException e)
        {
            // forsøg på pane manipulation
            // ExceptionThrown();
            MessageDialogHelper.Show(e.Message, "wrong format");
            
        }
        

        //Actions
        public void RemoveItem()
        {
            ShoppingCart.NewTotalPrice(_selectedItem.Price);
            ShoppingCart.RemoveItem(_selectedIndex);
            UpdateTotalPrice();
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
            //lav kvittering og afslut køb

            UpdateTotalPrice();
            
            _receipt = new Receipt(TotalPrice, BuyerName);

            foreach (Item item in ShoppingCart.Cart)
            {
                item.ItemString = item.ToString();
                _receipt.AddToReceipt(item);
            }

            
            OnPropertyChanged(nameof(Receipt));
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
