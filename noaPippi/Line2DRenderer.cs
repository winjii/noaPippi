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
    class Line2DRenderer
    {
        static private Line2DRenderer instance;
        static public Line2DRenderer GetInstance()
        {
            if (instance == null) throw new NullReferenceException();
            return instance;
        }
        static public Line2DRenderer InitializeInstance(ContentManager content)
        {
            return instance = new Line2DRenderer(content);
        }

        private Texture2D white;

        private Line2DRenderer(ContentManager content)
        {
            white = content.Load<Texture2D>("white");
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 p, Vector2 q, float thinkness, Color color)
        {
            Vector2 pos = (p + q)/2;
            Vector2 center = new Vector2((float)white.Width/2, (float)white.Height/2);
            float rad = (float)Math.Atan((q - p).Y/(q - p).X);
            Vector2 scale = new Vector2((q - p).Length(), thinkness);
            spriteBatch.Draw(white, pos, null, color, rad, center, scale, SpriteEffects.None, 0);
        }
    }
}
