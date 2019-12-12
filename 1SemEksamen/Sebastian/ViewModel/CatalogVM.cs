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
                ((RelayCommand)AddFoodToCartCommand).RaiseCanExecuteChanged();
                OnPropertyChanged();
            }
        }


        private ICommand _addFoodToCart;

        public ICommand AddFoodToCartCommand
        {
            get { return _addFoodToCart; }
            set { _addFoodToCart = value;}
        }
        private ICommand _addDrinksToCart;

        public ICommand AddDrinksToCartCommand
        {
            get { return _addDrinksToCart; }
            set { _addDrinksToCart = value; }
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
            FoodCatalog.AddFood(new Food("/Assets/StoreLogo.png", "Burger", 2.0));
            FoodCatalog.AddFood(new Food("/Assets/StoreLogo.png", "Hotdog", 2.0));
            FoodCatalog.AddFood(new Food("/Assets/StoreLogo.png", "Sandwich", 2.0));
            FoodCatalog.AddFood(new Food("/Assets/StoreLogo.png", "Pasta", 2.0));

            DrinkCatalog.AddDrink(new Drink("1000ml",  "Vand", 3.0));
            DrinkCatalog.AddDrink(new Drink("333ml", "Cola", 3.0));
            DrinkCatalog.AddDrink(new Drink("500ml", "Øl", 3.0));
            DrinkCatalog.AddDrink(new Drink("322ml","Juice", 3.0));
            
            _addFoodToCart = new RelayCommand(AddFood, CanAlwaysExecute);
            _addDrinksToCart = new RelayCommand(AddDrinks, CanAlwaysExecute);
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

        public bool CanAlwaysExecute()
        {
            return true;
        }



        //Action

        public void AddDrinks()
        {
            foreach (Drink drink in DrinkCatalog.Menu)
            {

                  for (int j =drink.Amount; j > 0; j--)
                  {
                        ShoppingCart.AddItem(drink);
                        ShoppingCart.NewTotalPrice(drink.Price);
                  }
                  drink.Amount = 0;
            }
        }

        
        
        public void AddFood()
        {
            foreach (Food food in FoodCatalog.Menu)
            {
                for (int j = food.Amount; j > 0; j--)
                {
                    ShoppingCart.AddItem(food);
                    ShoppingCart.NewTotalPrice(food.Price);
                }
                food.Amount = 0;
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
