using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using _1SemEksamen.Annotations;
using _1SemEksamen.Common;
using _1SemEksamen.Tristan.Model;

namespace _1SemEksamen.Tristan.ViewModel
{
    class BesøgendeStatestikViewModel:INotifyPropertyChanged
    {
        public List<YakseKasser> YakseTimer { get; set; }

        public List<YakseKasser> YakseDage { get; set; }

        public List<YakseBokseDag> XakseDag { get; set; }
        public List<YakseBokseTimer> XakseTimer { get; set; }

        public BesøgendeStatestik Besøgende { get; set; }

        int HøjesteTalTimer = 0;
        int HøjesteTalDage = 0;

        private RelayCommand _hent;

        public ICommand Hent
        {
            get { return _hent; }
        }

        public int HighestNumberDays
        {
            get
            {
                foreach (Højder tal in Besøgende.UgeData)
                {

                    if (tal.Højde>HøjesteTalDage)
                    {
                        HøjesteTalDage = tal.Højde;
                    }
                }
                YakseDage = new List<YakseKasser>();
                YakseDage.Add(new YakseKasser(Convert.ToInt32(HøjesteTalDage)));
                YakseDage.Add(new YakseKasser(Convert.ToInt32(HøjesteTalDage * 0.9)));
                YakseDage.Add(new YakseKasser(Convert.ToInt32(HøjesteTalDage * 0.8)));
                YakseDage.Add(new YakseKasser(Convert.ToInt32(HøjesteTalDage * 0.7)));
                YakseDage.Add(new YakseKasser(Convert.ToInt32(HøjesteTalDage * 0.6)));
                YakseDage.Add(new YakseKasser(Convert.ToInt32(HøjesteTalDage * 0.5)));
                YakseDage.Add(new YakseKasser(Convert.ToInt32(HøjesteTalDage * 0.4)));
                YakseDage.Add(new YakseKasser(Convert.ToInt32(HøjesteTalDage * 0.3)));
                YakseDage.Add(new YakseKasser(Convert.ToInt32(HøjesteTalDage * 0.2)));
                YakseDage.Add(new YakseKasser(Convert.ToInt32(HøjesteTalDage * 0.1)));
                YakseDage.Add(new YakseKasser(0));
                OnPropertyChanged(nameof(YakseDage));
                return HøjesteTalDage;
            }
            set { HøjesteTalDage = value; }
        }

        public double GangeFaktorDage
        {
            get { return (400.0 / HighestNumberDays); }
            set { GangeFaktorDage = value; }
        }

        public double GangeFaktorTimer
        {
            get { return (400.0 / HighestNumberHours); }
            set { GangeFaktorTimer = value; }
        }

        public int HighestNumberHours
        {
            get
            {
                foreach (Højder tal in Besøgende.MandagData)
                {

                    if (tal.Højde > HøjesteTalTimer)
                    {
                        HøjesteTalTimer = tal.Højde;
                    }
                }
                YakseTimer = new List<YakseKasser>();
                YakseTimer.Add(new YakseKasser(HøjesteTalTimer));
                YakseTimer.Add(new YakseKasser(Convert.ToInt32(HøjesteTalTimer * 0.9)));
                YakseTimer.Add(new YakseKasser(Convert.ToInt32(HøjesteTalTimer * 0.8)));
                YakseTimer.Add(new YakseKasser(Convert.ToInt32(HøjesteTalTimer * 0.7)));
                YakseTimer.Add(new YakseKasser(Convert.ToInt32(HøjesteTalTimer * 0.6)));
                YakseTimer.Add(new YakseKasser(Convert.ToInt32(HøjesteTalTimer * 0.5)));
                YakseTimer.Add(new YakseKasser(Convert.ToInt32(HøjesteTalTimer * 0.4)));
                YakseTimer.Add(new YakseKasser(Convert.ToInt32(HøjesteTalTimer * 0.3)));
                YakseTimer.Add(new YakseKasser(Convert.ToInt32(HøjesteTalTimer * 0.2)));
                YakseTimer.Add(new YakseKasser(Convert.ToInt32(HøjesteTalTimer * 0.1)));
                YakseTimer.Add(new YakseKasser(0));
                OnPropertyChanged(nameof(YakseTimer));
                return HøjesteTalTimer;

            }
            set { HøjesteTalTimer = value; }
        }

        public BesøgendeStatestikViewModel()
        {
            Besøgende = new BesøgendeStatestik();
            _hent=new RelayCommand(HentXakse);
            XakseTimer = new List<YakseBokseTimer>();
            XakseTimer.Add(new YakseBokseTimer(Convert.ToInt32(Besøgende.MandagData[0].Højde*GangeFaktorTimer)));
            XakseTimer.Add(new YakseBokseTimer(Convert.ToInt32(Besøgende.MandagData[1].Højde * GangeFaktorTimer)));
            XakseTimer.Add(new YakseBokseTimer(Convert.ToInt32(Besøgende.MandagData[2].Højde * GangeFaktorTimer)));
            XakseTimer.Add(new YakseBokseTimer(Convert.ToInt32(Besøgende.MandagData[3].Højde * GangeFaktorTimer)));
            XakseTimer.Add(new YakseBokseTimer(Convert.ToInt32(Besøgende.MandagData[4].Højde * GangeFaktorTimer)));
            XakseTimer.Add(new YakseBokseTimer(Convert.ToInt32(Besøgende.MandagData[5].Højde * GangeFaktorTimer)));
  
            XakseDag = new List<YakseBokseDag>();
            XakseDag.Add(new YakseBokseDag(Convert.ToInt32(Besøgende.UgeData[0].Højde * GangeFaktorDage)));
            XakseDag.Add(new YakseBokseDag(Convert.ToInt32(Besøgende.UgeData[1].Højde * GangeFaktorDage)));
            XakseDag.Add(new YakseBokseDag(Convert.ToInt32(Besøgende.UgeData[2].Højde * GangeFaktorDage)));
            XakseDag.Add(new YakseBokseDag(Convert.ToInt32(Besøgende.UgeData[3].Højde * GangeFaktorDage)));
            XakseDag.Add(new YakseBokseDag(Convert.ToInt32(Besøgende.UgeData[4].Højde * GangeFaktorDage)));
            XakseDag.Add(new YakseBokseDag(Convert.ToInt32(Besøgende.UgeData[5].Højde * GangeFaktorDage)));
            XakseDag.Add(new YakseBokseDag(Convert.ToInt32(Besøgende.UgeData[6].Højde * GangeFaktorDage)));


        }

        void HentXakse()
        {
            ObservableCollection < Højder > XAkse = RelayCommand.ObjectParameter as ObservableCollection<Højder>;
            XakseTimer = new List<YakseBokseTimer>();
            HøjesteTalTimer = 0;
            foreach (Højder tal in XAkse)
            {
                if (tal.Højde > HøjesteTalTimer)
                {
                    HøjesteTalTimer = tal.Højde;
                }
            }
            foreach (Højder Antal in XAkse)
            {
                XakseTimer.Add(new YakseBokseTimer(Convert.ToInt32(Antal.Højde*GangeFaktorTimer)));
            }

            OnPropertyChanged(nameof(XakseTimer));
            OnPropertyChanged(nameof(YakseTimer));
        }

        //int HøjesteAntalDag()
        //{
        //    foreach (Højder tal in Besøgende.MandagData)
        //    {

        //        if (tal.Højde > HøjesteTalTimer)
        //        {
        //            HøjesteTalTimer = tal.Højde;
        //        }
        //    }
        //    YakseTimer = new List<YakseKasser>();
        //    YakseTimer.Add(new YakseKasser(HøjesteTalTimer));
        //    YakseTimer.Add(new YakseKasser(Convert.ToInt32(HøjesteTalTimer * 0.9)));
        //    YakseTimer.Add(new YakseKasser(Convert.ToInt32(HøjesteTalTimer * 0.8)));
        //    YakseTimer.Add(new YakseKasser(Convert.ToInt32(HøjesteTalTimer * 0.7)));
        //    YakseTimer.Add(new YakseKasser(Convert.ToInt32(HøjesteTalTimer * 0.6)));
        //    YakseTimer.Add(new YakseKasser(Convert.ToInt32(HøjesteTalTimer * 0.5)));
        //    YakseTimer.Add(new YakseKasser(Convert.ToInt32(HøjesteTalTimer * 0.4)));
        //    YakseTimer.Add(new YakseKasser(Convert.ToInt32(HøjesteTalTimer * 0.3)));
        //    YakseTimer.Add(new YakseKasser(Convert.ToInt32(HøjesteTalTimer * 0.2)));
        //    YakseTimer.Add(new YakseKasser(Convert.ToInt32(HøjesteTalTimer * 0.1)));
        //    YakseTimer.Add(new YakseKasser(0));
        //    OnPropertyChanged(nameof(YakseTimer));
        //    return HøjesteTalTimer;
        //}

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
