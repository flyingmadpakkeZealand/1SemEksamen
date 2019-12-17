using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Appointments.DataProvider;

namespace _1SemEksamen.Tristan.Model
{
    class BesøgendeStatestik
    {
        public ObservableCollection<YakseKasser> Yakse { get; set; }

        public BesøgendeStatestik()
        {
            
        }   
        


    }

    class YakseKasser
    {
        private int _height;

        private int _number;

        public int Height
        {
            get { return _height; }
            set { _height = value; }
        }

        public int Number
        {
            get { return _number; }
            set { _number = value; }
        }

        public YakseKasser(int Height, int Number)
        {
            _height = Height;
            _number = Number;
        }

        public YakseKasser()
        {
            
        }
    }
}
