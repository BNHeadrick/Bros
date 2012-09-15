using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CS8803AGA;

namespace CS8803AGA.questcontent
{
    //Objective is to get your companion by providing him with liquer.
    //talks to Constants.LIQUERMERCH then Constants.COMPANION
    
    class GetCompanion : BaseQuest
    {
        
        public GetCompanion()
        {
            nextState = new GetIntoParty();
        }

        public new bool isComplete()
        {

            return false;
        }

        public new BaseQuest getNextState()
        {
            
            return nextState;
        }

    }
}
