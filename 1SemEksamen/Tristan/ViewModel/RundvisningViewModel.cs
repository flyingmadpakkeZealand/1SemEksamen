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
using DateTime = System.DateTime;

namespace _1SemEksamen.Tristan.ViewModel
{
    class RundvisningViewModel:INotifyPropertyChanged
    {

 
        private ICommand _gemCommand;
        public RundvisningSingleton RundvisningInstance { get; set; }

        public bool GemIsEnabled { get; set; }

        public string CheckDatoFormat
        {
            get { return RundvisningInstance.RundvisningDateTime.ToString();}
            set {
                try
                {
                    RundvisningInstance.RundvisningDateTime = Convert.ToDateTime(value);
                }
                catch (Exception e)
                {
                    MessageDialogHelper.Show("Du har intastet en dato der ikke er gyldig", "Fejl 40");
                } } }

        public RundvisningViewModel()
        {
            _gemCommand = new RelayCommand(Gem);
            RundvisningInstance = RundvisningSingleton.Instance;
            GemIsEnabled = true;
        }

        public async Task<bool> CheckDato(DateTime dato)
        {
            object loadedFiles = await PersistencyFacade.LoadObjectsAsync(ProgramSaveFiles.Rundvisninger, typeof(List<DateTime>));
            List<DateTime> dates = loadedFiles as List<DateTime>;
            if (dates == null)
            {
                return true;
            }
            foreach (DateTime datoer in dates)
            {
                if (dato.DayOfYear == datoer.DayOfYear && dato.Hour == datoer.Hour && dato.Year == datoer.Year)
                {
                    MessageDialogHelper.Show("Du har intastet en dato der allerede er reserveret", "Fejl 40");
                    return false;
                }
            }
            
            return true;
            
        }

        public async void Gem()
        {
            GemIsEnabled = false;
            OnPropertyChanged(nameof(GemIsEnabled));
            if ( await CheckDato(RundvisningInstance.RundvisningDateTime) == true)
            {
                GemRundvisning(RundvisningInstance.RundvisningDateTime);
                OnPropertyChanged(nameof(RundvisningSingleton));
                MessageDialogHelper.Show("Din rundvisning er reserveret", "Yay");
            }

            GemIsEnabled = true;
            OnPropertyChanged(nameof(GemIsEnabled));
        }

        public ICommand GemCommand
        {
            get { return _gemCommand; }
            set { _gemCommand = value; }
        }

        

        async void GemRundvisning(DateTime Rundvisning)
        {
            await PersistencyFacade.SaveObjectsAsync(Rundvisning, ProgramSaveFiles.Rundvisninger, SaveMode.Continuous);
        }




        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
