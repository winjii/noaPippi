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
    class Note
    {
        public enum NoteType{
            div1,
            div2,
            div4,
            div8,
            div16,
            div32
        }
        private static Texture2D[] texture;
        private static Vector2[] origin;

        public int Key { get; }
        public NoteType Type { get; }
        public bool IsDotted { get; }
        private StaffNotation parent;
        private Game game;
        public Note(int key, NoteType type, bool isDotted, StaffNotation parent, Game game)
        {
            Key = key;
            Type = type;
            IsDotted = isDotted;
            this.parent = parent;
            this.game = game;
        }
        public void LoadContent()
        {
            if (texture == null)
            {
                texture = new Texture2D[6];
                texture[0] = game.Content.Load<Texture2D>("note1");
                texture[1] = game.Content.Load<Texture2D>("note2");
                texture[2] = game.Content.Load<Texture2D>("note4");
                texture[3] = game.Content.Load<Texture2D>("note8");
                texture[4] = game.Content.Load<Texture2D>("note16");
                texture[5] = game.Content.Load<Texture2D>("note32");
            }
            if (origin == null)
            {
                origin = new Vector2[6];
                origin[0] = new Vector2(texture[0].Width/2f, texture[0].Height/2f);
                origin[1] = new Vector2(texture[1].Width/2f, (texture[1].Height - 50)*3f/3.5f);
                origin[2] = new Vector2(texture[2].Width/2f, (texture[2].Height - 50)*3f/3.5f);
                origin[3] = new Vector2(494.5f, (texture[3].Height - 50)*3f/3.5f);
                origin[4] = new Vector2(494.5f, (texture[4].Height - 50)*3f/3.5f);
                origin[5] = new Vector2(494.5f, (texture[5].Height - 50)*3f/3.5f);
            }
        }
        private float getScale(double lineDiff)
        {
            switch (Type)
            {
                case NoteType.div1:
                    return (float)(parent.Viewport.RateToRelativeY(lineDiff)/texture[0].Height - 50);
                case NoteType.div2:
                    return (float)(parent.Viewport.RateToRelativeY(lineDiff)/(texture[1].Height - 50)*3.5f);
                case NoteType.div4:
                    return (float)(parent.Viewport.RateToRelativeY(lineDiff)/(texture[2].Height - 50)*3.5f);
                case NoteType.div8:
                    return (float)(parent.Viewport.RateToRelativeY(lineDiff)/(texture[3].Height - 50)*3.5f);
                case NoteType.div16:
                    return (float)(parent.Viewport.RateToRelativeY(lineDiff)/(texture[4].Height - 50)*3.5f);
                case NoteType.div32:
                    return (float)(parent.Viewport.RateToRelativeY(lineDiff)/(texture[5].Height - 50)*3.5f);
                default:
                    throw new Exception();
            }
        }
        public void Draw(double x, double lineDiff)
        {
            Line2DRenderer lineRenderer = Line2DRenderer.GetInstance();
            parent.SpriteBatch.Draw(
                texture[(int)Type],
                new Vector2((float)x, (float)parent.NoteToY(Key)),
                null,
                Color.White,
                0f,
                origin[(int)Type],
                getScale(lineDiff),
                SpriteEffects.None,
                0f);
        }
        public double GetDuration() => (IsDotted ? 1.5 : 1.0)/(1 << (int)Type);
    }
}
