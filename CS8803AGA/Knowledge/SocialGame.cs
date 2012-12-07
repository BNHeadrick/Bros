using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CS8803AGA.Knowledge
{
    class SocialGame
    {
        public string name; /**< name of the game */

        public SocialGame(string _name)
        {
            name = _name;
        }

        /**
         * Returns text to be displayed as part of the game
         *@return
         */
        public string text()
        {
            return "Put some kind of stock text thing here.";
        }
    }
}
