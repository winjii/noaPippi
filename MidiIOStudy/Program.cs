using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MidiIOStudy {
    class Program {
        [StructLayout(LayoutKind.Sequential)]
        public class MIDI
        {
            IntPtr m_pDeviceHandle;
            IntPtr m_pDeviceName; /* 20120124型と長さを修正 */
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
        static extern int MIDIIn_GetBytes(IntPtr pMIDIIn, [In, Out]Byte[] pBuf, int lLen);

        static void Main(string[] args)
        {
            Console.WriteLine(MIDIIn_GetDeviceNum());
            Char[] ret = new Char[100];
            int a = MIDIIn_GetDeviceNameW(0, ret, 100);
            String name = new String(ret);
            name = name.Trim('\0');
            Console.WriteLine(name);
            IntPtr midi = MIDIIn_OpenW(ret);
            Byte[] buf = new Byte[810];
            while (true)
            {
                int size = MIDIIn_GetBytes(midi, buf, 810);
                if (size > 0) break;
            }
            MIDIIn_Close(midi);
        }
    }
}
