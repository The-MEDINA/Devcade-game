using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Devcade;
using Microsoft.Xna.Framework.Content;
using System;
using System.Threading;
using DevcadeGame;
using System.Collections.Generic;
using System.Net.NetworkInformation;
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
        Texture2D P1ShootsRaw;
        Texture2D P1lostRaw;
        Texture2D enemyStepToDraw;
        List<Rectangle>player1Unholsters = new();
        List<Rectangle>Player1GunUp = new();
        List<Rectangle>PlayerShoots = new();
        List<Rectangle>P1Lost = new();
        List<Rectangle> enemyCutout = new();
        public static ContentManager _content;
        int quickDrawStep = 0;
        int enemyStep = 0;
        bool unstick = false;
        bool unstickEnemy = false;
        int enemyFrames;
        Random rnjesus;
        Animator ugh = new Animator();
        Animator enemyAnimator = new Animator();
        int enemyCountdown;
        int countdown;
        bool startDraw = false;
        bool tempTestingLoss = false;
        //starts the QuickDraw(1P) gamemode.
        public QuickDraw1P(SpriteBatch spriteBatch, SpriteFont font, ContentManager Content)
        {
            _spriteBatch = spriteBatch;
            _font = font;
            _content = Content;
            rnjesus = new Random();
            enemyCountdown = rnjesus.Next(65,66);
            countdown = rnjesus.Next(1000,10001);
            LoadThis();
        }
        //loading assets here.
        public void LoadThis()
        {
            player1Stands = _content.Load<Texture2D>("P1Standing - Temp");
            P1UnholstersRaw = _content.Load<Texture2D>("P1Holster - Temp");
            P1GunUpRaw = _content.Load<Texture2D>("P1PullGun - Temp");
            P1ShootsRaw = _content.Load<Texture2D>("P1Shoots - Temp");
            P1lostRaw = _content.Load<Texture2D>("P1Lost - Temp");
        }
        /*Here's where game logic and cutouts are done.
        cutouts for any sprites MUST be done here, else they don't animate properly.*/
        public void UpdateThis(GameTime _gameTime)
        {
            countdown -= _gameTime.ElapsedGameTime.Milliseconds;
            if (Input.GetButton(1, Input.ArcadeButtons.Menu))
            {
                ResetGame();
            }
            if (countdown <= 0)
            {
                startDraw = true;
            }
            EnemyLogic(_gameTime, rnjesus.Next(65,66));
            if (tempTestingLoss == true && unstick == false)
            {
                P1Lost = ugh.CreateCutout(32,128);
                unstick = true;
            }
            if (enemyStep >= 7 && !(quickDrawStep == 4))
            {
                tempTestingLoss = true;
            }
            else if (tempTestingLoss == false && startDraw == true)
            {
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
                else if(Input.GetButton(1, Input.ArcadeButtons.A2) && quickDrawStep == 3)
                {
                    quickDrawStep = 4;
                    PlayerShoots = ugh.CreateCutout(34,128);
                }
            }
        }
        //drawing out the game here.
        //I'm not a big fan of how game logic basically runs twice i'm gonna be honest.
        public void DrawThis(GameTime _gameTime)
        {
            _spriteBatch.DrawString(_font, "Quick Draw (1P) Gamemode.", new Vector2(0, 0), Color.Black);
            enemyAnimator.AnimateThis(enemyStepToDraw, enemyFrames, 250, 490, _spriteBatch, _gameTime, enemyCutout, (SpriteEffects)1);
            if (tempTestingLoss == true && !(quickDrawStep == 4))
            {
                ugh.AnimateThis(P1lostRaw, 32, 32, 490, _spriteBatch, _gameTime, P1Lost, 0);
            }
            else
            {
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
                    ugh.AnimateThis(P1UnholstersRaw, 9, 32, 490, _spriteBatch, _gameTime, player1Unholsters, 0);
                }
                else if(quickDrawStep == 2 || quickDrawStep == 3)
                {
                    ugh.AnimateThis(P1GunUpRaw, 9, 32, 490, _spriteBatch, _gameTime, Player1GunUp, 0);
                }
                else if (quickDrawStep == 4)
                {
                    ugh.AnimateThis(P1ShootsRaw, 34, 32, 490, _spriteBatch, _gameTime, PlayerShoots, 0);
                }
            }
        }
        public void EnemyLogic(GameTime _gameTime,int enemySpeed)
        {
            if (startDraw == true)
            {
                enemyCountdown -= (int)_gameTime.ElapsedGameTime.Milliseconds;
            }
            if (enemyCountdown <= 0)
            {
                enemyStep++;
                enemyCountdown = enemySpeed;
            }
            /*There might be a bug *somewhere* that's causing the enemy's animations to not play right when enemyStep is incremented by 1.
            As a bandaid fix I just incremented by 1, ran code, then incremented again.
            we love bandaid fixes*/
            if (quickDrawStep == 4)
            {
                enemyFrames = 32;
                enemyStepToDraw = P1lostRaw;
                if (unstickEnemy == false)
                    enemyCutout = enemyAnimator.CreateCutout(enemyFrames,128);
                unstickEnemy = true;
            }
            else
            {
                if (enemyStep == 0)
                {
                    enemyFrames = 1;
                    enemyStepToDraw = player1Stands;
                    enemyCutout = enemyAnimator.CreateCutout(enemyFrames,128);
                }
                if (enemyStep == 1)
                {
                    enemyFrames = 9;
                    enemyStepToDraw = P1UnholstersRaw;
                    enemyCutout = enemyAnimator.CreateCutout(enemyFrames,128);
                    enemyStep++;
                }
                if (enemyStep == 4)
                {
                    enemyFrames = 9;
                    enemyStepToDraw = P1GunUpRaw;
                    enemyCutout = enemyAnimator.CreateCutout(enemyFrames,128);
                    enemyStep++;
                }
            // enemyStep 6, he cocks the gun.
                if (enemyStep == 7)
                {
                    enemyFrames = 34;
                    enemyStepToDraw = P1ShootsRaw;
                    enemyCutout = enemyAnimator.CreateCutout(enemyFrames,128);
                    tempTestingLoss = true;
                    enemyStep++;
                }
            }
        }
        public void ResetGame()
        {
            unstick = false;
            unstickEnemy = false;
            quickDrawStep = 0;
            enemyStep = 0;
            tempTestingLoss = false;
            enemyCountdown = rnjesus.Next(65,66);
            countdown = rnjesus.Next(100,10001);
            startDraw = false;
        }
    }
}
