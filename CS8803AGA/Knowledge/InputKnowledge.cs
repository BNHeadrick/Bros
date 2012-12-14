using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CS8803AGA.dialog;

namespace CS8803AGA.Knowledge
{
    class InputKnowledge
    {

        string[] cult_knowledge;
        Random random;

        public void TestInput()
        {
            NLG.init();

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

            cult_knowledge = new string[] {         "brewtopia",
                                                    "bears",
                                                    "fire bears",
                                                    "pogo sticks",
                                                    "tacos",
                                                    "brews",
                                                    "rad shoes",
                                                    "bed sheets",

            };



            
            random = new Random();
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
            
            SocialStatusRules.singleton.addSocStatRule("loves brewtopia", new SocialStatusRule(SocialStatusRule.TYPE_RELATION, "brewtopia", 5, 100));


            for (int i = 1; i < cult_knowledge.Length; i++)
            {
                /*
                int rand1 = random.Next(0, 99);
                int rand2 = random.Next(0, 100);
                while (rand2 <= rand1)
                {
                    rand2 = random.Next(0, 100);
                }
                */
                SocialStatusRules.singleton.addSocStatRule("p1 likes "+cult_knowledge[i], new SocialStatusRule(SocialStatusRule.TYPE_CULTURAL, cult_knowledge[i], 60, 100));
                SocialStatusRules.singleton.addSocStatRule("p2 dislikes "+cult_knowledge[i], new SocialStatusRule(SocialStatusRule.TYPE_CULTURAL_TARGET, cult_knowledge[i], 0, 40));

                SocialStatusRules.singleton.addSocStatRule("p1 dislikes " + cult_knowledge[i], new SocialStatusRule(SocialStatusRule.TYPE_CULTURAL, cult_knowledge[i], 0, 40));
                SocialStatusRules.singleton.addSocStatRule("p2 likes " + cult_knowledge[i], new SocialStatusRule(SocialStatusRule.TYPE_CULTURAL_TARGET, cult_knowledge[i], 60, 100));
            }

        }

        private void addSocialGames()
        {
            SGame a = new SGame("Praise Brewtopia!");
            a.drelation_target = 1;
            SocialGames.singleton.addSocialGame(a);
            Random sRand = new Random();
            for (int i = 1; i < cult_knowledge.Length; i++)
            {
                

                SGame aGame = new SGame("Admire " + cult_knowledge[i] + "!");
                aGame.ssR.Add("p1 likes " + cult_knowledge[i]);
                aGame.ssR.Add("p2 likes " + cult_knowledge[i]);
                aGame.drelation_target = sRand.Next(1, 6);
                SocialGames.singleton.addSocialGame(aGame);
                
                aGame = new SGame("Condemn " + cult_knowledge[i] + "!");
                aGame.ssR.Add("p1 dislikes " + cult_knowledge[i]);
                aGame.ssR.Add("p2 dislikes " + cult_knowledge[i]);
                aGame.drelation_target = sRand.Next(1, 6);
                SocialGames.singleton.addSocialGame(aGame);
                
                aGame = new SGame("Admire " + cult_knowledge[i] + "!");
                aGame.ssR.Add("p1 likes " + cult_knowledge[i]);
                aGame.ssR.Add("p2 dislikes " + cult_knowledge[i]);
                aGame.drelation_target = sRand.Next(-6, -1);
                SocialGames.singleton.addSocialGame(aGame);

                aGame = new SGame("Condemn " + cult_knowledge[i] + "!");
                aGame.ssR.Add("p2 likes " + cult_knowledge[i]);
                aGame.ssR.Add("p1 dislikes " + cult_knowledge[i]);
                aGame.drelation_target = sRand.Next(-6, -1);
                SocialGames.singleton.addSocialGame(aGame);
                
            }

        }

    }
}
