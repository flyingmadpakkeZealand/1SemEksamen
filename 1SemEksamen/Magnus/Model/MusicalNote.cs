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

        private static int _hitCounter = 0;

        private async void LoadSoundFileTask(string note)
        {
            _soundFile = await _soundFilesFolder.GetFileAsync(note + ".wav");
            if (_hitCounter==12)
            {
                PianoVMHandler.ProgressBar = 66;
            }
            Task test = Task.Run(() => //Simulated loading time.
            {
                Thread.Sleep(3000);
            });
            await test;
            _stream = await _soundFile.OpenAsync(FileAccessMode.Read);
            _hitCounter++;
            if (_hitCounter==12)
            {
                PianoVMHandler.ProgressBar = 100;
            }
            
        }
        /*Bug: Fixed - QuickFix (Probably not long term solution)
         Because the app uses a loop to initialize multiple MusicalNote objects, a task is started each time it enters LoadSoundFileTask.
         that means the first time we enter here, there is no problem, but once the first MusicalNote object is done, the loading bar is set 100.
         Upon reaching the await the first time we make a MusicalNote object, mainthread will exit to the piano constructor which will then start the next
         MusicalNote constructor because of the loop. As the progress bar was set 100 as soon as the first MusicalNote is done, but the rest are still a few milliseconds behind,
         the mainthread has given back control to user and "unlocked" the PianoPage, thus spamming "J" as the paage is loading, to invoke the last MusicalNote that is created, 
         will crash the program as it isn't quite ready when mainthread gives back control.
         Solution:
         Update progress bar
         Or be more strict and complete disallow mainthread to return control at all - as the behaviour of a running task combined with await can be a bit weird, this might not even be possible.
         Second solution requires answering the question:
         how does C# interpretate that it should wait for a task to complete (super task), when there is another task (sub task) inside it that has currently given back control to the first task (super task)
         because it is awaiting with async await context.
*/

        public void PlayNote()
        {
            _soundMediaElement.SetSource(_stream, _soundFile.ContentType);
        }

    }
}
