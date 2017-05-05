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
    class RelativeViewport
    {
        protected RelativeViewport parent;
        private double rateOfX;
        private double rateOfY;
        private double rateOfWidth;
        private double rateOfHeight;

        protected RelativeViewport(RelativeViewport parent)
        {
            if (parent == null) this.parent = this;
            else this.parent = parent;
        }
        public double GetX() { return parent.GetWidth()*rateOfX; }
        public double GetY() { return parent.GetHeight()*rateOfY; }
        public double GetWidth() { return parent.GetWidth()*rateOfWidth; }
        public double GetHeight() { return parent.GetHeight()*rateOfHeight; }
        public double GetRateOfX() { return rateOfX; }
        public double GetRateOfY() { return rateOfY; }
        public double GetRateOfWidth() { return rateOfWidth; }
        public double GetRateOfHeight() { return rateOfHeight; }
        public bool IsRoot() { return parent == this; }
        public double GetAbsoluteX()
        {
            if (IsRoot()) return GetX();
            return parent.GetX() + GetX();
        }
        public double GetAbsoluteY()
        {
            if (IsRoot()) return GetY();
            return parent.GetY() + GetY();
        }
        public Viewport GetIntViewport()
        {
            return new Viewport(
                (int)(GetAbsoluteX() + 0.5),
                (int)(GetAbsoluteY() + 0.5),
                (int)(GetWidth() + 0.5),
                (int)(GetHeight() + 0.5)
            );
        }
    }
}
