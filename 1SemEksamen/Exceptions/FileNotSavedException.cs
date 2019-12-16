using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1SemEksamen.Common;

namespace _1SemEksamen.Exceptions
{
    public class FileNotSavedException : Exception
    {
        private FileNotFoundException _actualException;

        public FileNotFoundException ActualException
        {
            get { return _actualException;}
        }

        public FileNotSavedException(string message, FileNotFoundException actualException):base(message)
        {
            _actualException = actualException;
        }

        public void ShowDefaultMessageOnScreen()
        {
            MessageDialogHelper.Show(Message + _actualException.FileName, "File not Found Exception");
        }
    }
}
