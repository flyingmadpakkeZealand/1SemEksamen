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
using _1SemEksamen.Sebastian.Model;

namespace _1SemEksamen.Sebastian.ViewModel
{
    class CatalogVM : INotifyPropertyChanged
    {

        public ShoppingCart ShoppingCart { get; set; }


        public Catalog FoodCatalog { get; set; }
        public Catalog DrinkCatalog { get; set; }

        private Item _selectedItem;

        public Item SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;

                ((RelayCommand)DecreaseAmountCommand).RaiseCanExecuteChanged();
                ((RelayCommand)IncreaseAmountCommand).RaiseCanExecuteChanged();
                ((RelayCommand)AddItemToCartCommand).RaiseCanExecuteChanged();
                OnPropertyChanged();
            }
        }


        private ICommand _addItemToCart;

        public ICommand AddItemToCartCommand
        {
            get { return _addItemToCart; }
            set { _addItemToCart = value; }
        }


        private ICommand _decreaseAmount;

        public ICommand DecreaseAmountCommand
        {
            get { return _decreaseAmount; }
            set { _decreaseAmount = value; }
        }


        private ICommand _increaseAmount;

        public ICommand IncreaseAmountCommand
        {
            get { return _increaseAmount; }
            set { _increaseAmount = value; }
        }




        public CatalogVM()
        {
            ShoppingCart = ShoppingCart.Instance;
            FoodCatalog = new Catalog();
            DrinkCatalog = new Catalog();
            FoodCatalog.AddFood(new Food("/Assets/StoreLogo.png", "burger", 2.0));
            FoodCatalog.AddFood(new Food("/Assets/StoreLogo.png", "burger", 2.0));
            FoodCatalog.AddFood(new Food("/Assets/StoreLogo.png", "burger", 2.0));
            FoodCatalog.AddFood(new Food("/Assets/StoreLogo.png", "burger", 2.0));

            DrinkCatalog.AddDrink(new Drink("1000ml",  "Vand", 3.0));
            DrinkCatalog.AddDrink(new Drink("333ml", "Cola", 3.0));
            DrinkCatalog.AddDrink(new Drink("500ml", "Øl", 3.0));
            DrinkCatalog.AddDrink(new Drink("322ml","Juice", 3.0));
            
            _addItemToCart = new RelayCommand(Add,SelectedAmountNotZero);
            _increaseAmount = new RelayCommand(IncreaseAmount, ItemIsSelected);
            _decreaseAmount = new RelayCommand(DecreaseAmount,ItemIsSelected);
            }


        



        //func
        public bool ItemIsSelected()
        {

            return _selectedItem != null;
        }

        public bool SelectedAmountNotZero()
        {
            return ItemIsSelected() && _selectedItem.Amount != 0;
        }



        //Action

        public void Add()
        {
            for (int j = SelectedItem.Amount; j > 0; j--)
            {
              ShoppingCart.AddItem(_selectedItem);  
            }
        }

        public void IncreaseAmount()
        {
            SelectedItem.Amount = _selectedItem.Amount+1;
        }
        public void DecreaseAmount()
        {
            SelectedItem.Amount--;
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
