﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using CS8803AGA.questcontent;

namespace CS8803AGA.dialog
{
    class DialogManager
    {
        private static Dictionary<int, Dialog> dialogs;

        public static void load(Point location)
        {
            dialogs = DialogDefinitions.charactersAt(location);
        }

        /**
         * Checks if special quest dialog should be set for this character
         *@param character the character to check for
         */
        public static void checkForQuestDialog(int character)
        {
            if (Quest.currentQuest != Quest.QUEST_TYPE.GET_COMPANION)
            {
                if (!Quest.talkedToStolenBrew)
                {
                    dialogs[character].setDialog(0);
                    if (character == Constants.STOLEN_BREW)
                    {
                        Quest.talkedToStolenBrew = true;
                    }
                }
                else if (!(Quest.talkedToBrewThief1 && Quest.talkedToBrewThief2 && Quest.talkedToBrewThief3))
                {
                    dialogs[character].setDialog(1);
                    switch (character)
                    {
                    case Constants.STOLEN_BREW:
                        dialogs[character].setDialog(0);
                        break;
                    case Constants.BREW_THIEF1:
                        Quest.talkedToBrewThief1 = true;
                        break;
                    case Constants.BREW_THIEF2:
                        Quest.talkedToBrewThief2 = true;
                        break;
                    case Constants.BREW_THIEF3:
                        Quest.talkedToBrewThief3 = true;
                        break;
                    }
                }
                else if (!Quest.talkedToStolenBrewAgain)
                {
                    dialogs[character].setDialog(1);
                    if (character == Constants.STOLEN_BREW)
                    {
                        Quest.talkedToStolenBrewAgain = true;
                    }
                }
                else
                {
                    dialogs[character].setDialog(2);
                    switch (character)
                    {
                    case Constants.STOLEN_BREW:
                        if (!Quest.drankStolenBrew)
                        {
                            dialogs[character].setDialog(1);
                        }
                        break;
                    case Constants.BREW_THIEF2:
                        if (Quest.drankStolenBrew)
                        {
                            dialogs[character].setDialog(3);
                        }
                        Quest.drankStolenBrew = true;
                        break;
                    case Constants.BREW_THIEF3:
                        if (Quest.hasPartyKey)
                        {
                            dialogs[character].setDialog(3);
                        }
                        Quest.hasPartyKey = true;
                        break;
                    }
                }
            }

        }

        /**
         * Gets the next dialog for the specified character
         *@param character which character to get the dialog for
         *@return the dialog or null if the character does not exist
         */
        public static Dialog get(int character)
        {
            
            if (dialogs.ContainsKey(character))
            {
                checkForQuestDialog(character);

                Dialog d = dialogs[character].getCurrent();
                // maybe need to do something special to set next flag??

                dialogs[character].next();
                return d;
            }
            return null;
        }
    }
}