using Microsoft.VisualStudio.TestTools.UnitTesting;
using noaPippi.Midi;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace noaPippi.Midi.Tests
{
    [TestClass()]
    public class MidiInTests
    {
        //[TestMethod()]
        public void GetDeviceCountTest()
        {
            Assert.Fail();
        }

        //[TestMethod()]
        public void GetDeviceNameTest()
        {
            Assert.Fail();
        }

        //[TestMethod()]
        public void CreateTest()
        {
            Assert.Fail();
        }

        //[TestMethod()]
        public void GetMessageTest()
        {
            int cnt = MidiIn.GetDeviceCount();
            Assert.IsFalse(cnt == 0);
                for (int i = 0; i < cnt; i++)
            {
                Console.WriteLine(MidiIn.GetDeviceName(i));
            }
            MidiIn mi = MidiIn.Create(MidiIn.GetDeviceName(0));
            List<object> message = new List<object>();
            Stopwatch s = new Stopwatch();
            s.Start();
            while (s.ElapsedMilliseconds < 2500) ;
            do {
                List<object> ret = mi.GetMessage();
                if (ret != null) message.AddRange(ret);
            } while (s.ElapsedMilliseconds < 5000);
            Assert.Fail();
        }
    }
}