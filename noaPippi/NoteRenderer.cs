using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace noaPippi
{
    class NoteRenderer : MeasurableElementRenderer
    {
        private static Texture2D[] texture;
        private static Vector2[] origin;
        static public void LoadContent(Game game)
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

        public Note Note { get; }
        private StaffNotation parent;
        public override MeasurableElement Element => Note;

        public NoteRenderer(Note note, StaffNotation parent)
        {
            this.Note = note;
            this.parent = parent;
        }
        private float getScale(double lineDiff)
        {
            switch (Note.Type)
            {
                case Note.NoteType.Div1:
                    return (float)(parent.Viewport.RateToRelativeY(lineDiff)/(texture[0].Height - 50));
                case Note.NoteType.Div2:
                    return (float)(parent.Viewport.RateToRelativeY(lineDiff)/(texture[1].Height - 50)*3.5f);
                case Note.NoteType.Div4:
                    return (float)(parent.Viewport.RateToRelativeY(lineDiff)/(texture[2].Height - 50)*3.5f);
                case Note.NoteType.Div8:
                    return (float)(parent.Viewport.RateToRelativeY(lineDiff)/(texture[3].Height - 50)*3.5f);
                case Note.NoteType.Div16:
                    return (float)(parent.Viewport.RateToRelativeY(lineDiff)/(texture[4].Height - 50)*3.5f);
                case Note.NoteType.Div32:
                    return (float)(parent.Viewport.RateToRelativeY(lineDiff)/(texture[5].Height - 50)*3.5f);
                default:
                    throw new Exception();
            }
        }
        override public void Draw(double x)
        {
            double lineDiff = parent.LineDiff;
            float y = (float)parent.Viewport.RateToRelativeY(parent.NoteToY(Note.getKey()));
            Line2DRenderer lineRenderer = Line2DRenderer.GetInstance();
            parent.SpriteBatch.Draw(
                texture[(int)Note.Type],
                new Vector2((float)x, y),
                null,
                Color.White,
                0f,
                origin[(int)Note.Type],
                getScale(lineDiff),
                SpriteEffects.None,
                0f);
        }
    }
}
