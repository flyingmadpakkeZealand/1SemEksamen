using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1SemEksamen.Sebastian.Model;

namespace _1SemEksamen.Sebastian.ViewModel
{
    class ShoppingCartVM
    {
        public ShoppingCart ShoppingCart { get; set; }

        public ShoppingCartVM()
        { 
            ShoppingCart = ShoppingCart.Instance;
        }

        
    }
}
