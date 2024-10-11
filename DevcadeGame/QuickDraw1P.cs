using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Devcade;
using Microsoft.Xna.Framework.Content;
using System;
using System.Threading;
using DevcadeGame;
//using DevcadeGame;

namespace WildWestShootout
{
	public class QuickDraw1P:Game1
	{
        //this is how to do time (ish)
        //testingthisagain = (float)_gameTime.ElapsedGameTime.TotalSeconds;
        SpriteBatch _spriteBatch;
        SpriteFont _font;
        Texture2D player1Sprite;
        Texture2D player1Stands;
        Texture2D player1Unholsters;
        bool canIpressthis = false;
        public static ContentManager _content;
        AnimationHelper animateThis = new AnimationHelper();
        float[] convertedTimer = {0,0};
        //Clock givemetime; to work. Thanks Ella :>
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
            //player1Sprite = _content.Load<Texture2D>("P1Standing - Temp");
            player1Stands = _content.Load<Texture2D>("P1Standing - Temp");
            player1Unholsters = _content.Load<Texture2D>("P1Holster - Temp");
        }
        //drawing out the game here.
        public void DrawThis()
        {
            player1Sprite = player1Stands;
            _spriteBatch.DrawString(_font, $"Quick Draw (1P) gamemode.\n{canIpressthis}", new Vector2(0, 0), Color.Black);
            _spriteBatch.Draw(player1Sprite, new Vector2(32,490), Color.White);	
        }
        //Here's where the game stuff is gonna happen (I say gonna cause as of writing this it doesn't do much)
        public void UpdateThis(GameTime _gameTime)
        {
            if (Input.GetButton(1, Input.ArcadeButtons.StickDown))
            {
                Console.WriteLine(animateThis.Animate(player1Unholsters, 9, 64, _gameTime));
                canIpressthis = true;
            }
            DrawThis();
        }
    }
}
