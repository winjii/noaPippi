using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace noaPippi
{
    //TODO: 固定された表示領域と動的な表示領域のクラスを派生させたい
    abstract class IVirtualViewport
    {
        protected IVirtualViewport parent;
        abstract public double GetRateOfX();
        abstract public double GetRateOfY();
        abstract public double GetRateOfWidth();
        abstract public double GetRateOfHeight();
        public bool IsRoot() { return this == parent; }
        public Viewport GetAbsoluteViewport()
        {
            Viewport parentViewport = parent.GetAbsoluteViewport();
            return new Viewport(
                (int)(parentViewport.X + parentViewport.Width*GetRateOfX() + 0.5),
                (int)(parentViewport.Y + parentViewport.Height*GetRateOfY() + 0.5),
                (int)(parentViewport.Width*GetRateOfWidth() + 0.5),
                (int)(parentViewport.Height*GetRateOfHeight() + 0.5)
            );
        }
    }
}
