/*
chronological listing of the back story and events that occurred 
during game play, a detailed sense of time, what social game the social fact took place 
in, what characters were playing the game and what changes in 
the social network occurred.
 * 
 * A class that contains individual social facts.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CS8803AGA.controllers;

namespace CS8803AGA.Knowledge
{


    public class SocialFacts
    {
        Dictionary<string, SocialFact> dictionary;
        

        public SocialFacts()
        {
            dictionary = new Dictionary<string, SocialFact>();
        }

        public void addSocialFact(String key, String aName)
        {
            SocialFact sf = new SocialFact(aName);
            dictionary.Add(key, sf);
        }

        public void addSocialFact(String key, String aName, float aAffection, CharacterController init, CharacterController targ, CharacterController tParty)
        {
            SocialFact sf = new SocialFact(aName, aAffection, init, targ, tParty);
            dictionary.Add(key, sf);
        }

        public SocialFact getSocialFact(String key)
        {
            SocialFact theSF = null;
            // See whether Dictionary contains this string.
            if (dictionary.ContainsKey(key))
            {
                theSF = dictionary[key];
            }
            return theSF;
        }
    }

    public class SocialFact
    {

        private string name;

        private float affection;    //this is the social status; a float between 0.0 and 1.0 showing the amount that a char likes another char.
        private CharacterController initiator;    //should later be a pointer to an NPC
        private CharacterController target;       //should later be a pointer to an NPC
        private CharacterController thirdParty;   //should later be a pointer to an NPC

        public SocialFact(String aName, float aAffection, CharacterController init, CharacterController targ, CharacterController tParty)
        {
            name = aName;
            affection = aAffection;
            initiator = init;
            target = targ;
            thirdParty = tParty;
        }

        public SocialFact(String aName)
        {
            name = aName;
        }

    }

}
