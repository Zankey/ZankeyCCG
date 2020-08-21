using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZCCG
{
    // This will be used for "Start of turn" Cards and probably will make a new one for "End of Turn" cards
    [CreateAssetMenu(menuName = "Turns/Start Of Turn Phase")]
    public class StartOfTurnPhase : Phase
    {

        public override bool IsComplete()
        {
            if (forceExit)
            {
                forceExit = false;
                return true;
            }

            return false;
        }

        public override void OnEndPhase()
        {
            if (isInit)
            {
                Settings.gameManager.SetState(null);
                isInit = false;
            }
        }

        public override void OnStartPhase()
        {
            if (!isInit)
            {
                Settings.gameManager.SetState(null);
                Settings.gameManager.onPhaseChanged.Raise();
                isInit = true;
            }
        
            Debug.Log("There are" + Settings.gameManager.currentPlayer.currentHolder.boardGrid.value.GetComponentsInChildren<CardInstance>() + "board cards");

            //Check for "Start of Turn" Cards
            foreach(CardInstance inst in Settings.gameManager.currentPlayer.currentHolder.boardGrid.value.GetComponentsInChildren<CardInstance>())
            {
                if(inst.viz.CheckTags("StartOfTurn"))
                {
                    Debug.Log("This card has the SOT tag!");
                    //Do Start of turn things
                }
            }

            forceExit = true;
            
        }
    }
}
