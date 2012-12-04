using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CS8803AGA.Knowledge
{
    class SocialGames
    {

        Dictionary<string, SGame> dictionary;

        public SocialGames()
        {
            dictionary = new Dictionary<string, SGame>();
        }

        public void addSocialGame(String key, String aName)
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
        String name;
        public SGame(String aName)
        {
            name = aName;
        }

    }

}
