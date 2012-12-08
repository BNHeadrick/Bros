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
                                        Constants.COMPANION
            };

            int[] relations = new int[] { 0, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50,
                                          50, 0, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50,
                                          50, 50, 0, 50, 50, 50, 50, 50, 50, 50, 50, 50,
                                          50, 50, 50, 0, 50, 50, 50, 50, 50, 50, 50, 50,
                                          50, 50, 50, 50, 0, 50, 50, 50, 50, 50, 50, 50,
                                          50, 50, 50, 50, 50, 0, 50, 50, 50, 50, 50, 50,
                                          50, 50, 50, 50, 50, 50, 0, 50, 50, 50, 50, 50,
                                          50, 50, 50, 50, 50, 50, 50, 0, 50, 50, 50, 50,
                                          50, 50, 50, 50, 50, 50, 50, 50, 0, 50, 50, 50,
                                          50, 50, 50, 50, 50, 50, 50, 50, 50, 0, 50, 50,
                                          50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 0, 50,
                                          50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 0
            };

            string[] cult_knowledge = new string[] {"bears",
                                                    "fire bears"
            };

            int[] cult_relations = new int[] {
                   /* bears => */   0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                   /* fire beras */ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
            };

            for (int i = 0; i < partiers.Length; i++)
            {
                SocialNetworks.singleton.addSocNet("" + partiers[i]);
                CulturalKnowledgebase.singleton.addCultKnowledge("" + partiers[i]);
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
        }

    }
}
