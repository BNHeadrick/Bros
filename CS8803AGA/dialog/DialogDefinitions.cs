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
            else if (location == Area.PARTYHOOD_NORTH)
            {
                dialog.Add(Constants.STOLEN_BREW, new Dialog("One of these fools has stolen my precious Brew!").add(
                                                             "Have you figured out who it was? Cool, go beat them up for me!").add(
                                                             "What the hell, why'd you drink my brew?"));
                dialog.Add(Constants.BREW_THIEF1, new Dialog("What do you want?").add(
                                                             "You think I stole the brew? No way, it wasn't me. Let's go to Brewtopia together!").add(
                                                             "Ha, ha, ha, fool! You fell for my trick, I'm going to leave you behind and go to Brewtopia all by myself."));
                dialog.Add(Constants.BREW_THIEF2, new Dialog("What do you want?").add(
                                                             "A brew? I don't even know what that is.").add(
                                                             "You caught me, I took the brew. Here, you can have it back.*Companion shotgunned the brew.*\n").add(
                                                             "Here, you can drink this other brew I stole too!\n*Companion shotgunned the brew.*"));
                dialog.Add(Constants.BREW_THIEF3, new Dialog("Leave me alone, please").add(
                                                             "I saw someone else take a brew. I think it was that guy down there.").add(
                                                             "Uggghh, please take this legendary haunted key, just leave me alone!").add(
                                                             "I am plagued by far less ghosts now that I no longer hold the haunted key. Thank you for mugging me."));
            }
            else if (location == Area.LIQUOR_STORE)
            {
                dialog.Add(132, new Dialog("Welcome to Brews 'R' Us.  Here's a ton of brews!").add("Welcome to Brews 'R' Us"));
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
                dialog.Add(Constants.COMPANION, new Dialog("Let's get some brews, bro!", false, Color.PapayaWhip).add("SWEET, SOME TASTY BREWS!"));
            }

            return dialog;
        }
    }
}
