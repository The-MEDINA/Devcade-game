using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace DevcadeGame
{
    public class Clock
    {
        GameTime rawGameTime;
        TimeSpan timeMiddleMan;
        float[] timeInFloat = new float[2];
        public Clock(GameTime _gameTime)
        {
            rawGameTime = _gameTime;
            //testingthis();
        }
        public float[] GetTheTime()
        {
            timeMiddleMan += timeMiddleMan+rawGameTime.ElapsedGameTime;
            timeInFloat[0] = timeMiddleMan.Seconds;
            timeInFloat[1] = timeMiddleMan.Milliseconds;
            return timeInFloat;
        }
        public void testingthis()
        {
            System.Console.WriteLine(rawGameTime.ElapsedGameTime);
        }
    }
}