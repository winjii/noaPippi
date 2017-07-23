using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace noaPippi
{
    abstract class MeasurableElementRenderer
    {
        abstract public MeasurableElement Element { get; }
        abstract public void Draw(double x);
    }
}
