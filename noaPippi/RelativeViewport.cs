using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace noaPippi
{
    //TODO: 毎回再帰で描画領域を求めているのをメモ化で高速化
    abstract class RelativeViewport
    {
        private List<Children> children;
        public bool IsHorizontal { get; set; }

        protected RelativeViewport(bool isHorizontal)
        {
            children = new List<Children>();
            IsHorizontal = isHorizontal;
        }
        
        public void AddChildren(double rateOfLength, bool isHorizontal, bool isStatic)
        {
            double rateOfPos = children.Last().GetAbsolutePos() + children.Last().GetAbsoluteLength();
            Children tmp = new Children(this, rateOfPos, rateOfLength, isHorizontal, isStatic);
            children.Add(tmp);
        }
        virtual public void Recalculate()
        {
            foreach (Children c in children)
            {
                c.Recalculate();
            }
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


        class Children : RelativeViewport
        {
            protected RelativeViewport parent;
            public bool IsStatic { get; set; }
            public double RateOfPos { get; }
            public double RateOfLength { get; }

            public Children(RelativeViewport parent, double rateOfPos, double rateOfLength, bool isHorizontal, bool isStatic) : base(isHorizontal)
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

        class Root : RelativeViewport
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
