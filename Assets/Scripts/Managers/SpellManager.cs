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

        public bool spellQueued = false;

        private void Start() {
            Settings.spellManager = this;
            if (Settings.gameManager.currentSelectedHolder != null)
            {
                targetHolder = Settings.gameManager.currentSelectedHolder;
            }
        }

        public void CastSpell(int spellId)
        {
            Debug.Log("Spell Was Cast");
            if (spellQueued == true)
            {
                switch (spellId)
                {
                    //deal 1 spell damage to single target  
                    case(0):
                        Debug.Log("Case 0: Target - 1 Damage - spell");
                        //spell logic
                        if (targetHolder.currentTargetCard != null)
                        {
                            targetHolder.currentTargetCard.SubtractCardHealth( 1 + Settings.gameManager.currentPlayer.currentSpellDamage);
                            spellQueued = false;
                        }
                        else 
                        if (targetHolder.currentTargetPlayer != null)
                        {
                            targetHolder.currentTargetPlayer.SubtractHeroCurrentHealth (1 + Settings.gameManager.currentPlayer.currentSpellDamage);
                            spellQueued = false;
                        }
                        targetHolder.ResetTargetCard();
                        targetHolder.ResetTargetPlayer();
                        return;

                    //deal 1 spell damage to all enemy minions  
                    case (1):
                        Debug.Log("Case 1: AOE - 1 Damage - Spell");
                        for (int i = Settings.gameManager.otherPlayer.cardsDown.Count - 1; i >= 0 ; i--)
                        {
                            Settings.gameManager.otherPlayer.cardsDown[i].SubtractCardHealth(1 + Settings.gameManager.currentPlayer.currentSpellDamage);
                            Debug.Log("1 Damage Dealt to: " + i);
                        }
                        spellQueued = false;
                        return;
                    
                    default:
                        targetHolder.ResetAll();
                        Debug.Log("Spell does not exist");
                        return;
                }
            }
            else
            {
                return;
            }
        }

        public void SetSpellTarget()
        {
            Debug.Log("Setting spell target");
            Settings.gameManager.currentSelectedHolder.currentSelectedCard.gameObject.SetActive(false);
            Settings.gameManager.DrawTargetingLine(Settings.gameManager.currentPlayer.heroStatsUI.gameObject.transform.position);
            Settings.gameManager.SetState(spellTargetSelection);
            Debug.Log("Setstate: spellTargetSelection");
            onTargetSelection.Raise();
        }
        

    }
}
