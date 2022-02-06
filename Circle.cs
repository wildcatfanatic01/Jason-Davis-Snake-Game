using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
    class Circle
    {
        public int X { get; set; }      //This will identify the X coordinate location of the circle.
        public int Y { get; set; }      //This will identify the Y coordinate location of the circle.

        public Circle()                 //This function will reset the X and Y coordinate locations to 0, i.e. the Origin.
        {
            X = 0;
            Y = 0;
        }


    }
}