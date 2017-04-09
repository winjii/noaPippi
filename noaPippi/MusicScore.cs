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
    /// 音符を表示する辺りの領域を担当するクラス
    /// </summary>
    class MusicScore : SeparatedComponent
    {
        private SpriteBatch spriteBatch;
        private Texture2D black;
        public MusicScore(Game game, GraphicsDevice graphicsDevice) : base(game)
        {
            spriteBatch = new SpriteBatch(graphicsDevice);
        }
        protected override void LoadContent()
        {
            base.LoadContent();

            black = Game.Content.Load<Texture2D>("black");
        }

        protected override void separatelyDraw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }
    }
}
