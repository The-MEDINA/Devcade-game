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
        public void PrepareToAnimate(SpriteBatch spritebatch, Texture2D spriteSheet, int frames, int size, int positionX, int positionY)
        {
            for (int i = 0; i < frames; i++)
            {
                spriteSheetList.Add(new(i*size, 0, size, size));
            }
            Animating(spritebatch, spriteSheet, positionX, positionY);
        }
        public void Animating(SpriteBatch spriteBatch, Texture2D spriteSheet, int positionX, int positionY)
        {
            spriteBatch.Draw(spriteSheet, new Vector2(positionX, positionY), spriteSheetList[0], Color.White);
        }
    }
}