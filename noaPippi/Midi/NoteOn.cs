﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace noaPippi.Midi
{
    class NoteOn
    {
        public byte Key { get; }
        public byte Level { get; }
        public NoteOn(byte key, byte level)
        {
            Key = key;
            Level = level;
        }
    }
}
