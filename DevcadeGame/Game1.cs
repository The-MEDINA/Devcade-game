using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Devcade;
using System;

namespace WildWestShootout
{
	public class Game1 : Game
	{
		GraphicsDeviceManager _graphics;
		SpriteBatch _spriteBatch;
		QuickDraw1P games;
		Texture2D targetsprite;
		Texture2D uhhh;
		Texture2D ok;
		string whichGame = "Demo";
		private SpriteFont plswork;	
		bool gameSet = false;
		//bool showthis = false;
		/// <summary>
		/// Stores the window dimensions in a rectangle object for easy use
		/// </summary>
		private Rectangle windowSize;
		
		/// <summary>
		/// Game constructor
		/// </summary>
		public Game1()
		{
			_graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			IsMouseVisible = true;
		}

		/// <summary>
		/// Performs any setup that doesn't require loaded content before the first frame.
		/// </summary>
		protected override void Initialize()
		{
			// Sets up the input library
			Input.Initialize();
			//Persistence.Init(); Uncomment if using the persistence section for save and load

			// Set window size if running debug (in release it will be fullscreen)
			#region
#if DEBUG
			_graphics.PreferredBackBufferWidth = 420;
			_graphics.PreferredBackBufferHeight = 980;
			_graphics.ApplyChanges();
#else
			_graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
			_graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
			_graphics.ApplyChanges();
#endif
			#endregion
			
			// TODO: Add your initialization logic here

			windowSize = GraphicsDevice.Viewport.Bounds;
			
			base.Initialize();
		}

		/// <summary>
		/// Performs any setup that requires loaded content before the first frame.
		/// </summary>
		protected override void LoadContent()
		{
			_spriteBatch = new SpriteBatch(GraphicsDevice);

			// TODO: use this.Content to load your game content here
			targetsprite = Content.Load<Texture2D>("1 0");
			uhhh = Content.Load<Texture2D>("1 1");
			ok = Content.Load<Texture2D>("1 2");
			plswork = Content.Load<SpriteFont>("text");
		}

		/// <summary>
		/// Your main update loop. This runs once every frame, over and over.
		/// </summary>
		/// <param name="gameTime">This is the gameTime object you can use to get the time since last frame.</param>
		protected override void Update(GameTime gameTime)
		{
			Input.Update(); // Updates the state of the input library

			//Exit code here.
			//System.Console.WriteLine(Keyboard.GetState().IsKeyDown(Keys.Escape));
			if (Keyboard.GetState().IsKeyDown(Keys.Escape) || (Input.GetButton(1, Input.ArcadeButtons.Menu) && Input.GetButton(2, Input.ArcadeButtons.Menu)))
			{
				Exit();
			}
			//select game.
			if (Input.GetButton(1, Input.ArcadeButtons.A1) && gameSet == false)
			{
				games = new QuickDraw1P(_spriteBatch, plswork, Content);
				whichGame = "QuickDraw1P";
				gameSet = true;
			}
			base.Update(gameTime);
		}

		/// <summary>
		/// Your main draw loop. This runs once every frame, over and over.
		/// </summary>
		/// <param name="gameTime">This is the gameTime object you can use to get the time since last frame.</param>
		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);
			
			// Batches all the draw calls for this frame, and then performs them all at once
			_spriteBatch.Begin();
			//_spriteBatch.Draw(animate, new Vector2(0,200), Color.White);
			if (whichGame.Equals("Demo"))
			{
			_spriteBatch.Draw(targetsprite, new Vector2(0,0), Color.White);			
			_spriteBatch.Draw(uhhh, new Vector2(200,100), Color.White);
			_spriteBatch.DrawString(plswork, "OK!", new Vector2(100, 100), Color.Black);		
			}
			else if (whichGame.Equals("QuickDraw1P"))
			{
				games.DrawThis();
			}
			_spriteBatch.End();

			base.Draw(gameTime);
		}
	}
	
}