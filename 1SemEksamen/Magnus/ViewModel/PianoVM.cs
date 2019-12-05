using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;
using _1SemEksamen.Annotations;
using _1SemEksamen.Common;
using _1SemEksamen.Magnus.Handler;
using _1SemEksamen.Magnus.Model;

namespace _1SemEksamen.Magnus.ViewModel
{
    public class PianoVM : INotifyPropertyChanged
    {
        public PianoVMHandler PianoVmHandler { get; set; }

        private Piano _piano;

        public Piano Piano
        {
            get { return _piano; }
        }


        public PianoVM()
        {
            PianoVmHandler = new PianoVMHandler(this);
            _piano = new Piano(); //Careful, as of now the constructor of piano has asynchronous behaviour. It will give control back potentially before _piano is in a valid state. Use loading bar.
            _pressPlayPianoNoteCommand = new RelayCommand(PianoVmHandler.PlayPianoNote); //PianoVM has to do this before it returns control, if it could return control before initializing the command, the command would actually have to be updated with OnPropertyChanged. 
            _pressPlayPianoChordCommand = new RelayCommand(PianoVmHandler.PlayPianoChord);
            _pressPlayPianoMelodyCommand = new RelayCommand(PianoVmHandler.PlayPianoMelody);
        }

        private RelayCommand _pressPlayPianoMelodyCommand;

        public ICommand PressPlayPianoMelodyCommand
        {
            get { return _pressPlayPianoMelodyCommand; }
        }


        private RelayCommand _pressPlayPianoChordCommand;

        public ICommand PressPlayPianoChordCommand
        {
            get { return _pressPlayPianoChordCommand; }
        }


        private RelayCommand _pressPlayPianoNoteCommand;

        public ICommand PressPlayPianoNoteCommand
        {
            get { return _pressPlayPianoNoteCommand; }
        }

        private Visibility _loadingVisibility;

        public Visibility LoadingVisibility
        {
            get { return _loadingVisibility; }
            set
            {
                _loadingVisibility = value;
                OnPropertyChanged();
            }
        }

        private bool _enableButtons;

        public bool EnableButtons
        {
            get { return _enableButtons; }
            set
            {
                _enableButtons = value;
                OnPropertyChanged();
            }
        }


        private int _progressBarStatus;

        public int ProgressBarStatus
        {
            get { return _progressBarStatus; }
            set
            {
                _progressBarStatus = value;
                OnPropertyChanged();
                if (value == 100)
                {
                    LoadingVisibility = Visibility.Collapsed;
                    EnableButtons = true;
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
