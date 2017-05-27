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
    /// (指定された描画領域に描画する)(DrawableGameComponentの派生クラス)
    /// </summary>
    //TODO: VirtualViewportのサポート
    abstract class SeparatedComponent : DrawableGameComponent
    {
        protected RelativeViewport viewport;
        private SpriteBatch spriteBatch;
        public SeparatedComponent(Game game, RelativeViewport viewport) : base(game)
        {
            this.viewport = viewport;
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            base.LoadContent();
        }
        protected abstract void separatelyDraw(GameTime gameTime, SpriteBatch spriteBatch);
        public override void Draw(GameTime gameTime)
        {
            Viewport tmp = spriteBatch.GraphicsDevice.Viewport;
            spriteBatch.GraphicsDevice.Viewport = viewport.GetIntViewport();
            spriteBatch.Begin();
            separatelyDraw(gameTime, spriteBatch);
            spriteBatch.End();
            spriteBatch.GraphicsDevice.Viewport = tmp;

            base.Draw(gameTime);
        }
    }
}
