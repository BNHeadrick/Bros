using System;
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
        List<string> ssR;  
        String name;
        
        // results
        public int drelation;
        public int drelation_target;
        public int drelation_third;
        public int drelation_third_target;
        public List<string> predicates_add;
        public List<string> predicates_remove;
        public List<string> predicates_add_third;
        public List<string> predicates_remove_third;
        public List<string> personality_add;
        public List<string> personality_remove;
        public List<string> personality_add_target;
        public List<string> personality_remove_target;

        public SGame(string aName)
        {
            drelation = 0;
            drelation_target = 0;
            drelation_third = 0;
            drelation_third_target = 0;
            predicates_add = new List<string>();
            predicates_remove = new List<string>();
            predicates_add_third = new List<string>();
            predicates_remove_third = new List<string>();
            personality_add = new List<string>();
            personality_remove = new List<string>();
            personality_add_target = new List<string>();
            personality_remove_target = new List<string>();

            name = aName;
            ssR = new List<string>();
        }

        public SGame(string aName, List<string> aSSR)
        {
            drelation = 0;
            drelation_target = 0;
            drelation_third = 0;
            drelation_third_target = 0;
            predicates_add = new List<string>();
            predicates_remove = new List<string>();
            predicates_add_third = new List<string>();
            predicates_remove_third = new List<string>();
            personality_add = new List<string>();
            personality_remove = new List<string>();
            personality_add_target = new List<string>();
            personality_remove_target = new List<string>();

            name = aName;
            ssR = aSSR;
        }

        
        public void run(int p1, int p2, int p3) {
            SocialNetworks.singleton.getSocialNetwork("" + p1).getInnerNetwork("" + p2).relation += drelation;
            SocialNetworks.singleton.getSocialNetwork("" + p2).getInnerNetwork("" + p1).relation += drelation_target;
            for (int i = 0; i < predicates_add.Count; i++)
            {
                if (!SocialNetworks.singleton.getSocialNetwork("" + p1).getInnerNetwork("" + p2).predicates.Contains(predicates_add[i]))
                {
                    SocialNetworks.singleton.getSocialNetwork("" + p1).getInnerNetwork("" + p2).predicates.Add(predicates_add[i]);
                }
                if (!SocialNetworks.singleton.getSocialNetwork("" + p2).getInnerNetwork("" + p1).predicates.Contains(predicates_add[i]))
                {
                    SocialNetworks.singleton.getSocialNetwork("" + p2).getInnerNetwork("" + p1).predicates.Add(predicates_add[i]);
                }
            }
            for (int i = 0; i < predicates_remove.Count; i++)
            {
                SocialNetworks.singleton.getSocialNetwork("" + p1).getInnerNetwork("" + p2).predicates.Remove(predicates_remove[i]);
                SocialNetworks.singleton.getSocialNetwork("" + p2).getInnerNetwork("" + p1).predicates.Remove(predicates_remove[i]);
            }

            if (p3 != -1)
            {
                SocialNetworks.singleton.getSocialNetwork("" + p1).getInnerNetwork("" + p3).relation += drelation_third;
                SocialNetworks.singleton.getSocialNetwork("" + p2).getInnerNetwork("" + p3).relation += drelation_third_target;
                for (int i = 0; i < predicates_add_third.Count; i++)
                {
                    if (!SocialNetworks.singleton.getSocialNetwork("" + p1).getInnerNetwork("" + p3).predicates.Contains(predicates_add_third[i]))
                    {
                        SocialNetworks.singleton.getSocialNetwork("" + p1).getInnerNetwork("" + p3).predicates.Add(predicates_add_third[i]);
                    }
                    if (!SocialNetworks.singleton.getSocialNetwork("" + p2).getInnerNetwork("" + p3).predicates.Contains(predicates_add_third[i]))
                    {
                        SocialNetworks.singleton.getSocialNetwork("" + p2).getInnerNetwork("" + p3).predicates.Add(predicates_add_third[i]);
                    }
                }
                for (int i = 0; i < predicates_remove_third.Count; i++)
                {
                    SocialNetworks.singleton.getSocialNetwork("" + p1).getInnerNetwork("" + p3).predicates.Remove(predicates_remove_third[i]);
                    SocialNetworks.singleton.getSocialNetwork("" + p2).getInnerNetwork("" + p3).predicates.Remove(predicates_remove_third[i]);
                }
            }

            for (int i = 0; i < personality_add.Count; i++)
            {
                if (!PersonalityDescriptions.singleton.getPersDesc(""+p1).personality.Contains(personality_add[i]))
                {
                    PersonalityDescriptions.singleton.getPersDesc("" + p1).personality.Add(personality_add[i]);
                }
            }
            for (int i = 0; i < personality_add_target.Count; i++)
            {
                if (!PersonalityDescriptions.singleton.getPersDesc(""+p2).personality.Contains(personality_add_target[i]))
                {
                    PersonalityDescriptions.singleton.getPersDesc(""+p2).personality.Add(personality_add_target[i]);
                }
            }
            for (int i = 0; i < personality_remove.Count; i++)
            {
                PersonalityDescriptions.singleton.getPersDesc(""+p1).personality.Remove(personality_remove[i]);
            }
            for (int i = 0; i < personality_remove_target.Count; i++)
            {
                PersonalityDescriptions.singleton.getPersDesc(""+p2).personality.Remove(personality_remove_target[i]);
            }
        }

    }

}
