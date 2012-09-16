using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CS8803AGA.questcontent
{
    class Quest
    {
        public enum QUEST_TYPE
        {
            GET_COMPANION,
            CRASH_PARTY,
            PROTECT_COMPANION
        }

        public static QUEST_TYPE currentQuest = QUEST_TYPE.GET_COMPANION;

        //// vars for GET_COMPANION
        public static bool talkedToBrewMerch = false;
        public static bool talkedToCompanion = false;


        //// vars for CRASH_PARTY
        public static bool talkedToStolenBrew = false;
        public static bool talkedToBrewThief1 = false;
        public static bool talkedToBrewThief2 = false;
        public static bool talkedToBrewThief3 = false;
        public static bool talkedToStolenBrewAgain = false;
        public static bool drankStolenBrew = false;
        public static bool hasPartyKey = false;


        //// vars for PROTECT_COMPANION
        public static bool atParty = false;
        public static bool hasFood = false;
        public static bool hasBrew = false;

        public static bool gameOver = false;

        public static Dictionary<int, int> foodGiven = new Dictionary<int, int>();
        public static Dictionary<int, int> brewsGiven = new Dictionary<int, int>();

        public static void initPartyQuest()
        {
            foodGiven.Add(Constants.COMPANION, 0);
            foodGiven.Add(Constants.PARTY_PEOPLE1, 0);
            foodGiven.Add(Constants.PARTY_PEOPLE2, 0);
            foodGiven.Add(Constants.PARTY_PEOPLE3, 0);
            foodGiven.Add(Constants.PARTY_PEOPLE4, 0);
            foodGiven.Add(Constants.PARTY_PEOPLE5, 0);
            foodGiven.Add(Constants.PARTY_PEOPLE6, 0);
            foodGiven.Add(Constants.PARTY_PEOPLE7, 0);
            foodGiven.Add(Constants.PARTY_PEOPLE8, 0);
            foodGiven.Add(Constants.PARTY_PEOPLE9, 0);
            foodGiven.Add(Constants.PARTY_PEOPLE10, 0);

            brewsGiven.Add(Constants.COMPANION, 0);
            brewsGiven.Add(Constants.PARTY_PEOPLE1, 0);
            brewsGiven.Add(Constants.PARTY_PEOPLE2, 0);
            brewsGiven.Add(Constants.PARTY_PEOPLE3, 0);
            brewsGiven.Add(Constants.PARTY_PEOPLE4, 0);
            brewsGiven.Add(Constants.PARTY_PEOPLE5, 0);
            brewsGiven.Add(Constants.PARTY_PEOPLE6, 0);
            brewsGiven.Add(Constants.PARTY_PEOPLE7, 0);
            brewsGiven.Add(Constants.PARTY_PEOPLE8, 0);
            brewsGiven.Add(Constants.PARTY_PEOPLE9, 0);
            brewsGiven.Add(Constants.PARTY_PEOPLE10, 0);
        }

        public static void checkPartyQuest()
        {
            bool companionSober = (foodGiven[Constants.COMPANION] - brewsGiven[Constants.COMPANION] >= 1);
            bool partyer1Drunk = (brewsGiven[Constants.PARTY_PEOPLE1] - foodGiven[Constants.PARTY_PEOPLE1] >= 1);
            bool partyer2Drunk = (brewsGiven[Constants.PARTY_PEOPLE2] - foodGiven[Constants.PARTY_PEOPLE2] >= 1);
            bool partyer3Drunk = (brewsGiven[Constants.PARTY_PEOPLE3] - foodGiven[Constants.PARTY_PEOPLE3] >= 1);
            bool partyer4Drunk = (brewsGiven[Constants.PARTY_PEOPLE4] - foodGiven[Constants.PARTY_PEOPLE4] >= 1);
            bool partyer5Drunk = (brewsGiven[Constants.PARTY_PEOPLE5] - foodGiven[Constants.PARTY_PEOPLE5] >= 1);
            bool partyer6Drunk = (brewsGiven[Constants.PARTY_PEOPLE6] - foodGiven[Constants.PARTY_PEOPLE6] >= 1);
            bool partyer7Drunk = (brewsGiven[Constants.PARTY_PEOPLE7] - foodGiven[Constants.PARTY_PEOPLE7] >= 1);
            bool partyer8Drunk = (brewsGiven[Constants.PARTY_PEOPLE8] - foodGiven[Constants.PARTY_PEOPLE8] >= 1);
            bool partyer9Drunk = (brewsGiven[Constants.PARTY_PEOPLE9] - foodGiven[Constants.PARTY_PEOPLE9] >= 1);
            bool partyer10Drunk = (brewsGiven[Constants.PARTY_PEOPLE10] - foodGiven[Constants.PARTY_PEOPLE10] >= 1);

            int enoughDrunk = (partyer1Drunk ? 1 : 0) +
                              (partyer2Drunk ? 1 : 0) +
                              (partyer3Drunk ? 1 : 0) +
                              (partyer4Drunk ? 1 : 0) +
                              (partyer5Drunk ? 1 : 0) +
                              (partyer6Drunk ? 1 : 0) +
                              (partyer7Drunk ? 1 : 0) +
                              (partyer8Drunk ? 1 : 0) +
                              (partyer9Drunk ? 1 : 0) +
                              (partyer10Drunk ? 1 : 0);

            if (companionSober && enoughDrunk >= 5)
            {
                gameOver = true;
            }
        }
    }
}
