using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace noaPippi
{
    class Note : MeasurableElement
    {
        public enum NoteType{
            Div1,
            Div2,
            Div4,
            Div8,
            Div16,
            Div32
        }
        public enum Accidentals
        {
            None = 0,
            Natural = 0,
            Sharp = 1,
            Flat = -1
        }
        public enum PitchName
        {
            C = 0,
            D = 2,
            E = 4,
            F = 5,
            G = 7,
            A = 9,
            B = 11
        }

        public PitchName Pitch { get; }
        public int Octave { get; }
        public Accidentals Accidental { get; }
        public NoteType Type { get; }
        public bool IsDotted { get; }
        public Note(NoteType type, PitchName pitch, int octave, bool isDotted, Accidentals accidental = Accidentals.None)
        {
            Pitch = pitch;
            Accidental = accidental;
            Octave = octave;
            Type = type;
            IsDotted = isDotted;
        }
        public int getKey()
        {
            return (int)Pitch + 12*Octave + (int)Accidental;
        }
        override public double GetDuration() => (IsDotted ? 1.5 : 1.0)/(1 << (int)Type);
        public override MeasurableElementRenderer CreateRenderer(StaffNotation parent, Game game)
            => new NoteRenderer(this, parent);
    }
}
