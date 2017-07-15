using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace noaPippi.Midi
{
    //TODO?: 3バイトでないメッセージもあるらしい
    class MidiMessageParser
    {
        public object Parse(byte a, byte b, byte c)
        {
            uint type = a;

            //ノートオフ
            if ((type >> 4) == 0x8 || ((type >> 4) == 0x09 && (uint)c == 0))
            {
                return new NoteOff(b, c);
            }
            //ノートオン
            else if ((type >> 4) == 0x9)
            {
                return new NoteOn(b, c);
            }
            //キーアフタータッチ
            else if ((type >> 4) == 0xA)
            {
                return new AfterTouch(b, c);
            }
            else return null;
        }
        public List<object> Parse(List<byte> rawMessage)
        {
            List<object> res = new List<object>(rawMessage.Count/3);
            for (int i = 0; i + 3 <= rawMessage.Count; i++)
            {
                object ret = Parse(rawMessage[i], rawMessage[i + 1], rawMessage[i + 2]);
                if (ret == null) continue;
                res.Add(ret);
            }
            return res;
        }
    }
}
