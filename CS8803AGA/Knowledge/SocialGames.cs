using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CS8803AGA.controllers;
using CS8803AGA.dialog;

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

        public void addSocialGame(SGame game)
        {
            dictionary.Add(game.name, game);
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

        public static List<SGame> getAllGames(int p1, int p2)
        {
            List<SGame> games = new List<SGame>();
            foreach (KeyValuePair<string, SGame> pair in singleton.dictionary)
            {
                if (pair.Value.satisfied(p1, p2))
                {
                    games.Add(pair.Value);
                }
            }
            return games;
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
        private string dialog;

        public List<string> ssR;  
        public String name;
        
        // results
        public int p3;
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
            p3 = -1;
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
            p3 = -1;
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

        public SGame(string aName, List<string> aSSR, int _drelation, int _drelation_target, int _drelation_third,
                     int _drelation_third_target, List<string> _predicates_add, List<string> _predicates_remove,
                     List<string> _predicates_add_third, List<string> _predicates_remove_third, List<string> _personality_add,
                     List<string> _personality_remove, List<string> _personality_add_target, List<string> _personality_remove_target)
        {
            p3 = -1;
            drelation = _drelation;
            drelation_target = _drelation_target;
            drelation_third = _drelation_third;
            drelation_third_target = _drelation_third_target;
            predicates_add = _predicates_add;
            predicates_remove = _predicates_remove;
            predicates_add_third = _predicates_add_third;
            predicates_remove_third = _predicates_remove_third;
            personality_add = _personality_add;
            personality_remove = _personality_remove;
            personality_add_target = _personality_add_target;
            personality_remove_target = _personality_remove_target;

            name = aName;
            ssR = aSSR;
        }

        
        public void run(int p1, int p2, List<string> results) {
            int textSize = (new Random()).Next(15, 35);
            dialog = NLG.get(textSize);
            for (int i = 50; i < dialog.Length; i += 50)
            {
                while (dialog[i] != ' ')
                {
                    i--;
                    if (i < 0)
                    {
                        i += 50;
                        break;
                    }
                }
                dialog = dialog.Insert(i + 1, "\n");
            }

            SocialNetworks.singleton.getSocialNetwork("" + p1).getInnerNetwork("" + p2).relation += drelation;
            if (SocialNetworks.singleton.getSocialNetwork("" + p1).getInnerNetwork("" + p2).relation > InnerSocialNetwork.RELATION_MAX)
            {
                SocialNetworks.singleton.getSocialNetwork("" + p1).getInnerNetwork("" + p2).relation = InnerSocialNetwork.RELATION_MAX;
            }
            else if (SocialNetworks.singleton.getSocialNetwork("" + p1).getInnerNetwork("" + p2).relation < InnerSocialNetwork.RELATION_MIN)
            {
                SocialNetworks.singleton.getSocialNetwork("" + p1).getInnerNetwork("" + p2).relation = InnerSocialNetwork.RELATION_MIN;
            }
            SocialNetworks.singleton.getSocialNetwork("" + p2).getInnerNetwork("" + p1).relation += drelation_target;
            if (SocialNetworks.singleton.getSocialNetwork("" + p2).getInnerNetwork("" + p1).relation > InnerSocialNetwork.RELATION_MAX)
            {
                SocialNetworks.singleton.getSocialNetwork("" + p2).getInnerNetwork("" + p1).relation = InnerSocialNetwork.RELATION_MAX;
            }
            else if (SocialNetworks.singleton.getSocialNetwork("" + p2).getInnerNetwork("" + p1).relation < InnerSocialNetwork.RELATION_MIN)
            {
                SocialNetworks.singleton.getSocialNetwork("" + p2).getInnerNetwork("" + p1).relation = InnerSocialNetwork.RELATION_MIN;
            }

            // results
            if (drelation != 0)
            {
                results.Add("Relations: " + (drelation > 0 ? "+" : "") + drelation + ", now: "+SocialNetworks.singleton.getSocialNetwork("" + p1).getInnerNetwork("" + p2).relation+" #" + p2);
            }
            if (drelation_target != 0)
            {
                results.Add("Relations: " + (drelation_target > 0 ? "+" : "") + drelation_target + ", now: "+SocialNetworks.singleton.getSocialNetwork("" + p2).getInnerNetwork("" + p1).relation+" #" + p1);
            }

            for (int i = 0; i < predicates_add.Count; i++)
            {
                if (!SocialNetworks.singleton.getSocialNetwork("" + p1).getInnerNetwork("" + p2).predicates.Contains(predicates_add[i]))
                {
                    results.Add("+"+predicates_add[i] + " #0");
                    SocialNetworks.singleton.getSocialNetwork("" + p1).getInnerNetwork("" + p2).predicates.Add(predicates_add[i]);
                }
                if (!SocialNetworks.singleton.getSocialNetwork("" + p2).getInnerNetwork("" + p1).predicates.Contains(predicates_add[i]))
                {
                    SocialNetworks.singleton.getSocialNetwork("" + p2).getInnerNetwork("" + p1).predicates.Add(predicates_add[i]);
                }
            }
            for (int i = 0; i < predicates_remove.Count; i++)
            {
                results.Add("-" + predicates_remove[i] + " #0");
                SocialNetworks.singleton.getSocialNetwork("" + p1).getInnerNetwork("" + p2).predicates.Remove(predicates_remove[i]);
                SocialNetworks.singleton.getSocialNetwork("" + p2).getInnerNetwork("" + p1).predicates.Remove(predicates_remove[i]);
            }

            if (p3 != -1)
            {
                SocialNetworks.singleton.getSocialNetwork("" + p1).getInnerNetwork("" + p3).relation += drelation_third;
                if (SocialNetworks.singleton.getSocialNetwork("" + p1).getInnerNetwork("" + p3).relation > InnerSocialNetwork.RELATION_MAX)
                {
                    SocialNetworks.singleton.getSocialNetwork("" + p1).getInnerNetwork("" + p3).relation = InnerSocialNetwork.RELATION_MAX;
                }
                else if (SocialNetworks.singleton.getSocialNetwork("" + p1).getInnerNetwork("" + p3).relation < InnerSocialNetwork.RELATION_MIN)
                {
                    SocialNetworks.singleton.getSocialNetwork("" + p1).getInnerNetwork("" + p3).relation = InnerSocialNetwork.RELATION_MIN;
                }
                SocialNetworks.singleton.getSocialNetwork("" + p2).getInnerNetwork("" + p3).relation += drelation_third_target;
                if (SocialNetworks.singleton.getSocialNetwork("" + p2).getInnerNetwork("" + p3).relation > InnerSocialNetwork.RELATION_MAX)
                {
                    SocialNetworks.singleton.getSocialNetwork("" + p2).getInnerNetwork("" + p3).relation = InnerSocialNetwork.RELATION_MAX;
                }
                else if (SocialNetworks.singleton.getSocialNetwork("" + p2).getInnerNetwork("" + p3).relation < InnerSocialNetwork.RELATION_MIN)
                {
                    SocialNetworks.singleton.getSocialNetwork("" + p2).getInnerNetwork("" + p3).relation = InnerSocialNetwork.RELATION_MIN;
                }

                if (drelation_third != 0)
                {
                    results.Add("Relations: " + (drelation_third > 0 ? "+" : "") + drelation_third + ", now: " + SocialNetworks.singleton.getSocialNetwork("" + p1).getInnerNetwork("" + p3).relation + " #" + p1 + "|" + p3);
                }
                if (drelation_third_target != 0)
                {
                    results.Add("Relations: " + (drelation_third_target > 0 ? "+" : "") + drelation_third_target + ", now: " + SocialNetworks.singleton.getSocialNetwork("" + p2).getInnerNetwork("" + p3).relation + " #" + p2 + "|" + p3);
                }

                for (int i = 0; i < predicates_add_third.Count; i++)
                {
                    if (!SocialNetworks.singleton.getSocialNetwork("" + p1).getInnerNetwork("" + p3).predicates.Contains(predicates_add_third[i]))
                    {
                        results.Add("+" + predicates_add_third[i] + " #" + p1 + "|" + p3);
                        SocialNetworks.singleton.getSocialNetwork("" + p1).getInnerNetwork("" + p3).predicates.Add(predicates_add_third[i]);
                    }
                    if (!SocialNetworks.singleton.getSocialNetwork("" + p2).getInnerNetwork("" + p3).predicates.Contains(predicates_add_third[i]))
                    {
                        results.Add("+" + predicates_add_third[i] + " #" + p2+"|"+p3);
                        SocialNetworks.singleton.getSocialNetwork("" + p2).getInnerNetwork("" + p3).predicates.Add(predicates_add_third[i]);
                    }
                }
                for (int i = 0; i < predicates_remove_third.Count; i++)
                {
                    results.Add("-" + predicates_remove_third[i] + " w/ me #" + p3);
                    results.Add("-" + predicates_remove_third[i] + " w/ you #" + p3);
                    SocialNetworks.singleton.getSocialNetwork("" + p1).getInnerNetwork("" + p3).predicates.Remove(predicates_remove_third[i]);
                    SocialNetworks.singleton.getSocialNetwork("" + p2).getInnerNetwork("" + p3).predicates.Remove(predicates_remove_third[i]);
                }
            }

            for (int i = 0; i < personality_add.Count; i++)
            {
                if (!PersonalityDescriptions.singleton.getPersDesc(""+p1).personality.Contains(personality_add[i]))
                {
                    results.Add("+" + personality_add[i] + " #" + p1);
                    PersonalityDescriptions.singleton.getPersDesc("" + p1).personality.Add(personality_add[i]);
                }
            }
            for (int i = 0; i < personality_add_target.Count; i++)
            {
                if (!PersonalityDescriptions.singleton.getPersDesc(""+p2).personality.Contains(personality_add_target[i]))
                {
                    results.Add("+" + personality_add_target[i] + " #" + p2);
                    PersonalityDescriptions.singleton.getPersDesc(""+p2).personality.Add(personality_add_target[i]);
                }
            }
            for (int i = 0; i < personality_remove.Count; i++)
            {
                results.Add("-" + personality_remove[i] + " #" + p1);
                PersonalityDescriptions.singleton.getPersDesc(""+p1).personality.Remove(personality_remove[i]);
            }
            for (int i = 0; i < personality_remove_target.Count; i++)
            {
                results.Add("-" + personality_remove_target[i] + " #" + p2);
                PersonalityDescriptions.singleton.getPersDesc(""+p2).personality.Remove(personality_remove_target[i]);
            }
        }

        /**
         * Wether or not two players can play this game
         *@param p1
         *@param p2
         *@return true or false
         */
        public bool satisfied(int p1, int p2) {
            if (p1 == p3 || p2 == p3)
            {
                return false;
            }
            for (int i = 0; i < ssR.Count; i++)
            {
                if (!SocialStatusRules.singleton.getSocialStatusRule(ssR[i]).satisfied(p1, p2))
                {
                    return false;
                }
            }
            return true;
        }

        public string text()
        {
            return dialog;
        }

    }

}
