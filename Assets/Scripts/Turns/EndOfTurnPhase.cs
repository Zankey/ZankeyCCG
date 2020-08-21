using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZCCG
{
    // This will be used for "End of turn" Cards and probably will make a new one for "Start of turn" cards
    [CreateAssetMenu(menuName = "Turns/End Of Turn Phase")]
    public class EndOfTurnPhase : Phase
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
                //Activate any cards with "End of turn" effects
            }

            //Check for "End of Turn" Cards
            foreach (CardInstance inst in Settings.gameManager.currentPlayer.currentHolder.boardGrid.value.GetComponentsInChildren<CardInstance>())
            {
                if (inst.viz.CheckTags("EndOfTurn"))
                {
                    Debug.Log("This card has the EOT tag, Do its stuff!");
                    //Do End of turn things Here
                }
                inst.isAsleep = true;
            }

            forceExit = true;
        }
    }
}
