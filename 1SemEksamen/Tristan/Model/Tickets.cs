using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace _1SemEksamen.Tristan.Model
{
    public class Ticket
    {
        public int Barn { get; set; }
        public int Voksen { get; set; }
        public int Pensionist { get; set; }
        public int KortEllerStudent { get; set; }
        public int TotalPrice { get; set; }

        

        public Ticket(int barn, int voksen, int pensionist, int kortEllerStudent, int totalPrice)
        {
            Barn = barn;
            Voksen = voksen;
            Pensionist = pensionist;
            KortEllerStudent = kortEllerStudent;
            TotalPrice = totalPrice;
        }



    }
}
