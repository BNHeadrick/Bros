/*
chronological listing of the back story and events that occurred 
during game play, a detailed sense of time, what social game the social fact took place 
in, what characters were playing the game and what changes in 
the social network occurred.
 * 
 * It is important to note that the social facts database is also used to 
store the initial state of the game world
 * 
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

        public static SocialFacts singleton = new SocialFacts();

        Dictionary<string, SocialFact> dictionary;
        

        protected SocialFacts()
        {
            dictionary = new Dictionary<string, SocialFact>();
        }

        public void addSocialFact(String key, String aName)
        {
            SocialFact sf = new SocialFact(aName);
            dictionary.Add(key, sf);
        }

        public void addSocialFact(String key, String aName, float aAffection, int init, int targ, int tParty)
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
        private int initiator;   
        private int target;       
        private int thirdParty;   

        public SocialFact(String aName, float aAffection, int init, int targ, int tParty)
        {
            name = aName;
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
