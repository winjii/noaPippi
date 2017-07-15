using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace noaPippi.Midi
{
    public class MidiIn
    {
        [StructLayout(LayoutKind.Sequential)]
        class MIDI
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
        private static extern int MIDIIn_GetDeviceNum();
        /* MIDI入力デバイスの名前を調べる */
        [DllImport("MIDIIO.dll")]
        private static extern int MIDIIn_GetDeviceNameW(int lID, [Out]Char[] pszDeviceName, int lLen);
        /* MIDI入力デバイスを開く */
        [DllImport("MIDIIO.dll")]
        private static extern IntPtr MIDIIn_OpenW(Char[] pszDeviceName);
        /* MIDI入力デバイスを閉じる */
        [DllImport("MIDIIO.dll")]
        private static extern int MIDIIn_Close(IntPtr pMIDIDevice);
        /* MIDI入力デバイスから任意長のバイナリデータを入力する */
        [DllImport("MIDIIO.dll")]
        private static extern int MIDIIn_GetBytes(IntPtr pMIDIIn, [Out]Byte[] pBuf, int lLen);

        public static int GetDeviceCount() => MIDIIn_GetDeviceNum();
        public static string GetDeviceName(int id)
        {
            Char[] name = new Char[256];
            MIDIIn_GetDeviceNameW(id, name, 256);
            string res = new string(name);
            return res.TrimEnd('\0');
        }
        public static MidiIn Create(string deviceName)
        {
            IntPtr ret = MIDIIn_OpenW(deviceName.ToCharArray());
            if (ret == null) return null;
            return new MidiIn(ret);
        }

        private IntPtr midi;
        private Byte[] buf;
        private const int BUF_SIZE = 1024;
        private MidiIn(IntPtr midi)
        {
            this.midi = midi;
            buf = new Byte[BUF_SIZE];
        }
        public List<object> GetMessage()
        {
            int size = MIDIIn_GetBytes(midi, buf, BUF_SIZE);
            if (size == 0) return null;
            List<byte> rawMessage = new List<byte>(size);
            for (int i = 0; i < size; i++) rawMessage.Add(buf[i]);
            MidiMessageParser parser = new MidiMessageParser();
            return parser.Parse(rawMessage);
        }
        ~MidiIn()
        {
            MIDIIn_Close(midi);
        }
    }
}
