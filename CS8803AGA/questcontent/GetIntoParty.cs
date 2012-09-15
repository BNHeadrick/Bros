using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CS8803AGA.questcontent
{
    class GetIntoParty : BaseQuest
    {
        public GetIntoParty()
        {
            nextState = new ProtectCompanion();
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
