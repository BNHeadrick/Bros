/*
Social Networks are a metric theCiF 2system uses to measure 
the relationship between any two characters—every character in 
every net has a link to every other character, and no character has 
a link to themselves. 
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CS8803AGA.controllers;

namespace CS8803AGA.Knowledge
{
    class SocialNetworks
    {
        //this needs to have access to all NPCs and have a one to many relationship with all non-self NPCs
        //this could be done in a simple 2D array if nothing more sophisticated seems nessisary.

        Dictionary<string, SocialNetwork> dictionary;
        public static SocialNetworks singleton = new SocialNetworks();

        protected SocialNetworks()
        {
            dictionary = new Dictionary<string, SocialNetwork>();
        }

        public void addSocNet(String key)
        {
            SocialNetwork sn = new SocialNetwork();
            dictionary.Add(key, sn);

        }


        public SocialNetwork getSocialNetwork(String key)
        {
            SocialNetwork theSN = null;
            // See whether Dictionary contains this string.
            if (dictionary.ContainsKey(key))
            {
                theSN = dictionary[key];
            }
            return theSN;
        }
        
        public void addPredicates(int personA, int personB, String predicateForA, String predicateForB)
        {
            String firstPerson = personA.ToString();
            String secPerson = personB .ToString();

            dictionary[firstPerson].getInnerNetwork(secPerson).addPredicate(predicateForA);
            dictionary[secPerson].getInnerNetwork(firstPerson).addPredicate(predicateForB);

        }
        

        public void addPredicate(int personA, int personB, String predicateForBoth)
        {
            String firstPerson = personA.ToString();
            String secPerson = personB.ToString();

            dictionary[firstPerson].getInnerNetwork(secPerson).addPredicate(predicateForBoth);
            dictionary[secPerson].getInnerNetwork(firstPerson).addPredicate(predicateForBoth);
        }

    }

    public class SocialNetwork
    {
        Dictionary<string, InnerSocialNetwork> innerSocNet;

        public SocialNetwork()
        {
            innerSocNet = new Dictionary<string, InnerSocialNetwork>();
        }

        public void addInnerNetwork(String key)
        {
            InnerSocialNetwork isn = new InnerSocialNetwork();
            innerSocNet.Add(key, isn);
        }

        public InnerSocialNetwork getInnerNetwork(string key)
        {
            return innerSocNet[key];
        }
        /*
        public void addPredicates(int personA, int personB, String predicateForA, String predicateForB)
        {
            String firstPerson = personA.ToString;
            String secPerson = personA.ToString;

            innerSocNet[firstPerson].addPredicate(predicateForA);
            innerSocNet[secPerson].addPredicate(predicateForB);

        }

        public void addPredicate(int personA, int personB, String predicateForBoth)
        {
            String firstPerson = personA.ToString;
            String secPerson = personA.ToString;

            innerSocNet[firstPerson].addPredicate(predicateForBoth);
            innerSocNet[secPerson].addPredicate(predicateForBoth);

        }
        */
    }
    /*
     *This class has is the "many" part of each social classes' one to many relationship.
     */
    public class InnerSocialNetwork
    {
        public const int RELATION_MIN = 0;
        public const int RELATION_MAX = 100;

        public int relation;
        public List<string> predicates;
        public InnerSocialNetwork()
        {
            relation = 0;
            predicates = new List<string>();
        }

        public void addPredicate(String pred){
            predicates.Add(pred);
        }
    }
}
