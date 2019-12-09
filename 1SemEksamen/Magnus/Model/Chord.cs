using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1SemEksamen.Magnus.Model
{
    public class Chord
    {
        private List<MusicalNote> _chordNotes;

        public Chord(List<MusicalNote> chordNotes)
        {
            _chordNotes = chordNotes;
        }

        public void PlayChord()
        {
            foreach (MusicalNote musicalNote in _chordNotes)
            {
                musicalNote.PlayNote();
            }
        }
    }
}
