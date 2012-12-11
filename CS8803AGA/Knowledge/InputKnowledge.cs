using System;
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
                                        Constants.PLAYER,
                                        Constants.COMPANION,
                                        Constants.COOK,
                                        Constants.BREW_MAIDEN
            };

            int[] relations = new int[] { 0, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50,
                                          50, 0, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50,
                                          50, 50, 0, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50,
                                          50, 50, 50, 0, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50,
                                          50, 50, 50, 50, 0, 50, 50, 50, 50, 50, 50, 50, 50, 50,
                                          50, 50, 50, 50, 50, 0, 50, 50, 50, 50, 50, 50, 50, 50,
                                          50, 50, 50, 50, 50, 50, 0, 50, 50, 50, 50, 50, 50, 50,
                                          50, 50, 50, 50, 50, 50, 50, 0, 50, 50, 50, 50, 50, 50,
                                          50, 50, 50, 50, 50, 50, 50, 50, 0, 50, 50, 50, 50, 50,
                                          50, 50, 50, 50, 50, 50, 50, 50, 50, 0, 50, 50, 50, 50,
                                          50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 0, 50, 50, 50,
                                          50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 0, 50, 50,
                                          50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 0, 50,
                                          50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 0
            };

            string[] cult_knowledge = new string[] {"bears",
                                                    "fire bears",
                                                    "pogo sticks",
                                                    "tacos",
                                                    "brews",
                                                    "brewtopia",
                                                    "rad shoes",
                                                    "bed sheets"

            };

            int[] cult_relations = new int[] {
                   /* bears => */   0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                   /* fire bears */ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                   /* pogo sticks*/ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                   /* tacos => */   0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                   /* brews */      0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                   /* b-topia  */   0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                   /* rad shoes */  0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                   /* bed sheets */ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
            };

            for (int i = 0; i < partiers.Length; i++)
            {
                SocialNetworks.singleton.addSocNet("" + partiers[i]);
                CulturalKnowledgebase.singleton.addCultKnowledge("" + partiers[i]);
                PersonalityDescriptions.singleton.addPersDesc("" + partiers[i], partiers[i]);
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
            SocialNetworks.singleton.addPredicate(Constants.PARTY_PEOPLE1, Constants.PARTY_PEOPLE2, "love");
            SocialNetworks.singleton.addPredicate(Constants.PARTY_PEOPLE1, Constants.PARTY_PEOPLE2, "love");

            // add any purse desks
            PersonalityDescriptions.singleton.addPersDesc("sex-lexia", Constants.PARTY_PEOPLE1);

            // add social status rules
            SocialStatusRules.singleton.addSocStatRule("cool rule", new SocialStatusRule(SocialStatusRule.TYPE_RELATION, "", 49, 51));


            // add social games
            SGame s = new SGame("Praise Brewtopia!");
            s.ssR.Add("cool rule");
            s.drelation = -2;
            SocialGames.singleton.addSocialGame(s);
        }

    }
}
