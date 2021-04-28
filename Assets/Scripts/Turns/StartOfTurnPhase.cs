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
                GameManager.singleton.SetState(null);
                isInit = false;
            }
        }

        public override void OnStartPhase()
        {
            if (!isInit)
            {
                GameManager.singleton.SetState(null);
                GameManager.singleton.onPhaseChanged.Raise();
                isInit = true;
            }
        
            Debug.Log("There are" + GameManager.singleton.currentPlayer.currentHolder.boardGrid.value.GetComponentsInChildren<CardInstance>() + "board cards");

            //Check for "Start of Turn" Cards
            foreach(CardInstance inst in Settings.gameManager.currentPlayer.currentHolder.boardGrid.value.GetComponentsInChildren<CardInstance>())
            {
                if(inst.viz.CheckTags("StartOfTurn"))
                //Do End of turn things Here (If not silenced);
                {
                    Debug.Log("This card has the SOT tag!");
                    if (!inst.isSilenced)
                    {
                        Settings.spellManager.spellQueued = true;
                        Settings.spellManager.CastSpell(inst.spellId, inst.spellValue, null, Settings.gameManager.currentPlayer);
                    }

                }
            }
            
            if (Settings.gameManager.currentPlayer.deck.Count > 0)
            {
                Settings.gameManager.DrawCard(1 , Settings.gameManager.currentPlayer);
            }
            else
            {
                GameManager.singleton.currentPlayer.SubtractHeroCurrentHealth(Settings.gameManager.currentPlayer.fatigueCount);
                Debug.Log("OUT OF CARDS! "+ Settings.gameManager.currentPlayer.username +" takes "+ Settings.gameManager.currentPlayer.fatigueCount+ " damage!");
                Settings.gameManager.currentPlayer.fatigueCount++;
            }

            forceExit = true;
            
        }
    }
}
