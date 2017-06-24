using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace noaPippi {
    class MidiDeviceManager {
        static class NativeShit {
            public delegate void MidiInProc(
                IntPtr midi_in_handle,
                uint wMsg,
                uint dwInstance,
                uint dwParam1,
                uint dwParam2
            );

            [DllImport("winmm.dll")]
            public static extern uint MidiInOpen(
                IntPtr lphMidiIn,
                uint uDeviceID,
                IntPtr dwCallback,
                IntPtr dwCallbackInstance,
                uint dwFlags
            );
        }
    }
}
