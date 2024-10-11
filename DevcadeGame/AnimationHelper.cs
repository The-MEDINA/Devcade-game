using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DevcadeGame
{
    public class AnimationHelper
    {
        private List<Rectangle> spriteSheetList = new();
        public AnimationHelper()
        {

        }
        public int Animate(Texture2D spriteSheet, int numberOfFrames, int size, GameTime _gameTime)
        {
            for (int i = 0; i < numberOfFrames; i++)
            {
                spriteSheetList.Add(new(i*size, 0, size, size));
                return i;
            }
            return 0;
        }
    }
}