using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZCCG.GameElements
{
    [CreateAssetMenu(menuName = "Game Elements/Player Board Card")]
    public class PlayerBoardCard : GE_Logic
    {

        public SO.GameEvent onTargetSelection;
        public ZCCG.GameStates.State targetSelection;

        public override void OnClick(CardInstance inst)
        {

            if (inst.CanAttack())
            {
                Settings.gameManager.currentSelectedHolder.ResetSelectedPlayer();
                Settings.gameManager.currentSelectedHolder.SetSelectedCard(inst);
                Settings.gameManager.SetState(targetSelection); 
                onTargetSelection.Raise();
                
                Debug.Log("This card can attack! , select target!");
            }
            else
            {
                // if (!Settings.gameManager.currentPlayer.cardsDown.Contains(inst))
                // {
                //     Debug.Log("That is the enemy's minion");
                // }
                if (inst.isAsleep && !inst.hasAttacked)
                {
                    Debug.Log("This card cant attack yet");
                }
                if (inst.hasAttacked)
                {
                    Debug.Log("This card already attacked");
                }
                // if (inst.isFrozen)
                // {
                //     Debug.Log("This card already attacked");
                // }
                //
                // ECT......
                
            }
        }

        public override void OnHighlight(CardInstance inst)
        {
            Debug.Log(" Enlarge card preview");
            
        }
    }
}
