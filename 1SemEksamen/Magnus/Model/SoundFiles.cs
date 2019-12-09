using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Controls;

namespace _1SemEksamen.Magnus.Model
{
    public class SoundFiles
    {
        protected static StorageFolder _soundFilesFolder = null;

        public static StorageFolder SoundFilesFolder
        {
            get { return _soundFilesFolder; }
            set { _soundFilesFolder = value; }
        }

        protected StorageFile _soundFile;
        protected MediaElement _soundMediaElement;
        protected IRandomAccessStream _stream;
    }
}
