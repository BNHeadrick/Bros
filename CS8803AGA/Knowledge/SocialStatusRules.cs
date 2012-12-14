/*Social status rules are the preconditions that must first be met 
before a game initiating a social status change can be played.
They are essentially a listing of horn clauses, where the head is 
the social status change in question, and the conditions of the
body are checked against the social facts database and the social 
networks
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CS8803AGA.Knowledge
{
    public class SocialStatusRules
    {
        Dictionary<string, SocialStatusRule> dictionary;
        public static SocialStatusRules singleton = new SocialStatusRules();

        protected SocialStatusRules()
        {
            dictionary = new Dictionary<string, SocialStatusRule>();
        }

        public void addSocStatRule(String key, SocialStatusRule rule)
        {
            dictionary.Add(key, rule);
        }

        public SocialStatusRule getSocialStatusRule(String key)
        {
            SocialStatusRule theSR = null;
            // See whether Dictionary contains this string.
	        if (dictionary.ContainsKey(key))
	        {
                theSR = dictionary[key];
	        }
            return theSR;
        }

        //if(socialstatuschangeisheadistrue & thebody compared to the social facts and social networks)
        //TODO; finish this!
        /*
        public bool canGameStart(String rule, SGame sg)
        {
            Boolean canStart = false;
            if (dictionary.ContainsKey(rule))
            {
                
            }
            return canStart;
        }
        */

    }

    public class SocialStatusRule{
        public const int TYPE_RELATION = 0;
        public const int TYPE_PREDICATE = 1;
        public const int TYPE_CULTURAL = 2;
        public const int TYPE_CULTURAL_TARGET = 3;
        public const int TYPE_PAST_GAME = 4;
        public const int TYPE_PERSONALITY = 5;
        public const int TYPE_PERSONALITY_TARGET = 6;
        public const int TYPE_END_GAME = 7;
        public const int TYPE_RELATION_TARGET = 8;

        private const int GOAL_HATERS = 3; 

        public int type;
        public string data; /**< either the game we want, the culture to check, or the predicate to check */
        public int min; /**< the minimum accepted value */
        public int max; /**< the max accepted value */

        public SocialStatusRule(string aName): this(TYPE_RELATION, "", 0, 0)
        {
        }
        public SocialStatusRule(int _type, string _data, int _min, int _max)
        {
            type = _type;
            data = _data;
            min = _min;
            max = _max;
        }
        
        /**
         * Checks whether or not this rule is satisfied
         *@param p1 player 1
         *@param p2 player 2
         *@retrun true if it is satisfied
         */
        public bool satisfied(int p1, int p2)
        {
            switch (type)
            {
                case TYPE_RELATION: // check the relation
                    {
                        // get the social network between two character
                        int relation = SocialNetworks.singleton.getSocialNetwork("" + p1).getInnerNetwork("" + p2).relation;
                        return (relation >= min && relation <= max);
                    }
                case TYPE_PREDICATE:
                    return SocialNetworks.singleton.getSocialNetwork("" + p1).getInnerNetwork("" + p2).predicates.Contains(data);
                case TYPE_CULTURAL:
                    {
                        int relation = CulturalKnowledgebase.singleton.getCulturalKnowledge("" + p1).getInnerNetwork(data).relation;
                        return (relation >= min && relation <= max);
                    }
                case TYPE_CULTURAL_TARGET:
                    {
                        int relation = CulturalKnowledgebase.singleton.getCulturalKnowledge("" + p2).getInnerNetwork(data).relation;
                        return (relation >= min && relation <= max);
                    }
                case TYPE_PAST_GAME:
                    foreach (KeyValuePair<string, SocialFact> fact in SocialFacts.singleton.dictionary) {
                        if (fact.Value.equals(data, p1, p2))
                        {
                            return true;
                        }
                    }
                    return false;
                case TYPE_PERSONALITY:
                    return PersonalityDescriptions.singleton.getPersDesc("" + p1).personality.Contains(data);
                case TYPE_PERSONALITY_TARGET:
                    return PersonalityDescriptions.singleton.getPersDesc("" + p2).personality.Contains(data);
                case TYPE_END_GAME:
                    if (p1 == Constants.COMPANION && p2 == Constants.PLAYER)
                    {
                        int haters = 0;
                        foreach (KeyValuePair<string, SocialNetwork> entry in SocialNetworks.singleton.dictionary)
                        {
                            if (    entry.Key != ""+p1 && entry.Value.getInnerNetwork(""+p1).relation < 30) {
                                haters++;
                            }
                        }
                        return(haters>=GOAL_HATERS);
                    }
                    return false;
                case TYPE_RELATION_TARGET: // check the relation
                    {
                        // get the social network between two character
                        int relation = SocialNetworks.singleton.getSocialNetwork("" + p2).getInnerNetwork("" + p1).relation;
                        return (relation >= min && relation <= max);
                    }
            }
            return false;
        }
    }
}
