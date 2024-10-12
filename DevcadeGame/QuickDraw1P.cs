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
        Texture2D P1GunUpRaw;
        List<Rectangle>player1Unholsters = new();
        List<Rectangle>Player1GunUp = new();
        public static ContentManager _content;
        int quickDrawStep = 0;
        Random rnjesus;
        Animator ugh = new Animator();
        float countdown;
        bool startDraw = false;
        //Clock givemetime; to work. Thanks Ella :>
        //starts the QuickDraw(1P) gamemode.
        public QuickDraw1P(SpriteBatch spriteBatch, SpriteFont font, ContentManager Content)
        {
            _spriteBatch = spriteBatch;
            _font = font;
            _content = Content;
            rnjesus = new Random();
            countdown = rnjesus.Next(1000,10000);
            LoadThis();
        }
        //loading assets here.
        public void LoadThis()
        {
            player1Stands = _content.Load<Texture2D>("P1Standing - Temp");
            P1UnholstersRaw = _content.Load<Texture2D>("P1Holster - Temp");
            P1GunUpRaw = _content.Load<Texture2D>("P1PullGun - Temp");
        }
        /*Here's where game logic and cutouts are done.
        cutouts for any sprites MUST be done here, else they don't animate properly.*/
        public void UpdateThis(GameTime _gameTime)
        {
            countdown -= _gameTime.ElapsedGameTime.Milliseconds;
            if (countdown >= 0)
            {
                startDraw = true;
            }
            if (Input.GetButton(1, Input.ArcadeButtons.StickDown) && quickDrawStep == 0)
            {
                quickDrawStep = 1;
                player1Unholsters = ugh.CreateCutout(9,128);
            }
            else if (Input.GetButton(1, Input.ArcadeButtons.StickUp) && quickDrawStep == 1)
            {
                quickDrawStep = 2;
                Player1GunUp = ugh.CreateCutout(9,128);
            }
            else if (Input.GetButton(1, Input.ArcadeButtons.A1) && quickDrawStep == 2)
            {
                quickDrawStep = 3;
            }
        }
        //drawing out the game here.
        public void DrawThis(GameTime _gameTime)
        {
            _spriteBatch.DrawString(_font, "Quick Draw (1P) Gamemode.", new Vector2(0, 0), Color.Black);
            if (countdown <= 0)
            {
                _spriteBatch.DrawString(_font, "Draw!", new Vector2(100, 100), Color.Black);
            }
            if (quickDrawStep == 0)
            {
                _spriteBatch.Draw(player1Stands, new Vector2(32,490),  Color.White);
            }
            else if(quickDrawStep == 1)
            {
                ugh.AnimateThis(P1UnholstersRaw, 9, 32, 490, _spriteBatch, _gameTime, player1Unholsters);
            }
            else if(quickDrawStep == 2 || quickDrawStep == 3)
            {
                ugh.AnimateThis(P1GunUpRaw, 9, 32, 490, _spriteBatch, _gameTime, Player1GunUp);
            }
        }
    }
}
