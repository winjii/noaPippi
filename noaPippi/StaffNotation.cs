﻿using System;
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
        double lineDiff = 0.5/4.0;
        public Clef MyClef { get; }
        List<List<object>> measures;
        private double[] noteY;

        public StaffNotation(Clef.ClefType clefType, Game game, RelativeViewport viewport) : base(game, viewport)
        {
            measures = new List<List<object>>();
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
        public void Load(XmlDocument doc)
        {
            this.measures = new List<List<object>>();
            XmlNodeList measures = doc.SelectNodes("part/measure");
            XmlNode attributes = measures[0]["attributes"];
            int divisions = int.Parse(attributes["divisions"].InnerText);
            //TODO: 調に対応
            //XmlNode noteNumber = attributes.SelectSingleNode("noteNumber");
            //TODO: 拍子に対応
            //XmlNode time = attributes.SelectSingleNode("time");
            foreach (XmlNode measure in measures)
            {
                foreach (XmlNode note in measure)
                {
                    XmlNode pitch = note["pitch"];
                    pitch[]
                }
            }
        }
        public double NoteToY(int noteNumber)
        {
            return noteY[noteNumber];
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
