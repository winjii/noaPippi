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
    class Rest : MeasurableElement
    {
        public enum RestType
        {
            div1 = 0,
            div2 = 1,
            div4 = 2,
            div8 = 3,
            div16 = 4
        }
        
        public RestType Type { get; }
        public Rest(RestType type)
        {
            Type = type;
        }
        override public double GetDuration() => 1d/(1 << (int)Type);
        public override MeasurableElementRenderer CreateRenderer(StaffNotation parent, Game game)
            => new RestRenderer(this, parent);
    }
}
