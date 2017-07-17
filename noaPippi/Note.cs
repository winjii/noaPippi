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
            Div1,
            Div2,
            Div4,
            Div8,
            Div16,
            Div32
        }
        public enum Accidentals
        {
            None = 0,
            Natural = 0,
            Sharp = 1,
            Flat = -1
        }
        public enum PitchName
        {
            C = 0,
            D = 2,
            E = 4,
            F = 5,
            G = 7,
            A = 9,
            B = 11
        }
        private static Texture2D[] texture;
        private static Vector2[] origin;

        public PitchName Pitch { get; }
        public int Octave { get; }
        public Accidentals Accidental { get; }
        public NoteType Type { get; }
        public bool IsDotted { get; }
        private StaffNotation parent;
        private Game game;
        public Note(NoteType type, PitchName pitch, int octave, bool isDotted, StaffNotation parent, Game game, Accidentals accidental = Accidentals.None)
        {
            Pitch = pitch;
            Accidental = accidental;
            Octave = octave;
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
                case NoteType.Div1:
                    return (float)(parent.Viewport.RateToRelativeY(lineDiff)/(texture[0].Height - 50));
                case NoteType.Div2:
                    return (float)(parent.Viewport.RateToRelativeY(lineDiff)/(texture[1].Height - 50)*3.5f);
                case NoteType.Div4:
                    return (float)(parent.Viewport.RateToRelativeY(lineDiff)/(texture[2].Height - 50)*3.5f);
                case NoteType.Div8:
                    return (float)(parent.Viewport.RateToRelativeY(lineDiff)/(texture[3].Height - 50)*3.5f);
                case NoteType.Div16:
                    return (float)(parent.Viewport.RateToRelativeY(lineDiff)/(texture[4].Height - 50)*3.5f);
                case NoteType.Div32:
                    return (float)(parent.Viewport.RateToRelativeY(lineDiff)/(texture[5].Height - 50)*3.5f);
                default:
                    throw new Exception();
            }
        }
        public int getKey()
        {
            return (int)Pitch + 12*Octave + (int)Accidental;
        }
        public void Draw(double x, double lineDiff)
        {
            Line2DRenderer lineRenderer = Line2DRenderer.GetInstance();
            parent.SpriteBatch.Draw(
                texture[(int)Type],
                new Vector2((float)x, (float)parent.NoteToY(getKey())),
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
