using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Appointments.DataProvider;
using _1SemEksamen.Annotations;

namespace _1SemEksamen.Tristan.Model
{
    class BesoegendeStatestik
    {
        public ObservableCollection<YakseKasser> Yakse { get; set; }

        public ObservableCollection<Højder> MandagData { get; set; }
        public ObservableCollection<Højder> TirsdagData { get; set; }
        public ObservableCollection<Højder> OnsdagData { get; set; }
        public ObservableCollection<Højder> TorsdagData { get; set; }
        public ObservableCollection<Højder> FredagData { get; set; }
        public ObservableCollection<Højder> LørdagData { get; set; }

        public ObservableCollection<Højder> SøndagData { get; set; }


        public ObservableCollection<Højder> UgeData { get; set; }
        public BesoegendeStatestik()
        {
            MandagData = new ObservableCollection<Højder>();
            MandagData.Add(new Højder(10));
            MandagData.Add(new Højder(20));
            MandagData.Add(new Højder(30));
            MandagData.Add(new Højder(40));
            MandagData.Add(new Højder(40));
            MandagData.Add(new Højder(20));

            TirsdagData = new ObservableCollection<Højder>();
            TirsdagData.Add(new Højder(10));
            TirsdagData.Add(new Højder(10));
            TirsdagData.Add(new Højder(50));
            TirsdagData.Add(new Højder(30));
            TirsdagData.Add(new Højder(20));
            TirsdagData.Add(new Højder(10));

            OnsdagData = new ObservableCollection<Højder>();
            OnsdagData.Add(new Højder(30));
            OnsdagData.Add(new Højder(60));
            OnsdagData.Add(new Højder(30));
            OnsdagData.Add(new Højder(20));
            OnsdagData.Add(new Højder(40));
            OnsdagData.Add(new Højder(10));

            TorsdagData = new ObservableCollection<Højder>();
            TorsdagData.Add(new Højder(40));
            TorsdagData.Add(new Højder(20));
            TorsdagData.Add(new Højder(50));
            TorsdagData.Add(new Højder(10));
            TorsdagData.Add(new Højder(20));
            TorsdagData.Add(new Højder(40));

            FredagData = new ObservableCollection<Højder>();
            FredagData.Add(new Højder(30));
            FredagData.Add(new Højder(30));
            FredagData.Add(new Højder(50));
            FredagData.Add(new Højder(30));
            FredagData.Add(new Højder(70));
            FredagData.Add(new Højder(10));

            LørdagData = new ObservableCollection<Højder>();
            LørdagData.Add(new Højder(20));
            LørdagData.Add(new Højder(40));
            LørdagData.Add(new Højder(60));
            LørdagData.Add(new Højder(50));
            LørdagData.Add(new Højder(30));
            LørdagData.Add(new Højder(10));

            SøndagData = new ObservableCollection<Højder>();
            SøndagData.Add(new Højder(40));
            SøndagData.Add(new Højder(50));
            SøndagData.Add(new Højder(30));
            SøndagData.Add(new Højder(20));
            SøndagData.Add(new Højder(10));
            SøndagData.Add(new Højder(10));

            UgeData = new ObservableCollection<Højder>();
            UgeData.Add(new Højder(50));
            UgeData.Add(new Højder(100));
            UgeData.Add(new Højder(200));
            UgeData.Add(new Højder(50));
            UgeData.Add(new Højder(100));
            UgeData.Add(new Højder(70));
            UgeData.Add(new Højder(50));
        }
    }

    class YakseKasser
    {

        private int _number;

        public int Number
        {
            get { return _number; }
            set { _number = value; }
        }

        public YakseKasser(int Number)
        {
            _number = Number;

        }

    }
    class YakseBokseDag
    {
        private int _height;


        public int Height
        {
            get { return _height; }
            set { _height = value; }
        }



        public YakseBokseDag(int Height)
        {
            _height = Height;

        }

    }
    class YakseBokseTimer
    {
        private int _height;


        public int Height
        {
            get { return _height; }
            set { _height = value; }
        }


        public YakseBokseTimer(int Height)
        {
            _height = Height;

        }

    }

    class YakseTimer
    {
        private int _number;


        public int Number
        {
            get { return _number; }
            set { _number = value; }
        }


        public YakseTimer(int Number)
        {
            _number = Number;

        }

    }

    class YakseDage
    {
        private int _number;


        public int Number
        {
            get { return _number; }
            set { _number = value; }
        }


        public YakseDage(int Number)
        {
            _number = Number;

        }

    }

    class Højder
    {

        private int _højde;

        public int Højde
        {
            get { return _højde; }
            set { _højde = value; }
        }

        public Højder(int Number)
        {
            _højde = Number;

        }
    }

}
