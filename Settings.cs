using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
    class Settings
    {

        public static int Width { get; set; }       //This sets the Width property for the window of the game.
        public static int Height { get; set; }      //This sets the Height property for the window of the game.

        public static string directions;            //This will be used to set the direction.

        public Settings()                           //This is the default Settings Function.
        {
            Width = 16;
            Height = 16;
            directions = "left";                    //The default direction for the Snake will be Left.
        }


    }
}
