﻿/*
 * personality descriptions are a 
composite of character-specific entries in the social facts database 
and cultural knowledgebase, their links in the social networks, 
character traits, basic needs profile, and goals in the character‟s 
prospective memory
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CS8803AGA.controllers;

namespace CS8803AGA.Knowledge
{
    class PersonalityDescriptions
    {

        
        //get access to all NPCs
        Dictionary<string, PersonalityDescription> dictionary;
        public static PersonalityDescriptions singleton = new PersonalityDescriptions();

        protected PersonalityDescriptions()
        {
            dictionary = new Dictionary<string, PersonalityDescription>();
        }

        public void addPersDesc(string key)
        {
            if (!dictionary.ContainsKey(key))
            {
                dictionary.Add(key, new PersonalityDescription());
            }
        }

        public void addPersDesc(string key, int charVal){
            if (!dictionary.ContainsKey("" + charVal))
            {
                dictionary.Add("" + charVal, new PersonalityDescription());
            }
            dictionary["" + charVal].personality.Add(key);
        }

        public PersonalityDescription getPersDesc(String key)
        {
            PersonalityDescription thePD = null;
            // See whether Dictionary contains this string.
	        if (dictionary.ContainsKey(key))
	        {
	            thePD = dictionary[key];
	        }
            return thePD;
        }

    }

    

    public class PersonalityDescription
    {
        public List<string> personality;
        public PersonalityDescription()
        {
            personality = new List<string>();
        }
    }
}
