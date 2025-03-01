﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Stores all of the spells, looked up by spell ID
// TODO:
// Implement damage field from cards to use instead of hard coding damage so we can reuse spell IDs

namespace ZCCG
{
    public class SpellManager : MonoBehaviour
    {

        public SO.GameEvent onTargetSelection;
        public ZCCG.GameStates.State spellTargetSelection;

        public CurrentSelectedHolder targetHolder;

        // public CardInstance spellTarget;

        public bool spellQueued = false;

        //public int damage;

        private void Start() {
            Settings.spellManager = this;
            if (Settings.gameManager.currentSelectedHolder != null)
            {
                targetHolder = Settings.gameManager.currentSelectedHolder;
            }
        }

        public void CastSpell(int spellId, int spellValue, CardInstance targetCard, PlayerHolder targetPlayer)
        {
            // TODO:
            // gets the "damage" value from the spell 
            //damage = Settings.gameManager.currentSelectedHolder.currentSelectedCard.currentAttack;
            Debug.Log("Spell Was Cast");
            if (spellQueued == true)
            {
                switch (spellId)
                {
                    //deal X spell damage to single target  
                    case(0):
                        Debug.Log("Case 0: Target - x Damage - spell");
                        //spell logic
                        if (targetCard != null)
                        {
                            targetCard.SubtractCardHealth( spellValue + Settings.gameManager.currentPlayer.currentSpellDamage);
                            spellQueued = false;
                            Debug.Log("Target minion took "+ spellValue + " damage");
                        }
                        else 
                        if (targetPlayer != null)
                        {
                            targetPlayer.SubtractHeroCurrentHealth (spellValue + Settings.gameManager.currentPlayer.currentSpellDamage);
                            spellQueued = false;
                            Debug.Log("Target player took " + spellValue + " damage");
                        }
                        return;

                    //deal X spell damage to all enemy minions  
                    case (1):
                        Debug.Log("Case 1: AOE - " + spellValue + " Damage - Spell");
                        for (int i = Settings.gameManager.otherPlayer.cardsDown.Count - 1; i >= 0 ; i--)
                        {
                            Settings.gameManager.otherPlayer.cardsDown[i].SubtractCardHealth(spellValue + Settings.gameManager.currentPlayer.currentSpellDamage);
                            Debug.Log("" + spellValue + " Damage Dealt to: " + i);
                        }
                        spellQueued = false;
                        return;

                    // Gain X Armor
                    case(2):
                        Debug.Log("Case 2: Gain Armor");
                        Settings.gameManager.currentPlayer.AddHeroArmor(spellValue);
                        spellQueued = false;
                        return;

                    // Draw X Cards
                    case(3):
                        Debug.Log("Case 3: Draw Card");
                        Settings.gameManager.DrawCard(spellValue, targetPlayer);
                        return;

                    // Deal X damage to a random enemy minion
                    case(4):  
                        if (Settings.gameManager.otherPlayer.cardsDown.Count > 0)
                        {
                            Debug.Log("Case 4: Deal " + spellValue + " damage to a random minion");
                            Debug.Log("Other player is currently: "+Settings.gameManager.otherPlayer.username);
                            int randIndex = Random.Range(0, Settings.gameManager.otherPlayer.cardsDown.Count);
                            Settings.gameManager.otherPlayer.cardsDown[randIndex].SubtractCardHealth(spellValue);
                        }
                        spellQueued = false;
                        return;

                    // Deal X damage to a random enemy (including hero)
                    case(5):
                        if (Settings.gameManager.otherPlayer.cardsDown.Count >= 0)
                        {
                            Debug.Log("Case 5: Deal " + spellValue + " damage to a random enemy");
                            Debug.Log("Other player is currently: " + Settings.gameManager.otherPlayer.username);
                            int randIndex = Random.Range(0, Settings.gameManager.otherPlayer.cardsDown.Count + 1);
                            Debug.Log("OtherPlayer's cards on board: " + Settings.gameManager.otherPlayer.cardsDown.Count);
                            Debug.Log("Random Index is: "+ randIndex);

                            if (Settings.gameManager.otherPlayer.cardsDown.Count == 0 || randIndex >= Settings.gameManager.otherPlayer.cardsDown.Count)
                            {
                                Settings.gameManager.otherPlayer.SubtractHeroCurrentHealth(spellValue);
                                Debug.Log("Random damage dealt to enemy hero");
                            }                  
                            else if (Settings.gameManager.otherPlayer.cardsDown.Count > 0 && randIndex <= Settings.gameManager.otherPlayer.cardsDown.Count)
                            {
                                Settings.gameManager.otherPlayer.cardsDown[randIndex].SubtractCardHealth(spellValue);
                                Debug.Log("Random damage dealt to enemy minion");
                            }
                        }
                        spellQueued = false;
                        return;
                    
                    default:
                        targetHolder.ResetAll();
                        Debug.Log("Spell does not exist");
                        return;
                }
            }
        }

        // public void SetSpellTarget()
        // {
        //     Debug.Log("Setting spell target");
        //     Settings.gameManager.currentSelectedHolder.currentSelectedCard.gameObject.SetActive(false);
        //     // Settings.gameManager.DrawTargetingLine(Settings.gameManager.currentPlayer.heroStatsUI.gameObject.transform.position);
        //     Settings.gameManager.SetState(spellTargetSelection);
        //     Debug.Log("Setstate: spellTargetSelection");
        //     onTargetSelection.Raise();
        // }
        

    }
}
