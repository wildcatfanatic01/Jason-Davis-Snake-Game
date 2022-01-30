using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
    class Settings
    {

        public static int Width { get; set; }       //This sets the Width as an int class.
        public static int Height { get; set; }      //This sets the Height as an int class.

        public static string directions;            //This will be used to set the direction.

        public Settings()                           //This is the default Settings Function.
        {
            Width = 16;
            Height = 16;
            directions = "left";                    //The default direction for the Snake will be Left.
        }


    }
}
