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
    /// (指定された描画領域に描画する(DrawableGameComponentの派生クラス)
    /// </summary>
    abstract class SeparatedComponent : DrawableGameComponent
    {
        public RelativeViewport Viewport { get; }
        public SpriteBatch SpriteBatch { get; private set; }
        public SeparatedComponent(Game game, RelativeViewport viewport) : base(game)
        {
            this.Viewport = viewport;
        }
        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);

            base.LoadContent();
        }
        protected abstract void separatelyDraw(GameTime gameTime, SpriteBatch spriteBatch);
        public override void Draw(GameTime gameTime)
        {
            Viewport tmp = SpriteBatch.GraphicsDevice.Viewport;
            SpriteBatch.GraphicsDevice.Viewport = Viewport.GetIntViewport();
            SpriteBatch.Begin();
            separatelyDraw(gameTime, SpriteBatch);
            SpriteBatch.End();
            SpriteBatch.GraphicsDevice.Viewport = tmp;

            base.Draw(gameTime);
        }
    }
}
