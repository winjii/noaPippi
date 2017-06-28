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
        RelativeViewport staffNotationViewport;
        public MusicScore(Game game, RelativeViewport viewport) : base(game, viewport)
        {
            staffNotationViewport = viewport.AddFreeChild(0.1, 0.1, 0.8, 0.8);
        }
        protected override void LoadContent()
        {
            base.LoadContent();
            
        }

        protected override void separatelyDraw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            /*Line2DRenderer test = new Line2DRenderer(spriteBatch, Game.Content);
            Vector2 p00 = new Vector2(0, 0);
            Vector2 p01 = new Vector2((float)viewport.GetAbsoluteWidth(), 0);
            Vector2 p10 = new Vector2(0, (float)viewport.GetAbsoluteHeight());
            Vector2 p11 = new Vector2((float)viewport.GetAbsoluteWidth(), (float)viewport.GetAbsoluteHeight());
            test.Draw(p00, p01, 10, Color.Red);
            test.Draw(p01, p11, 10, Color.Red);
            test.Draw(p11, p10, 10, Color.Red);
            test.Draw(p10, p00, 10, Color.Red);
            test.Draw(p01 + new Vector2(-10, 10), p10 + new Vector2(10, -10), 20, Color.Black);*/
        }
    }
}
