﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace noaPippi
{
    class RestRenderer : MeasurableElementRenderer
    {
        private static Texture2D[] texture;
        private static Vector2[] origin;
        private StaffNotation parent;
        static public void LoadContent(Game game)
        {
            if (texture == null)
            {
                texture = new Texture2D[6];
                texture[0] = game.Content.Load<Texture2D>("rest1");
                texture[1] = game.Content.Load<Texture2D>("rest2");
                texture[2] = game.Content.Load<Texture2D>("rest4");
                texture[3] = game.Content.Load<Texture2D>("rest8");
                texture[4] = game.Content.Load<Texture2D>("rest16");
            }
            if (origin == null)
            {
                origin = new Vector2[6];
                origin[0] = new Vector2(texture[0].Width/2f, 0);
                origin[1] = new Vector2(texture[1].Width/2f, texture[1].Height);
                for (int i = 2; i < 5; i++)
                {
                    origin[i] = new Vector2(texture[i].Width/2f, texture[i].Height/2f);
                }
            }
        }

        public Rest Rest { get; }
        public override MeasurableElement Element => Rest;

        public RestRenderer(Rest rest, StaffNotation parent)
        {
            this.Rest = rest;
            this.parent = parent;
        }
        private float getScale(double lineDiff)
        {
            switch (Rest.Type)
            {
                case Rest.RestType.div1:
                    return (float)(parent.Viewport.RateToRelativeY(lineDiff)/texture[0].Height*0.25);
                case Rest.RestType.div2:
                    return (float)(parent.Viewport.RateToRelativeY(lineDiff)/texture[1].Height*0.25);
                case Rest.RestType.div4:
                    return (float)(parent.Viewport.RateToRelativeY(lineDiff)/texture[2].Height*2.5f);
                case Rest.RestType.div8:
                    return (float)(parent.Viewport.RateToRelativeY(lineDiff)/texture[3].Height*2f);
                case Rest.RestType.div16:
                    return (float)(parent.Viewport.RateToRelativeY(lineDiff)/texture[4].Height*2f);
                default:
                    throw new Exception();
            }
        }
        override public void Draw(double x)
        {
            double lineDiff = parent.LineDiff;
            int k0 = parent.MyClef.NoteNumberOfLowestLine;
            double y = parent.NoteToY(k0) - lineDiff*((Rest.Type == Rest.RestType.div1) ? 3 : 2);
            parent.SpriteBatch.Draw(
                texture[(int)Rest.Type],
                new Vector2((float)x, (float)parent.Viewport.RateToRelativeY(y)),
                null,
                Color.White,
                0f,
                origin[(int)Rest.Type],
                getScale(lineDiff),
                SpriteEffects.None,
                0f);
        }
    }
}
