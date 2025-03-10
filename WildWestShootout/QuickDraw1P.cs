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
using Microsoft.Xna.Framework.Audio;
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
        Texture2D background;
        List<Rectangle>player1Unholsters = new();
        List<Rectangle>Player1GunUp = new();
        List<Rectangle>PlayerShoots = new();
        List<Rectangle>P1Lost = new();
        List<Rectangle> enemyCutout = new();
        List<SoundEffect> soundEffects = new();
        public static ContentManager _content;
        int quickDrawStep = 0;
        int enemyStep = 0;
        bool unstick = false;
        bool unstickEnemy = false;
        int enemyFrames;
        int startTimer = 3000;
        int pauseTimer = 3000;
        float speedBonus = 0;
        Random rnjesus = new Random();
        Animator playerAnimator = new Animator();
        Animator enemyAnimator = new Animator();
        float[] enemyCountdown = new float[2];
        int countdown;
        bool startDraw = false;
        int highscore = 0;
        bool playerLost = false;
        int[] speedBounds = {750, 3000};
        int roundCount = 1;
        int enemySpeed;
        //starts the QuickDraw(1P) gamemode.
        public QuickDraw1P(SpriteBatch spriteBatch, SpriteFont font, ContentManager Content)
        {
            _spriteBatch = spriteBatch;
            _font = font;
            _content = Content;
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
            soundEffects.Add(Content.Load<SoundEffect>("impactful shot"));
            soundEffects.Add(Content.Load<SoundEffect>("intro noise"));
            background = _content.Load<Texture2D>("BACKGROUND");
            ResetGame();
        }
        /*Here's where game logic and cutouts are done.
        cutouts for any sprites MUST be done here, else they don't animate properly.*/
        public void UpdateThis(GameTime _gameTime)
        {
            EnemyLogic(_gameTime);
            if (StartGame(_gameTime) == true)
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
                if (playerLost == true && unstick == false)
                {
                    enemyStep = 8;
                    P1Lost = playerAnimator.CreateCutout(32,128);
                    soundEffects[0].CreateInstance().Play();
                    unstick = true;
                }
                if (enemyStep >= 7 && !(quickDrawStep == 4))
                {
                    playerLost = true;
                }
                else if (playerLost == false && startDraw == true)
                {
                    if (Input.GetButton(1, Input.ArcadeButtons.StickDown) && quickDrawStep == 0)
                    {
                        quickDrawStep = 1;
                        player1Unholsters = playerAnimator.CreateCutout(9,128);
                    }
                    else if (Input.GetButton(1, Input.ArcadeButtons.StickUp) && quickDrawStep == 1)
                    {
                        quickDrawStep = 2;
                        Player1GunUp = playerAnimator.CreateCutout(9,128);
                    }
                    else if (Input.GetButton(1, Input.ArcadeButtons.A1) && quickDrawStep == 2)
                    {
                        quickDrawStep = 3;
                    }
                    else if(Input.GetButton(1, Input.ArcadeButtons.A2) && quickDrawStep == 3)
                    {
                        quickDrawStep = 4;
                        PlayerShoots = playerAnimator.CreateCutout(34,128);
                    }
                    else if (quickDrawStep == 4)
                    {
                        if (PauseGame(_gameTime) == false)
                        {
                            NextRound();
                        }
                    }
                }
            }
        }
        //drawing out the game here.
        //I'm not a big fan of how game logic basically runs twice i'm gonna be honest.
        public void DrawThis(GameTime _gameTime)
        {
            _spriteBatch.Draw(background, new Vector2(0,432),  Color.White);
            _spriteBatch.DrawString(_font, $"Round: {roundCount}", new Vector2(0, 200), Color.Black);
            _spriteBatch.DrawString(_font, $"HighScore: {highscore:D8}", new Vector2(0, 900), Color.Black);
            enemyAnimator.AnimateThis(enemyStepToDraw, enemyFrames, 250, 490, _spriteBatch, _gameTime, enemyCutout, (SpriteEffects)1);
            if (enemyStep == 6 && unstickEnemy == false)
            {
                _spriteBatch.DrawString(_font, "click", new Vector2(250, 426), Color.Black);
            }
            if (playerLost == true && !(quickDrawStep == 4))
            {
                playerAnimator.AnimateThis(P1lostRaw, 32, 32, 490, _spriteBatch, _gameTime, P1Lost, 0);
                _spriteBatch.DrawString(_font, $"You lose! \nPress Menu button to retry.", new Vector2(0, 232), Color.Black);
            }
            else
            {
                if (countdown <= 0)
                {
                    _spriteBatch.DrawString(_font, "Draw!", new Vector2(150, 300), Color.Black);
                }
                if (quickDrawStep == 0)
                {
                    _spriteBatch.Draw(player1Stands, new Vector2(32,490),  Color.White);
                }
                else if(quickDrawStep == 1)
                {
                    playerAnimator.AnimateThis(P1UnholstersRaw, 9, 32, 490, _spriteBatch, _gameTime, player1Unholsters, 0);
                }
                else if(quickDrawStep == 2 || quickDrawStep == 3)
                {
                    playerAnimator.AnimateThis(P1GunUpRaw, 9, 32, 490, _spriteBatch, _gameTime, Player1GunUp, 0);
                    if (quickDrawStep == 3)
                    {
                        _spriteBatch.DrawString(_font, "click", new Vector2(32, 426), Color.Black);
                    }
                }
                else if (quickDrawStep == 4)
                {
                    playerAnimator.AnimateThis(P1ShootsRaw, 34, 32, 490, _spriteBatch, _gameTime, PlayerShoots, 0);
                    _spriteBatch.DrawString(_font, $"Nice draw! You win!\nSpeed + Difficulty bonus: \n{speedBonus}", new Vector2(0, 232), Color.Black);
                }
            }
        }
        public void EnemyLogic(GameTime _gameTime)
        {
            System.Console.WriteLine($"lower: {speedBounds[0]} upper: {speedBounds[1]} speed: {enemySpeed}");
            if (startDraw == true && quickDrawStep != 4 && enemyStep != 7)
            {
                enemyCountdown[1] += (float)_gameTime.ElapsedGameTime.Milliseconds;
                speedBonus -= (float)_gameTime.ElapsedGameTime.Milliseconds;
            }
            if (quickDrawStep == 4)
            {
                enemyFrames = 32;
                enemyStepToDraw = P1lostRaw;
                if (unstickEnemy == false)
                {
                    enemyCutout = enemyAnimator.CreateCutout(enemyFrames,128);
                    soundEffects[0].CreateInstance().Play();
                    speedBonus = (int)((3000-(enemySpeed-speedBonus))/Math.Sqrt(enemySpeed));
                }
                unstickEnemy = true;
            }
            else
            {
                if (speedBonus >= (enemySpeed/4)*3)
                {
                    enemyStep = 0;
                    enemyFrames = 1;
                    enemyStepToDraw = player1Stands;
                    enemyCutout = enemyAnimator.CreateCutout(enemyFrames,128);
                }
                else if (speedBonus >= enemySpeed/2)
                {
                    if (enemyStep == 0)
                    {
                        enemyFrames = 9;
                        enemyStepToDraw = P1UnholstersRaw;
                        enemyCutout = enemyAnimator.CreateCutout(enemyFrames,128);
                        enemyStep++;
                    }
                }
                else if (speedBonus >= enemySpeed/4)
                {
                    if (enemyStep == 1)
                    {
                        enemyFrames = 9;
                        enemyStepToDraw = P1GunUpRaw;
                        enemyCutout = enemyAnimator.CreateCutout(enemyFrames,128);
                        enemyStep++;
                    }
                }
                else if (speedBonus > 0)
                {
                    enemyStep = 6;
                }
            // enemyStep 6, he cocks the gun.
                else if (speedBonus <= 0)
                {
                    if (enemyStep == 6)
                    {
                        enemyFrames = 34;
                        enemyStepToDraw = P1ShootsRaw;
                        enemyCutout = enemyAnimator.CreateCutout(enemyFrames,128);
                        playerLost = true;
                        enemyStep++;
                    }
                }
            }
        }
        public bool StartGame(GameTime gameTime)
        {
            if (startTimer == 3000)
            {
                soundEffects[1].CreateInstance().Play();
            }
            if (startTimer <= 0)
            {
                return true;
            }
            else
            {
                startTimer-= gameTime.ElapsedGameTime.Milliseconds;
            }
            return false;
        }
        public bool PauseGame(GameTime gameTime)
        {
            if (pauseTimer <= 0)
            {
                pauseTimer = 3000;
                return false;
            }
            else
            {
                pauseTimer-= gameTime.ElapsedGameTime.Milliseconds;
                return true;
            }
        }
        public void ResetGame()
        {
            highscore = 0;
            roundCount = 1;
            unstick = false;
            unstickEnemy = false;
            quickDrawStep = 0;
            enemyStep = 0;
            playerLost = false;
            speedBounds[1] = 3000;
            countdown = rnjesus.Next(100,10001);
            startDraw = false;
            startTimer = 3000;
            speedBounds[0] = 750;
            enemySpeed = rnjesus.Next(speedBounds[0],speedBounds[1]);
            speedBonus = enemySpeed;
            playerAnimator.Reset();
            enemyAnimator.Reset();
        }
        public void NextRound()
        {
            highscore+= (1000+(int)speedBonus);
            roundCount++;
            unstick = false;
            unstickEnemy = false;
            quickDrawStep = 0;
            enemyStep = 0;
            playerLost = false;
            countdown = rnjesus.Next(100,10001);
            startDraw = false;
            startTimer = 3000;
            speedBounds[1] = (int)(350+(3000/(Math.Sqrt(roundCount))));
            if (speedBounds[0] > 350)
            {
                speedBounds[0] -= 40;
            }
            if (speedBounds[1] < 350)
            {
                speedBounds[1] = 350;
            }
            enemySpeed = rnjesus.Next(speedBounds[0],speedBounds[1]);
            speedBonus = enemySpeed;
            playerAnimator.Reset();
            enemyAnimator.Reset();
        }
    }
}