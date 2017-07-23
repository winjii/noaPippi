using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace noaPippi
{
    abstract class MeasurableElement
    {
        abstract public MeasurableElementRenderer CreateRenderer(StaffNotation parent, Game game);
        abstract public double GetDuration();
    }
}
