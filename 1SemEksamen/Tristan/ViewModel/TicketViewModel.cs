using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Storage;
using _1SemEksamen.Annotations;
using _1SemEksamen.Common;
using _1SemEksamen.Tristan.Model;
using Newtonsoft.Json;
using Tickets = _1SemEksamen.Tristan.View.Tickets;

namespace _1SemEksamen.Tristan.ViewModel
{
    public class TicketViewModel:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ICommand _addCommand;
        private ICommand _nejCommand;
        private ICommand _jaCommand;
        private static string BilletListe = "Billetter.dat";

        public Ticket TicketObjekt
        {
            get { return AktuelleTickets; }
            set
            {
                AktuelleTickets = value;
            }
        }


        private Ticket AktuelleTickets;
        public TicketViewModel()
        {
            AktuelleTickets = new Ticket(0,0,0,0,0);
            _addCommand = new RelayCommand(Add);
            _nejCommand = new RelayCommand(Nej);
            _jaCommand = new RelayCommand(Ja);
        }

        public ICommand AddCommand
        {
            get { return _addCommand; }
            set { _addCommand = value; }
        }

        public ICommand NejCommand
        {
            get { return _nejCommand; }
            set { _nejCommand = value; }
        }

        public ICommand JaCommand
        {
            get { return _jaCommand; }
            set { _jaCommand = value; }
        }


        public void Add()
        {
            switch (Convert.ToInt32(RelayCommand.ObjectParameter.ToString()))
            {
                case 0:
                {
                    TicketObjekt.Voksen = TicketObjekt.Voksen + 1;
                    TicketObjekt.TotalPrice = TicketObjekt.TotalPrice + 100;
                    break;
                }
                
                case 1:
                {
                    TicketObjekt.Barn = TicketObjekt.Barn + 1;
                    TicketObjekt.TotalPrice = TicketObjekt.TotalPrice + 50;
                        break;
                }
                case 2:
                {
                    TicketObjekt.Pensionist = TicketObjekt.Pensionist + 1;
                    TicketObjekt.TotalPrice = TicketObjekt.TotalPrice + 75;
                        break;
                }
                case 3:
                {
                    TicketObjekt.KortEllerStudent = TicketObjekt.KortEllerStudent + 1;
                    break;
                }
            }
            OnPropertyChanged(nameof(TicketObjekt));
        }

        public void Nej()
        {
            TicketObjekt = new Ticket(0,0,0,0,0);
            OnPropertyChanged(nameof(TicketObjekt));
        }

        public void Ja()
        {
            SaveBilletter(TicketObjekt);

            TicketObjekt = new Ticket(0, 0, 0, 0, 0);
            OnPropertyChanged(nameof(TicketObjekt));
            
        }

        async void SaveBilletter(Ticket billet)
        {
            string BilletJsonString = JsonConvert.SerializeObject(TicketObjekt);
            StorageFile localFile = await ApplicationData.Current.LocalFolder.CreateFileAsync(BilletListe, CreationCollisionOption.OpenIfExists);
            await FileIO.AppendTextAsync(localFile, BilletJsonString);
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
