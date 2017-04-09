using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace noaPippi
{
    /// <summary>
    /// Viewportの配分を良い感じにやってくれる
    /// </summary>
    class ViewportsManager
    {
        private Viewport viewport;
        //widthとheightをまとめた言い方が分からなかったのでバラバラに
        private List<double> widths;
        private List<double> heights;
        public ViewportsManager(Viewport viewport)
        {
            this.viewport = viewport;
            widths = new List<double>();
            heights = new List<double>();
        }
        public void AddChildren(/*何かしら*/)
        {
            //TODO
        }
    }
}
