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
        private VertexPositionColor staffNotation;
        public MusicScore(Game game, Viewport viewport, SpriteBatch spriteBatch) : base(game, viewport, spriteBatch)
        {

        }
        protected override void LoadContent()
        {
            base.LoadContent();
            

        }

        protected override void separatelyDraw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }
    }
}
