using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Xml;

namespace noaPippi
{
    class StaffNotation : SeparatedComponent
    {
        double lowerLineY = 0.75;
        public readonly double LineDiff = 0.5/4.0;
        public Clef MyClef { get; }
        private List<MeasurableElementRenderer> renderers;
        public List<MeasurableElement> Measure {
            get => renderers.ConvertAll(v => v.Element);
            set => renderers = value.ConvertAll(v => v.CreateRenderer(this, Game));
        }
        private double[] noteY;

        public StaffNotation(Clef.ClefType clefType, Game game, RelativeViewport viewport) : base(game, viewport)
        {
            renderers = new List<MeasurableElementRenderer>();
            MyClef = new Clef(clefType);
            noteY = new double[128];
            int b = MyClef.NoteNumberOfLowestLine;
            noteY[b] = lowerLineY;
            double[] d = { 0, 1, 0, 1, 1, 0, 1, 0, 1, 0, 1, 1 };
            for (int i = b + 1; i < 128; i++)
            {
                noteY[i] = noteY[i - 1] - d[(i - 1)%12]*LineDiff/2d;
            }
            for (int i = b - 1; i >= 0; i--)
            {
                noteY[i] = noteY[i + 1] + d[i%12]*LineDiff/2d;
            }
        }
        protected override void LoadContent()
        {
            base.LoadContent();
            //TODO: 初期化の責任がこのクラスにあるのはおかしい
            RestRenderer.LoadContent(Game);
            NoteRenderer.LoadContent(Game);
        }
        public double NoteToY(int noteNumber)
        {
            return noteY[noteNumber];
        }
        private void test(SpriteBatch spriteBatch)
        {
            MeasurableElementRenderer rest = new Rest(Rest.RestType.div4).CreateRenderer(this, Game);
            rest.Draw(100);
        }

        protected override void separatelyDraw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Line2DRenderer lineRenderer = Line2DRenderer.GetInstance();
            for (int i = 0; i < 5; i++)
            {
                double y = lowerLineY - i*LineDiff;
                lineRenderer.Draw(
                    spriteBatch,
                    new Vector2(0, (float)Viewport.RateToRelativeY(y)),
                    new Vector2((float)Viewport.GetAbsoluteWidth(), (float)Viewport.RateToRelativeY(y)),
                    3,
                    Color.Black);
            }
            test(spriteBatch);
            for (int i = 1; i < 4; i++)
            {
                lineRenderer.Draw(
                    spriteBatch,
                    new Vector2(
                        (float)Viewport.RateToRelativeX(0.25*i),
                        (float)Viewport.RateToRelativeY(lowerLineY)),
                    new Vector2(
                        (float)Viewport.RateToRelativeX(0.25*i),
                        (float)Viewport.RateToRelativeY(lowerLineY - LineDiff*4)),
                    2,
                    Color.Black);
            }
        }
    }
}
