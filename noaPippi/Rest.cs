using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace noaPippi
{
    class Rest
    {
        public enum RestType
        {
            div1 = 0,
            div4 = 2,
            div8 = 3,
            div16 = 4
        }
        private static Texture2D[] texture;
        private static Vector2[] origin;
        
        public RestType Type { get; }
        private StaffNotation parent;
        private Game game;
        public Rest(RestType type, StaffNotation parent, Game game)
        {
            Type = type;
            this.parent = parent;
            this.game = game;
        }
        public void LoadContent()
        {
            if (texture == null)
            {
                texture = new Texture2D[6];
                texture[0] = game.Content.Load<Texture2D>("rest1");
                texture[2] = game.Content.Load<Texture2D>("rest4");
                texture[3] = game.Content.Load<Texture2D>("rest8");
                texture[4] = game.Content.Load<Texture2D>("rest16");
            }
            if (origin == null)
            {
                origin = new Vector2[6];
                origin[0] = new Vector2(texture[0].Width/2f, 0);
                for (int i = 2; i < 5; i++)
                {
                    origin[i] = new Vector2(texture[i].Width/2f, texture[i].Height/2f);
                }
            }
        }
        private float getScale(double lineDiff)
        {
            switch (Type)
            {
                case RestType.div1:
                    return (float)(parent.Viewport.RateToRelativeY(lineDiff)/texture[0].Height*0.25);
                case RestType.div4:
                    return (float)(parent.Viewport.RateToRelativeY(lineDiff)/texture[2].Height*2.5f);
                case RestType.div8:
                    return (float)(parent.Viewport.RateToRelativeY(lineDiff)/texture[3].Height*2f);
                case RestType.div16:
                    return (float)(parent.Viewport.RateToRelativeY(lineDiff)/texture[4].Height*2f);
                default:
                    throw new Exception();
            }
        }
        public void Draw(double x, double lineDiff)
        {
            int k0 = parent.MyClef.NoteNumberOfLowestLine;
            double y = parent.NoteToY(k0) - lineDiff*((Type == RestType.div1) ? 3 : 2);
            parent.SpriteBatch.Draw(
                texture[(int)Type],
                new Vector2((float)x, (float)parent.Viewport.RateToRelativeY(y)),
                null,
                Color.White,
                0f,
                origin[(int)Type],
                getScale(lineDiff),
                SpriteEffects.None,
                0f);
        }
        public double GetDuration() => 1d/(1 << (int)Type);
    }
}
