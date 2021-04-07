using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace ZCCG
{
    public class SpellManager : MonoBehaviour
    {

        public SO.GameEvent onTargetSelection;
        public ZCCG.GameStates.State spellTargetSelection;

        public CurrentSelectedHolder targetHolder;

        private void Start() {
            Settings.spellManager = this;
            if (Settings.gameManager.currentSelectedHolder != null)
            {
                targetHolder = Settings.gameManager.currentSelectedHolder;
            }
        }

        public bool CastSpell(int spellId)
        {
            Debug.Log("Spell Was Cast");
            bool spellCompleted = false;
            switch (spellId)
            {
                //deal 1 spell damage to single target  
                case(0):
                    Debug.Log("Case 0: Target - 1 Damage - spell");
                    //spell logic
                    if (targetHolder.currentTargetCard != null)
                    {
                        targetHolder.currentTargetCard.SubtractCardHealth( 1 + Settings.gameManager.currentPlayer.currentSpellDamage);
                        spellCompleted = true;
                    }
                    else 
                    if (targetHolder.currentTargetPlayer != null)
                    {
                        targetHolder.currentTargetPlayer.SubtractHeroCurrentHealth (1 + Settings.gameManager.currentPlayer.currentSpellDamage);
                        spellCompleted = true;
                    }
                    targetHolder.ResetTargetCard();
                    targetHolder.ResetTargetPlayer();
                    return spellCompleted;

                //deal 1 spell damage to all enemy minions  
                case (1):
                    Debug.Log("Case 1: AOE - 1 Damage - Spell");
                    foreach (CardInstance inst in Settings.gameManager.otherPlayer.cardsDown)
                    {
                        inst.SubtractCardHealth(1 + Settings.gameManager.currentPlayer.currentSpellDamage);
                        Debug.Log("1 Damage Dealt to: " + inst);
                    }
                    spellCompleted = true;
                    return spellCompleted;
                
                default:
                    targetHolder.ResetAll();
                    return spellCompleted;
            }
        }

        public void SetSpellTarget()
        {
            Settings.gameManager.currentSelectedHolder.currentSelectedCard.gameObject.SetActive(false);
            Settings.gameManager.DrawTargetingLine(Settings.gameManager.currentPlayer.heroStatsUI.gameObject.transform.position);
            Settings.gameManager.SetState(spellTargetSelection);
            onTargetSelection.Raise();
        }
        

    }
}
