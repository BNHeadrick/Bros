using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CS8803AGA.questcontent
{
    class BaseQuest
    {
        protected BaseQuest nextState;
        
        public BaseQuest()
        {
        }

        
        public bool isComplete()
        {
            return false;
        }

        public BaseQuest getNextState()
        {
            return null;
        }
        
    }
}
