/*
 The cultural knowledgebase 
stores two categories of information: the cultural norms associated 
with a cultural knowledge base entry and how individual 
characters are associated with entries. This knowledge base is 
used to represent character personalities and to identify 
similarities between characters in social game instantiation. 
*/

/***
 * 
***/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CS8803AGA.controllers;

namespace CS8803AGA.Knowledge
{
    public class CulturalKnowledgebase
    {
        Dictionary<string, CulturalKnowledge> dictionary;
        public static CulturalKnowledgebase singleton = new CulturalKnowledgebase();

        protected CulturalKnowledgebase()
        {
            dictionary = new Dictionary<string, CulturalKnowledge>();
        }

        public void addCultKnowledge(String key){
            CulturalKnowledge ck = new CulturalKnowledge();
            dictionary.Add(key, ck);
        }

        public CulturalKnowledge getCulturalKnowledge(String key)
        {
            CulturalKnowledge theCK = null;
            // See whether Dictionary contains this string.
	        if (dictionary.ContainsKey(key))
	        {
	            theCK = dictionary[key];
	        }
            return theCK;
        }

    }

    

    public class CulturalKnowledge
    {
        Dictionary<string, InnerCulturalKnowledge> innerCulKno;

        public CulturalKnowledge()
        {
            innerCulKno = new Dictionary<string, InnerCulturalKnowledge>();
        }

        public void addInnerNetwork(string obj)
        {
            InnerCulturalKnowledge isn = new InnerCulturalKnowledge();
            innerCulKno.Add(obj, isn);
        }

        public InnerCulturalKnowledge getInnerNetwork(string key)
        {
            Console.WriteLine("the key is " + key);
            return innerCulKno[key];
        }
    }

    /*
     *This class has is the "many" part of each social classes' one to many relationship.
     */
    public class InnerCulturalKnowledge
    {
        public const int RELATION_MIN = 0;
        public const int RELATION_MAX = 100;

        public int relation;
        public InnerCulturalKnowledge()
        {
            relation = 0;
        }
    }
}
