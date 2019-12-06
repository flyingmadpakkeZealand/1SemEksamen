using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Controls;
using _1SemEksamen.Magnus.Handler;

namespace _1SemEksamen.Magnus.Model
{
    public class Melody : SoundFiles
    {
        //private static StorageFolder _soundFilesFolder = null;

        //public static StorageFolder SoundFilesFolder
        //{
        //    get { return _soundFilesFolder; }
        //    set { _soundFilesFolder = value; }
        //}

        //private StorageFile _soundFile;
        //private MediaElement _melodyMediaElement;
        //private IRandomAccessStream _stream;

        public string MelodyName { get; set; }

        public MediaElement OneMelody
        {
            get { return _soundMediaElement; }
        }

        public Melody(string melody)
        {
            _soundMediaElement = new MediaElement();
            _soundMediaElement.AutoPlay = false;
            MelodyName = melody;
            LoadSoundFileTask(melody);
        }

        private async void LoadSoundFileTask(string melody)
        {
            _soundFile = await _soundFilesFolder.GetFileAsync(melody + ".wav");

            _stream = await _soundFile.OpenAsync(FileAccessMode.Read);
            _soundMediaElement.SetSource(_stream, _soundFile.ContentType);
            PianoVMHandler.ProgressBar = PianoVMHandler.ProgressBar + 6;
        }

        //public void PlayMelody()
        //{
        //    _soundMediaElement.SetSource(_stream,_soundFile.ContentType);
        //}
    }
}
