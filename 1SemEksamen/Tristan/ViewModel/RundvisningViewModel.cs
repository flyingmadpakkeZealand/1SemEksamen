using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Storage;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Documents;
using _1SemEksamen.Annotations;
using _1SemEksamen.Common;
using _1SemEksamen.Tristan.Model;
using Newtonsoft.Json;

namespace _1SemEksamen.Tristan.ViewModel
{
    class RundvisningViewModel:INotifyPropertyChanged
    {
        private static string Rundvisninger = "Rundvisninger.dat";

        private ICommand _gemCommand;
        public RundvisningSingleton RundvisningInstance { get; set; }

        public string DateTimeConverter
        {
            get { return RundvisningInstance.RundvisningDateTime.ToString();}
            set
            {
                try
                {
                    RundvisningInstance.RundvisningDateTime = Convert.ToDateTime(value);
                }
                catch (Exception e)
                {
                    Console.WriteLine("You have entered a wrong date");
                    throw;
                }
               
            }
        }

        public RundvisningViewModel()
        {
            _gemCommand = new RelayCommand(Gem);
            RundvisningInstance = RundvisningSingleton.Instance;
        }


        public void Gem()
        {
            GemRundvisning(RundvisningInstance.RundvisningDateTime);
            
            OnPropertyChanged(nameof(RundvisningSingleton));
        }

        public ICommand GemCommand
        {
            get { return _gemCommand; }
            set { _gemCommand = value; }
        }

     

        async void GemRundvisning(DateTime Rundvisning)
        {
            string BilletJsonString = JsonConvert.SerializeObject(Rundvisning);
            StorageFile localFile = await ApplicationData.Current.LocalFolder.CreateFileAsync(Rundvisninger, CreationCollisionOption.OpenIfExists);
            await FileIO.AppendTextAsync(localFile, BilletJsonString);
        }




        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
