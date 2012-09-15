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

            if (location == Area.PARTYHOOD)
            {
                dialog.Add(301, new Dialog("I will never let anyone into this party. This is my life's goal."));
                dialog.Add(157, new Dialog("There's a pretty cool party going in this place.").add(
                                           "I wish I could sneak in myself!").add(
                                           "But you could never get past me. You're better off going to Brewtopia"));
            }
            else if (location == Area.LIQUOR_STORE)
            {
                dialog.Add(132, new Dialog("Welcome to Brews 'R' Us"));
                dialog.Add(35, new Dialog("Meow").add("Meow").add("Meow").add("Meow").add("Woof! I mean, MEOW!"));
            }
            else if (location == Area.START_SOUTH)
            {
                dialog.Add(271, new Dialog("Many years ago, I sent a brave group of adventures to find some brews, but they were never heard from again"));
                dialog.Add(242, new Dialog("We are the brave adventures on a journey for some brews!"));
                dialog.Add(181, new Dialog("Sorry, our party is full, you cannot join our quest"));
                dialog.Add(73, new Dialog("It has been many years and we still haven't found a single brew!").add(
                                          "Maybe it's time we just give up..."));
            }
            else if (location == Area.START_WEST)
            {
                dialog.Add(252, new Dialog("I have heard rumors of a Brew Utopia far to the east of here"));
            }
            else if (location == Area.START_EAST)
            {
                dialog.Add(366, new Dialog("On the other side of these trees is Brewtopia, but nobody has ever figured out how to cross them."));
                dialog.Add(351, new Dialog("Although I am riding in my plane, I cannot get past these trees! Oh no!"));
            }
            else
            {
                dialog.Add(Constants.COMPANION, new Dialog("Let's get some brews, bro!", false, Color.PapayaWhip));
            }

            return dialog;
        }
    }
}
