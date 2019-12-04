using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Controls;

namespace _1SemEksamen.Magnus.Model
{
    public class MusicalNote
    {
        private static StorageFolder _soundFilesFolder = null;

        public static StorageFolder SoundFilesFolder
        {
            get { return _soundFilesFolder; }
            set { _soundFilesFolder = value; }
        }

        private StorageFile _soundFile;
        private MediaElement _soundMediaElement;
        private IRandomAccessStream _stream;

        public MusicalNote(Piano.MusicalNoteNames note)
        {
            _soundMediaElement = new MediaElement();
            _soundMediaElement.AutoPlay = true;
            LoadSoundFileTask(note.ToString());
        }

        //public MusicalNote()
        //{
        //    _soundFile = null;
        //    _soundMediaElement = new MediaElement();
        //    _stream = null;
        //}

        private async void LoadSoundFileTask(string note)
        {
            _soundFile = await _soundFilesFolder.GetFileAsync(note + ".wav");
            //Task test = Task.Run(() =>
            //{
            //    Thread.Sleep(3000);
            //});
            //await test;
            _stream = await _soundFile.OpenAsync(FileAccessMode.Read);
        }

        public void PlayNote()
        {
            _soundMediaElement.SetSource(_stream, _soundFile.ContentType);
        }

    }
}
