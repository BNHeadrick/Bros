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

        public SocialStatusRules(String key, String aName)
        {
            dictionary = new Dictionary<string, SocialStatusRule>();
        }

        public void addSocStatRule(String key, String aName)
        {
            SocialStatusRule sr = new SocialStatusRule(aName);
            dictionary.Add(key, sr);
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
        public bool canGameStart(String rule, SGame sg)
        {
            Boolean canStart = false;
            if (dictionary.ContainsKey(rule))
            {
                
            }
            return canStart;
        }

    }

    public class SocialStatusRule{
        String name;
        public SocialStatusRule(String aName)
        {
            name = aName;
        }
    }
}
