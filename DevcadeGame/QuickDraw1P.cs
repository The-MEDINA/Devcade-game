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
        //this is how to do time (ish)
        //testingthisagain = (float)_gameTime.ElapsedGameTime.TotalSeconds;
        SpriteBatch _spriteBatch;
        SpriteFont _font;
        Texture2D player1Stands;
        Texture2D P1UnholstersRaw;
        List<Rectangle>player1Unholsters = new();
        bool canIpressthis = false;
        public static ContentManager _content;
        float aTimer = 0;
        int whichFrame = 0;
        Animator ugh;
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
            //all my prevous attempts to animate failed so now i'm scared to do it in a separate class.
            for (int i = 0; i < 9; i++)
            {
                player1Unholsters.Add(new(i*128,0,128,128));
            }

        }
        //drawing out the game here.
        public void DrawThis(GameTime _gameTime)
        {
            if (canIpressthis == true)
            {

                _spriteBatch.DrawString(_font, "bang. :>", new Vector2(0, 0), Color.Black);
                if (whichFrame < 8)
                {
                    if (aTimer > 0.2)
                    {
                        whichFrame++;
                        aTimer = 0;
                    }
                    aTimer+=_gameTime.ElapsedGameTime.Milliseconds;
                }
                 _spriteBatch.Draw(P1UnholstersRaw, new Vector2(32,490),  player1Unholsters[whichFrame], Color.White);
            }
            else
            {
                _spriteBatch.Draw(player1Stands, new Vector2(32,490),  Color.White);	
            }

        }
        //Here's where the game stuff is gonna happen (I say gonna cause as of writing this it doesn't do much)
        public void UpdateThis(GameTime _gameTime)
        {
            if (Input.GetButton(1, Input.ArcadeButtons.A2))
            {
                canIpressthis = true;
            }
        }
    }
}
