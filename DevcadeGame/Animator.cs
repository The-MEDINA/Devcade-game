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
        float timer = 0;
        /*creates the rectangle that'll be used for the animations. Returns a list of rectangles that you'll need to save.
        You can't just call this method in AnimateThis. I tried. :<*/
        public List<Rectangle> CreateCutout(int frames, int size)
        {
            spriteSheetList.Clear();
            timer = 0;
            whichFrame = 0;
            for (int i = 0; i < frames; i++)
            {
                spriteSheetList.Add(new((i* size), 0, size, size));
            }
            return spriteSheetList;
        }
        /*this method is a monstrosity oh my god.
        I think by doing it this way though, this method is very powerful.
        at least.. I hope so. As of writing this, I don't mess with like half the arguments.*/
        public void AnimateThis( Texture2D spriteSheet, int frames, int positionX, int positionY, SpriteBatch spriteBatch, GameTime gametime, List<Rectangle> spriteCutout)
        {
            if ((whichFrame < frames-1))
            {
                if (timer > 0.2)
                {
                    whichFrame++;
                    timer = 0;
                }
                timer+= (float)gametime.ElapsedGameTime.TotalMilliseconds;
            }
            spriteBatch.Draw(spriteSheet, new Vector2(positionX, positionY), spriteCutout[whichFrame], Color.White);
        }
    }
}