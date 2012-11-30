using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CS8803AGA.Knowledge
{
    class SocialFact
    {
        private string title;
        
        private float affection;    //this is the social status; a float between 0.0 and 1.0 showing the amount that a char likes another char.
        private int initiatorID;    //should later be a pointer to an NPC
        private int targetID;       //should later be a pointer to an NPC
        private int thirdPartyID;   //should later be a pointer to an NPC

        private string[] topics;
        private SocialGame[] choices;

    }
}
