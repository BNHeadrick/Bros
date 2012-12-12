﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CS8803AGA.Knowledge
{
    class InputKnowledge
    {

        public void TestInput()
        {
            int[] partiers = new int[]{ Constants.PARTY_PEOPLE1,
                                        Constants.PARTY_PEOPLE2,
                                        Constants.PARTY_PEOPLE3,
                                        Constants.PARTY_PEOPLE4,
                                        Constants.PARTY_PEOPLE5,
                                        Constants.PARTY_PEOPLE6,
                                        Constants.PARTY_PEOPLE7,
                                        Constants.PARTY_PEOPLE8,
                                        Constants.PARTY_PEOPLE9,
                                        Constants.PARTY_PEOPLE10,
                                        Constants.COOK,
                                        Constants.BREW_MAIDEN,
                                        Constants.PLAYER,
                                        Constants.COMPANION
            };

            int[] relations = new int[] { 
            /*PARTY_PEOPLE1  */            0, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 20,
            /*PARTY_PEOPLE2  */            50, 0, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 10,
            /*PARTY_PEOPLE3  */            50, 50, 0, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 30,
            /*PARTY_PEOPLE4  */            50, 50, 50, 0, 50, 50, 50, 50, 50, 50, 50, 50, 50, 40,
            /*PARTY_PEOPLE5  */            50, 50, 50, 50, 0, 50, 50, 50, 50, 50, 50, 50, 50, 20,
            /*PARTY_PEOPLE6  */            50, 50, 50, 50, 50, 0, 50, 50, 50, 50, 50, 50, 50, 20,
            /*PARTY_PEOPLE7  */            50, 50, 50, 50, 50, 50, 0, 50, 50, 50, 50, 50, 50, 30,
            /*PARTY_PEOPLE8  */            50, 50, 50, 50, 50, 50, 50, 0, 50, 50, 50, 50, 50, 20,
            /*PARTY_PEOPLE9  */            50, 50, 50, 50, 50, 50, 50, 50, 0, 50, 50, 50, 50, 10,
            /*PARTY_PEOPLE10 */            50, 50, 50, 50, 50, 50, 50, 50, 50, 0, 50, 50, 50, 50,
            /*COOK           */            50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 0, 50, 50, 80,
            /*BREW_MAIDEN    */            50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 0, 50, 90,
            /*PLAYER         */            50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 0, 50,
            /*COMPANION      */            50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 0


            };

            string[] cult_knowledge = new string[] {"bears",
                                                    "fire bears",
                                                    "pogo sticks",
                                                    "tacos",
                                                    "brews",
                                                    "brewtopia",
                                                    "rad shoes",
                                                    "bed sheets",
                                                    "hate companion"

            };

            
            Random random = new Random();
            int[] cult_relations = new int[partiers.Length * cult_knowledge.Length];
            for (int k = 0; k < partiers.Length * cult_knowledge.Length; k++)
            {
                
                int randomNumber = random.Next(0, 100);
                cult_relations[k] = randomNumber;
            }
            
            
            for (int i = 0; i < partiers.Length; i++)
            {
                SocialNetworks.singleton.addSocNet("" + partiers[i]);
                CulturalKnowledgebase.singleton.addCultKnowledge("" + partiers[i]);
                PersonalityDescriptions.singleton.addPersDesc("" + partiers[i]);
                for (int j = 0; j < partiers.Length; j++)
                {
                    if (j != i)
                    {
                        SocialNetworks.singleton.getSocialNetwork("" + partiers[i]).addInnerNetwork("" + partiers[j]);
                        SocialNetworks.singleton.getSocialNetwork("" + partiers[i]).getInnerNetwork("" + partiers[j]).relation = relations[j+i*partiers.Length];
                    }
                }
                for (int j = 0; j < cult_knowledge.Length; j++)
                {
                    CulturalKnowledgebase.singleton.getCulturalKnowledge("" + partiers[i]).addInnerNetwork("" + cult_knowledge[j]);
                    CulturalKnowledgebase.singleton.getCulturalKnowledge("" + partiers[i]).getInnerNetwork("" + cult_knowledge[j]).relation = cult_relations[j * partiers.Length + i];
                }
            }

            // add any social network predicates
            addSocNetPredicates();

            // add any purse desks
            addPersDesc();

            // add social status rules
            addSSRules();

            // add social games
            addSocialGames();
        }

        private void addSocNetPredicates()
        {
            SocialNetworks.singleton.addPredicate(Constants.PARTY_PEOPLE1, Constants.PARTY_PEOPLE2, "love");
            
            SocialNetworks.singleton.addPredicate(Constants.PARTY_PEOPLE3, Constants.PARTY_PEOPLE4, "hate");
            
            SocialNetworks.singleton.addPredicate(Constants.PLAYER, Constants.COMPANION, "bros");
            
        }

        private void addPersDesc()
        {
            PersonalityDescriptions.singleton.addPersDesc("sex-lexia", Constants.PARTY_PEOPLE1);
            PersonalityDescriptions.singleton.addPersDesc("sex-magnet", Constants.PARTY_PEOPLE2);
            PersonalityDescriptions.singleton.addPersDesc("fire bear lover", Constants.PARTY_PEOPLE3);
            PersonalityDescriptions.singleton.addPersDesc("fire bear slayer", Constants.PARTY_PEOPLE4);
            
            
            PersonalityDescriptions.singleton.addPersDesc("sex-lexia", Constants.PARTY_PEOPLE5);
            
            PersonalityDescriptions.singleton.addPersDesc("sex-magnet", Constants.PARTY_PEOPLE6);
            
            PersonalityDescriptions.singleton.addPersDesc("fire bear lover", Constants.PARTY_PEOPLE7);
            PersonalityDescriptions.singleton.addPersDesc("fire bear slayer", Constants.PARTY_PEOPLE8);
            
            PersonalityDescriptions.singleton.addPersDesc("LARP master", Constants.PARTY_PEOPLE9);
            PersonalityDescriptions.singleton.addPersDesc("LARP hater", Constants.PARTY_PEOPLE10);
            PersonalityDescriptions.singleton.addPersDesc("LARP master", Constants.COOK);
            PersonalityDescriptions.singleton.addPersDesc("LARP hater", Constants.BREW_MAIDEN);

            PersonalityDescriptions.singleton.addPersDesc("brew-coholic", Constants.COMPANION);
            
            PersonalityDescriptions.singleton.addPersDesc("dislikes bronies", Constants.COMPANION);
            
        }

        private void addSSRules()
        {
            SocialStatusRules.singleton.addSocStatRule("hates companion", new SocialStatusRule(SocialStatusRule.TYPE_CULTURAL, "hate companion", 50, 100));
            //below; would this fire from people who also hate the companion?  How can I make it so only people who don't hate the companion have a chance to say this?
            //SocialStatusRules.singleton.addSocStatRule("loves companion", new SocialStatusRule(SocialStatusRule.TYPE_RELATION, "love companion", 60, 100));
            
            SocialStatusRules.singleton.addSocStatRule("loves brewtopia", new SocialStatusRule(SocialStatusRule.TYPE_RELATION, "brewtopia", 5, 100));
            /*
            SocialStatusRules.singleton.addSocStatRule("loves brewtopia", new SocialStatusRule(SocialStatusRule.TYPE_RELATION, "brewtopia", 0, 100));
            */
            SocialStatusRules.singleton.addSocStatRule("hates companion", new SocialStatusRule(SocialStatusRule.TYPE_RELATION, "pick on", 0, 25));
            SocialStatusRules.singleton.addSocStatRule("hates companion", new SocialStatusRule(SocialStatusRule.TYPE_RELATION, "draw phallus on face", 0, 10));
            SocialStatusRules.singleton.addSocStatRule("hates companion", new SocialStatusRule(SocialStatusRule.TYPE_RELATION, "sweep the leg", 0, 1));
            
        }

        private void addSocialGames()
        {
            SGame a = new SGame("Praise Brewtopia!");
            a.ssR.Add("loves brewtopia");
            a.drelation = +1;
            SocialGames.singleton.addSocialGame(a);

            SGame s = new SGame("Talk Smack About #" + Constants.COMPANION);
            s.ssR.Add("hates companion");
            s.p3 = Constants.COMPANION;
            s.drelation_third_target = -5;
            SocialGames.singleton.addSocialGame(s);

            SGame s = new SGame("Talk Smack About #" + Constants.COMPANION);
            s.ssR.Add("hates companion");
            s.p3 = Constants.COMPANION;
            s.drelation_third_target = -5;
            SocialGames.singleton.addSocialGame(s);
            
        }

    }
}
