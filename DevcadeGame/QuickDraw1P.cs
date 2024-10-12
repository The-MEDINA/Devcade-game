using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Devcade;
using Microsoft.Xna.Framework.Content;
using System;
using System.Threading;
using DevcadeGame;
using System.Collections.Generic;
//using DevcadeGame;

namespace WildWestShootout
{
	public class QuickDraw1P:Game1
	{
        SpriteBatch _spriteBatch;
        SpriteFont _font;
        Texture2D player1Stands;
        Texture2D P1UnholstersRaw;
        List<Rectangle>player1Unholsters = new();
        bool canIpressthis = false;
        public static ContentManager _content;
        Animator ugh = new Animator();
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
            player1Stands = _content.Load<Texture2D>("P1Standing - Temp");
            P1UnholstersRaw = _content.Load<Texture2D>("P1Holster - Temp");
        }
                //Here's where the game stuff is gonna happen (I say gonna cause as of writing this it doesn't do much)
        public void UpdateThis(GameTime _gameTime)
        {
            if (Input.GetButton(1, Input.ArcadeButtons.StickDown))
            {
                canIpressthis = true;
            }
            else
            {
                canIpressthis = false;
                /*this won't really be that necessary in this project, but when an animation stops, reset its cutout.
                Doing so will fix the counters so that it'll play properly the next time it happens.*/
                player1Unholsters = ugh.CreateCutout(9,128);
            }
        }
        //drawing out the game here.
        public void DrawThis(GameTime _gameTime)
        {
            _spriteBatch.DrawString(_font, "Quick Draw (1P) Gamemode.", new Vector2(0, 0), Color.Black);
            if (canIpressthis == true)
            {
                ugh.AnimateThis(P1UnholstersRaw, 9, 32, 490, _spriteBatch, _gameTime, player1Unholsters);
            }
            else
            {
                _spriteBatch.Draw(player1Stands, new Vector2(32,490),  Color.White);	
            }

        }
    }
}
