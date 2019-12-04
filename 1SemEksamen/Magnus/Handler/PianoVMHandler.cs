using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1SemEksamen.Common;
using _1SemEksamen.Magnus.Model;
using _1SemEksamen.Magnus.ViewModel;

namespace _1SemEksamen.Magnus.Handler
{
    public class PianoVMHandler
    {

        public PianoVM PianoVm { get; set; }

        public PianoVMHandler(PianoVM pianoVM)
        {
            PianoVm = pianoVM;
        }

        public void playPianoNote()
        {
            PianoVm.Piano.PlayPianoNote(Convert.ToInt32(RelayCommand.ObjectParameter.ToString()));
        }
    }
}
