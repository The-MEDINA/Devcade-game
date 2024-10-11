using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Devcade;
using Microsoft.Xna.Framework.Content;

namespace WildWestShootout
{
	public class QuickDraw1P:Game1
	{
        SpriteBatch _spriteBatch;
        SpriteFont _font;
        Texture2D player1;
        public static ContentManager _content;
        //thank f*ck i got this to work. Thanks Ella :>
        //starts the QuickDraw(1P) gamemode.
        public QuickDraw1P(SpriteBatch spriteBatch, SpriteFont font, ContentManager Content)
        {
            _spriteBatch = spriteBatch;
            _font = font;
            _content = Content;
            LoadThis();
        }
        //loading assets here.
        public void LoadThis()
        {
            player1 = _content.Load<Texture2D>("P1Standing - Temp");
        }
        //drawing out the game here.
        public void DrawThis()
        {
            _spriteBatch.DrawString(_font, "yes?", new Vector2(100, 500), Color.Black);
            _spriteBatch.Draw(player1, new Vector2(0,0), Color.White);	
        }
    }
}
