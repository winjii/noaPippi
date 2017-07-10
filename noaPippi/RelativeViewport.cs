using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace noaPippi
{
    //TODO: 毎回再帰で描画領域を求めているのをメモ化で高速化(再計算は、Recalculateをオーバーライド)
    //TODO: IsStaticを活用して大きさを変えない機能を実装
    abstract class RelativeViewport
    {
        private List<FormedChild> formedChildren;
        private List<FreeChild> freeChildren;
        public bool IsHorizontal { get; set; }

        protected RelativeViewport(bool isHorizontal)
        {
            formedChildren = new List<FormedChild>();
            freeChildren = new List<FreeChild>();
            IsHorizontal = isHorizontal;
        }

        protected RelativeViewport() : this(true) { }
        
        public RelativeViewport AddFormedChild(double rateOfLength, bool isHorizontal, bool isStatic)
        {
            double rateOfPos = (formedChildren.Count > 0) ?
                formedChildren.Last().RateOfPos + formedChildren.Last().RateOfLength : 0;
            FormedChild res = new FormedChild(this, rateOfPos, rateOfLength, isHorizontal, isStatic);
            formedChildren.Add(res);
            return res;
        }
        public RelativeViewport AddFreeChild(double rateOfX, double rateOfY, double rateOfWidth, double rateOfHeight)
        {
            FreeChild res = new FreeChild(this, rateOfX, rateOfY, rateOfWidth, rateOfHeight);
            freeChildren.Add(res);
            return res;
        }
        virtual public void Recalculate()
        {
            foreach (FormedChild c in formedChildren)
            {
                c.Recalculate();
            }
            foreach (FreeChild c in freeChildren)
            {
                c.Recalculate();
            }
        }
        public double RateToAbsoluteX(double rateOfX)
        {
            return GetAbsoluteX() + rateOfX*GetAbsoluteWidth();
        }
        public double RateToAbsoluteY(double rateOfY)
        {
            return GetAbsoluteY() + rateOfY*GetAbsoluteHeight();
        }
        public double RateToRelativeX(double rateOfX)
        {
            return rateOfX*GetAbsoluteWidth();
        }
        public double RateToRelativeY(double rateOfY)
        {
            return rateOfY*GetAbsoluteHeight();
        }
        abstract public double GetAbsoluteX();
        abstract public double GetAbsoluteY();
        abstract public double GetAbsoluteWidth();
        abstract public double GetAbsoluteHeight();
        public Viewport GetIntViewport()
        {
            return new Viewport(
                (int)(GetAbsoluteX() + 0.5),
                (int)(GetAbsoluteY() + 0.5),
                (int)(GetAbsoluteWidth() + 0.5),
                (int)(GetAbsoluteHeight() + 0.5)
            );
        }

        public class FreeChild : RelativeViewport
        {
            protected RelativeViewport parent;
            public double RateOfX { get; }
            public double RateOfY { get; }
            public double RateOfWidth { get; }
            public double RateOfHeight { get; }

            public FreeChild(RelativeViewport parent, double rateOfX, double rateOfY, double rateOfWidth, double rateOfHeight)
            {
                this.parent = parent;
                RateOfX = rateOfX;
                RateOfY = rateOfY;
                RateOfWidth = rateOfWidth;
                RateOfHeight = rateOfHeight;
            }

            public override double GetAbsoluteX()
            {
                return parent.GetAbsoluteX() + RateOfX*parent.GetAbsoluteWidth();
            }
            public override double GetAbsoluteY()
            {
                return parent.GetAbsoluteY() + RateOfY*parent.GetAbsoluteHeight();
            }
            public override double GetAbsoluteWidth()
            {
                return RateOfWidth*parent.GetAbsoluteWidth();
            }
            public override double GetAbsoluteHeight()
            {
                return RateOfHeight*parent.GetAbsoluteHeight();
            }
        }

        public class FormedChild : RelativeViewport
        {
            protected RelativeViewport parent;
            public bool IsStatic { get; set; }
            public double RateOfPos { get; }
            public double RateOfLength { get; }

            public FormedChild(RelativeViewport parent, double rateOfPos, double rateOfLength, bool isHorizontal, bool isStatic) : base(isHorizontal)
            {
                this.parent = parent??throw new ArgumentNullException();
                RateOfPos = rateOfPos;
                RateOfLength = rateOfLength;
                IsStatic = isStatic;
            }
            
            public double GetAbsolutePos()
            {
                if (parent.IsHorizontal) return GetAbsoluteX();
                return GetAbsoluteY();
            }
            public double GetAbsoluteLength()
            {
                if (parent.IsHorizontal) return GetAbsoluteWidth();
                return GetAbsoluteHeight();
            }
            public override double GetAbsoluteX()
            {
                if (!parent.IsHorizontal) return 0;
                return parent.GetAbsoluteX() + parent.GetAbsoluteWidth()*RateOfPos;
            }
            public override double GetAbsoluteY()
            {
                if (parent.IsHorizontal) return 0;
                return parent.GetAbsoluteY() + parent.GetAbsoluteHeight()*RateOfPos;
            }
            public override double GetAbsoluteWidth()
            {
                if (!parent.IsHorizontal) return parent.GetAbsoluteWidth();
                return parent.GetAbsoluteWidth()*RateOfLength;
            }
            public override double GetAbsoluteHeight()
            {
                if (parent.IsHorizontal) return parent.GetAbsoluteHeight();
                return parent.GetAbsoluteHeight()*RateOfLength;
            }
        }

        public class Root : RelativeViewport
        {
            private double absoluteWidth;
            private double absoluteHeight;

            public Root(double absoluteWidth, double absoluteHeight, bool isHorizontal) : base(isHorizontal)
            {
                this.absoluteWidth = absoluteWidth;
                this.absoluteHeight = absoluteHeight;
            }

            public override double GetAbsoluteX()
            {
                return 0;
            }
            public override double GetAbsoluteY()
            {
                return 0;
            }
            public override double GetAbsoluteWidth()
            {
                return absoluteWidth;
            }
            public override double GetAbsoluteHeight()
            {
                return absoluteHeight;
            }
        }
    }
}
