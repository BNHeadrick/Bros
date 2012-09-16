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



    }
}
