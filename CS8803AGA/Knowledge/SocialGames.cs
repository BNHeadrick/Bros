﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CS8803AGA.controllers;

namespace CS8803AGA.Knowledge
{
    class SocialGames
    {
        public static SocialGames singleton = new SocialGames();
        Dictionary<string, SGame> dictionary;

        protected SocialGames()
        {
            dictionary = new Dictionary<string, SGame>();
        }

        public void addSocialGame(string key, string aName)
        {
            SGame sg = new SGame(aName);
            dictionary.Add(key, sg);
        }


        public SGame getSocialGame(String key)
        {
            SGame theSG = null;
            // See whether Dictionary contains this string.
	        if (dictionary.ContainsKey(key))
	        {
	            theSG = dictionary[key];
	        }
            return theSG;
        }

    }

    public class SGame{
        /*
        CharacterController charCont;
        public CulturalKnowledge(CharacterController cc)
        {
            charCont = cc;
        }
        */
        SocialStatusRule ssR; 
        String name;
        float resultingAction;
        public SGame(string aName)
        {
            name = aName;
        }

        public SGame(string aName, SocialStatusRule aSSR)
        {
            name = aName;
            ssR = aSSR;
        }

        /*
        public bool runSGame(CharacterController cc, SocialNetwork sn, ){

        }
        */
    }

}
