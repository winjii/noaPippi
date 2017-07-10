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
        StaffNotation staffNotationG;
        StaffNotation staffNotationF;
        public MusicScore(Game game, RelativeViewport viewport) : base(game, viewport)
        {
            RelativeViewport vG = new RelativeViewport.FormedChild(
                viewport, 0, 0.5, false, false);
            RelativeViewport vF = new RelativeViewport.FormedChild(
                viewport, 0.5, 0.5, false, false);
            staffNotationG = new StaffNotation(Clef.ClefType.G, game, vG);
            staffNotationF = new StaffNotation(Clef.ClefType.F, game, vF);
            game.Components.Add(staffNotationG);
            game.Components.Add(staffNotationF);
        }
        protected override void LoadContent()
        {
            base.LoadContent();
        }

        protected override void separatelyDraw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            
            /*
            Line2DRenderer test = Line2DRenderer.GetInstance();
            Vector2 p00 = new Vector2(0, 0);
            Vector2 p01 = new Vector2((float)viewport.GetAbsoluteWidth(), 0);
            Vector2 p10 = new Vector2(0, (float)viewport.GetAbsoluteHeight());
            Vector2 p11 = new Vector2((float)viewport.GetAbsoluteWidth(), (float)viewport.GetAbsoluteHeight());
            test.Draw(spriteBatch, p00, p01, 10, Color.Red);
            test.Draw(spriteBatch, p01, p11, 10, Color.Red);
            test.Draw(spriteBatch, p11, p10, 10, Color.Red);
            test.Draw(spriteBatch, p10, p00, 10, Color.Red);
            test.Draw(spriteBatch, p01 + new Vector2(-10, 10), p10 + new Vector2(10, -10), 20, Color.Black);
            */
        }
    }
}
