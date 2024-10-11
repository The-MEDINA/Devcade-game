using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace DevcadeGame
{
    public class Animator
    {
        List<Rectangle> spriteSheetList = new();
        int whichFrame = 0;
        public void PrepareToAnimate(SpriteBatch spritebatch, Texture2D spriteSheet, GameTime gametime, int frames, int size, int positionX, int positionY)
        {
            spriteSheetList.Clear();
            whichFrame = 0;
            for (int i = 0; i < frames; i++)
            {
                spriteSheetList.Add(new((i* size), 0, size, size));
            }
            Animating(spritebatch, spriteSheet, gametime, positionX, positionY, frames);
        }
        public void Animating(SpriteBatch spriteBatch, Texture2D spriteSheet, GameTime gametime, int positionX, int positionY, int frames)
        {
            float timer = 0;
            while (whichFrame < frames)
            {
                spriteBatch.Draw(spriteSheet, new Vector2(positionX, positionY), spriteSheetList[whichFrame], Color.White);
                if (timer > 10000000)
                {
                    whichFrame++;
                    timer = 0;
                }
                timer += (float)gametime.ElapsedGameTime.TotalMilliseconds;
            }
        }
    }
}