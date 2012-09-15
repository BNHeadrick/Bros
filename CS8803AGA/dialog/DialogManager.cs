using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace CS8803AGA.dialog
{
    class DialogManager
    {
        private static Dictionary<int, Dialog> dialogs;

        public static void load(Point location)
        {
            dialogs = DialogDefinitions.charactersAt(location);
        }

        /**
         * Gets the next dialog for the specified character
         *@param character which character to get the dialog for
         *@return the dialog or null if the character does not exist
         */
        public static Dialog get(int character)
        {
            
            if (dialogs.ContainsKey(character))
            {
                Dialog d = dialogs[character].getCurrent();
                // maybe need to do something special to set next flag??

                dialogs[character].next();
                return d;
            }
            return null;
        }
    }
}
