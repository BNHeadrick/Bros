using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CS8803AGA.dialog
{
    class DialogDefinitions
    {
        /**
         * Gets the dialog for the characters at a certain location
         *@param location the location
         *@return the dialog
         */
        public static Dictionary<int, Dialog> charactersAt(Point location)
        {
            Dictionary<int, Dialog> dialog = new Dictionary<int, Dialog>();

            if (location == Area.HOUSE)
            {
                dialog.Add(Constants.COMPANION, new Dialog("I have become drunk!", true));
                dialog.Add(181, new Dialog("Here, try this drink, I put some special stuff in it", false));
                dialog.Add(243, new Dialog("I am bunny girl", false));
                dialog.Add(271, new Dialog("I am king of all you see", false));
                dialog.Add(331, new Dialog("Talk to me again", false, Color.LightGoldenrodYellow).add(
                                           "This is a test of a really long dialog to see if we can't do anything special with super duper long dialog, ideally it will line break all on it's own", false));
            }
            else
            {
                dialog.Add(Constants.COMPANION, new Dialog("Let's get some brews, bro!", false, Color.PapayaWhip));
            }

            return dialog;
        }
    }
}
