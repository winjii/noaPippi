using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace noaPippi.Midi
{
    class AfterTouch
    {
        public byte Channel { get; }
        public byte Key { get; }
        public byte Level { get; }
        public AfterTouch(byte key, byte level)
        {
            Key = key;
            Level = level;
        }
    }
}
