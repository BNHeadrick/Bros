using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CS8803AGA.questcontent
{

    public class QuestManager
    {
        private BaseQuest currentQuest;

        //initialize initialQuest with the GetCompanion quest
        public QuestManager()
        {
            currentQuest = new GetCompanion();
        }

        public void checkForSuccess()
        {
            if (currentQuest.isComplete()){
                changeState();
            }
        }

        private void changeState()
        {
            currentQuest = currentQuest.getNextState();
            if (currentQuest == null)
            {

            }
        }
    }


}
