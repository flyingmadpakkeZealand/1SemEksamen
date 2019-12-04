﻿using System;
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
        private static PianoVM _staticPianoVm = null;

        public static int ProgressBar
        {
            set { _staticPianoVm.ProgressBarStatus = value; }
        }

        public PianoVM PianoVm { get; set; }

        public PianoVMHandler(PianoVM pianoVM)
        {
            _staticPianoVm = pianoVM;
            PianoVm = pianoVM;
        }

        public void playPianoNote()
        {
            PianoVm.Piano.PlayPianoNote(Convert.ToInt32(RelayCommand.ObjectParameter.ToString()));
        }
    }
}
