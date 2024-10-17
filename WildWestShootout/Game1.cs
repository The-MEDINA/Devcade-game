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
		string whichGame = "Demo";
		private SpriteFont theFont;	
		bool gameSet = false;
		private Rectangle windowSize;

		public Game1()
		{
			_graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			IsMouseVisible = true;
		}
		protected override void Initialize()
		{
			// Sets up the input library
			Input.Initialize();
			//Persistence.Init(); Uncomment if using the persistence section for save and load
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
			// huh
			windowSize = GraphicsDevice.Viewport.Bounds;
			
			base.Initialize();
		}
		protected override void LoadContent()
		{
			_spriteBatch = new SpriteBatch(GraphicsDevice);
			theFont = Content.Load<SpriteFont>("text");
		}
		protected override void Update(GameTime gameTime)
		{
			Input.Update(); // Updates the state of the input library

			//Exit code here.
			if (Keyboard.GetState().IsKeyDown(Keys.Escape) || (Input.GetButton(1, Input.ArcadeButtons.Menu) && Input.GetButton(2, Input.ArcadeButtons.Menu)))
			{
				Exit();
			}
			//select game.
			if (Input.GetButton(1, Input.ArcadeButtons.A1) && gameSet == false)
			{
				games = new QuickDraw1P(_spriteBatch, theFont, Content);
				whichGame = "QuickDraw1P";
				gameSet = true;
			}
			if (whichGame.Equals("QuickDraw1P"))
			{
				games.UpdateThis(gameTime);
			}
			base.Update(gameTime);
		}
		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);
			
			// Batches all the draw calls for this frame, and then performs them all at once
			_spriteBatch.Begin();
			if (whichGame.Equals("Demo"))
			{
			_spriteBatch.DrawString(theFont, "Wild West Shootout!\nHow to play:", new Vector2(50, 300), Color.Black);	
			_spriteBatch.DrawString(theFont, "When it says draw...\nFlick the stick down\nFlick the stick up\nPress red button\nPress blue button\nDo it fast and win!\n\nPress red button \nto start.", new Vector2(50, 450), Color.Black);
			_spriteBatch.DrawString(theFont, "Version CSH Game Jam 2024", new Vector2(0, 900), Color.Black);			
			}
			else if (whichGame.Equals("QuickDraw1P"))
			{
				games.DrawThis(gameTime);
			}
			_spriteBatch.End();
			base.Draw(gameTime);
		}
	}
	
}