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

        public void addCultKnowledge(String key, CharacterController cc){
            CulturalKnowledge ck = new CulturalKnowledge(cc);
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
        CharacterController charCont;
        public CulturalKnowledge(CharacterController cc)
        {
            charCont = cc;
            innerCulKno = new Dictionary<string, InnerCulturalKnowledge>();
        }

        public void addInnerNetwork(String key, string obj)
        {
            InnerCulturalKnowledge isn = new InnerCulturalKnowledge(obj);
            innerCulKno.Add(key, isn);
        }

        public InnerCulturalKnowledge getInnerNetwork(string key)
        {
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

        public string obj;
        public int relation;
        public InnerCulturalKnowledge(string _obj)
        {
            obj = _obj;
            relation = 0;
        }
    }
}
