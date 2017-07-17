using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace noaPippi.Midi
{
    class NoteOff
    {
        public byte Channel { get; }
        public byte NoteNumber { get; }
        public byte Level { get; }
        public NoteOff(byte noteNumber, byte level)
        {
            NoteNumber = noteNumber;
            Level = level;
        }
    }
}
