using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace noaPippi
{
    class StaffNotation : SeparatedComponent
    {
        double lowerLineY = 0.75;
        double lineDiff = 0.5/4.0;
        public Clef MyClef { get; }
        private double[] noteY;

        public StaffNotation(Clef.ClefType clefType, Game game, RelativeViewport viewport) : base(game, viewport)
        {
            MyClef = new Clef(clefType);
            noteY = new double[128];
            int b = MyClef.NoteNumberOfLowestLine;
            noteY[b] = lowerLineY;
            double[] d = { 0, 1, 0, 1, 1, 0, 1, 0, 1, 0, 1, 1 };
            for (int i = b + 1; i < 128; i++)
            {
                noteY[i] = noteY[i - 1] - d[(i - 1)%12]*lineDiff/2d;
            }
            for (int i = b - 1; i >= 0; i--)
            {
                noteY[i] = noteY[i + 1] + d[i%12]*lineDiff/2d;
            }
        }

        public double NoteToY(int key)
        {
            return noteY[key];
        }
        private void test(SpriteBatch spriteBatch)
        {
            Rest rest = new Rest(Rest.RestType.div4, this, Game);
            rest.LoadContent();
            rest.Draw(100, lineDiff);
        }

        protected override void separatelyDraw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Line2DRenderer lineRenderer = Line2DRenderer.GetInstance();
            for (int i = 0; i < 5; i++)
            {
                double y = lowerLineY - i*lineDiff;
                lineRenderer.Draw(
                    spriteBatch,
                    new Vector2(0, (float)Viewport.RateToRelativeY(y)),
                    new Vector2((float)Viewport.GetAbsoluteWidth(), (float)Viewport.RateToRelativeY(y)),
                    3,
                    Color.Black);
            }
            test(spriteBatch);
        }
    }
}
