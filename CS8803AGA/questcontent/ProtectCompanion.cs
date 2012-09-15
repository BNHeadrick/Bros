using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CS8803AGA.questcontent
{
    class ProtectCompanion : BaseQuest
    {
        public ProtectCompanion()
        {

        }

        public new bool isComplete()
        {
            return false;
        }

        public new BaseQuest getNextState()
        {
            
            return null;
        }
    }
}
