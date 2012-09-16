using System;
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
        public static void checkForQuestDialog(int character, int debug_party_character)
        {
            if (character == Constants.LIQUERMERCH)
            {
                if (!Quest.talkedToBrewMerch)
                {
                    dialogs[character].setDialog(0);
                    Quest.talkedToBrewMerch = true;
                }
                else
                {
                    dialogs[character].setDialog(1);
                }
            }
            if (Quest.currentQuest == Quest.QUEST_TYPE.GET_COMPANION && character == Constants.COMPANION)
            {
                if (!Quest.talkedToBrewMerch)
                {
                    dialogs[character].setDialog(0);
                }
                else
                {
                    dialogs[character].setDialog(1);
                    Quest.currentQuest = Quest.QUEST_TYPE.CRASH_PARTY;
                    Quest.talkedToCompanion = true;
                }
            }

            else if (Quest.currentQuest != Quest.QUEST_TYPE.GET_COMPANION &&
                (character == Constants.STOLEN_BREW || character == Constants.BREW_THIEF1 ||
                character == Constants.BREW_THIEF2 || character == Constants.BREW_THIEF3))
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
                            Quest.currentQuest = Quest.QUEST_TYPE.PROTECT_COMPANION;
                            break;
                    }
                }
            }
            else if (Quest.atParty)
            {
                if (Quest.hasBrew)
                {
                    dialogs[character].setDialog(1);
                    if (character != Constants.BREW_MAIDEN && character != Constants.COOK)
                    {
                        Quest.hasBrew = false;
                        // give brew to debug_party_character
                        Quest.brewsGiven[debug_party_character]++;
                    }
                }
                else if (Quest.hasFood)
                {
                    if (character == Constants.BREW_MAIDEN)
                    {
                        dialogs[character].setDialog(1);
                    }
                    else
                    {
                        dialogs[character].setDialog(2);
                        if (character != Constants.COOK)
                        {
                            Quest.hasFood = false;
                            // give food to debug_party_character
                            Quest.foodGiven[debug_party_character]++;
                        }
                    }
                }
                else
                {
                    dialogs[character].setDialog(0);
                    if (character == Constants.COOK)
                    {
                        Quest.hasFood = true;
                    }
                    else if (character == Constants.BREW_MAIDEN)
                    {
                        Quest.hasBrew = true;
                    }
                }
                Quest.checkPartyQuest();
            }
        }

        /**
         * Gets the next dialog for the specified character
         *@param character which character to get the dialog for
         *@return the dialog or null if the character does not exist
         */
        public static Dialog get(int character)
        {
            // debug party dialog
            int party_character = character;
            if (Quest.atParty && character != Constants.BREW_MAIDEN && character != Constants.COOK)
            {
                character = Constants.DEBUG_PARTY_DIALOG;
            }

            if (dialogs.ContainsKey(character))
            {
                checkForQuestDialog(character, party_character);

                Dialog d = dialogs[character].getCurrent();
                // maybe need to do something special to set next flag??
                if (Quest.gameOver)
                {
                    return new Dialog("Game over, you win");
                }

                dialogs[character].next();
                return d;
            }
            return null;
        }
    }
}
