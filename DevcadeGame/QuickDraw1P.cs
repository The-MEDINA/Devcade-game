using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Devcade;
using Microsoft.Xna.Framework.Content;
using System;

namespace WildWestShootout
{
	public class QuickDraw1P:Game1
	{
        SpriteBatch _spriteBatch;
        SpriteFont _font;
        Texture2D player1Sprite;
        TimeSpan timeMiddleMan = new TimeSpan();
        public static ContentManager _content;
        float[] convertedTimer = {0,0};
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
            player1Sprite = _content.Load<Texture2D>("P1Standing - Temp");
        }
        //drawing out the game here.
        public void DrawThis()
        {
            _spriteBatch.DrawString(_font, $"Quick Draw (1P) gamemode.", new Vector2(0, 0), Color.Black);
            _spriteBatch.Draw(player1Sprite, new Vector2(32,490), Color.White);	
        }
        //Here's where the game stuff is gonna happen (I say gonna cause as of writing this it doesn't do much)
        public void UpdateThis(GameTime _gameTime)
        {
            //- - - - - - - - - - - - - - -
            //Yes, I'm aware this is probably not the best way to do this. Or.. a good way to do this.
            //gameTime is giving me so many issues though, so this is the best I could do make it usable. 
            timeMiddleMan = timeMiddleMan+_gameTime.ElapsedGameTime;
            convertedTimer[0] = timeMiddleMan.Seconds;
            convertedTimer[1] = timeMiddleMan.Milliseconds;
            //- - - - - - - - - - - - - - -
            System.Console.WriteLine($"{convertedTimer[0]}, {convertedTimer[1]}");
            DrawThis();
        }
    }
}
