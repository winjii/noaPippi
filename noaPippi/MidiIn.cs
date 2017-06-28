using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace noaPippi
{
    class MidiIn
    {
        [StructLayout(LayoutKind.Sequential)]
        public class MIDI
        {
            IntPtr m_pDeviceHandle;
            IntPtr m_pDeviceName;
            int m_lMode;
            //配列にすると例外で落ちる
            //メモリ上での配置がC++の配列とは異なるためっぽい
            IntPtr m_pSysxHeader0;
            IntPtr m_pSysxHeader1;
            IntPtr m_pSysxHeader2;
            IntPtr m_pSysxHeader3;
            int m_bStarting;
            IntPtr m_pBuf;
            int m_lBufSize;
            int m_lReadPosition;
            int m_lWritePosition;
            int m_bBufLocked;
            byte m_byRunningStatus;
        }
        /* MIDI入力デバイスの数を調べる */
        [DllImport("MIDIIO.dll")]
        static extern int MIDIIn_GetDeviceNum();
        /* MIDI入力デバイスの名前を調べる */
        [DllImport("MIDIIO.dll")]
        static extern int MIDIIn_GetDeviceNameW(int lID, [Out]Char[] pszDeviceName, int lLen);
        /* MIDI入力デバイスを開く */
        [DllImport("MIDIIO.dll")]
        static extern IntPtr MIDIIn_OpenW(Char[] pszDeviceName);
        /* MIDI入力デバイスを閉じる */
        [DllImport("MIDIIO.dll")]
        static extern int MIDIIn_Close(IntPtr pMIDIDevice);
        /* MIDI入力デバイスから任意長のバイナリデータを入力する */
        [DllImport("MIDIIO.dll")]
        static extern int MIDIIn_GetBytes(IntPtr pMIDIIn, [Out]Byte[] pBuf, int lLen);


    }
}
