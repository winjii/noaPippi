using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace noaPippi
{
    class Clef
    {
        public enum ClefType
        {
            G, //ト音
            F  //ヘ音
        }

        public int NoteNumberOfLowestLine { get; }
        public ClefType Type { get; }
        public Clef(ClefType type)
        {
            Type = type;
            switch (type)
            {
                case ClefType.G:
                    NoteNumberOfLowestLine = 64;
                    break;
                case ClefType.F:
                    NoteNumberOfLowestLine = 43;
                    break;
            }
        }
    }
}
