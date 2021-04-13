using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ZCCG
{

    [CreateAssetMenu(menuName = "Areas/PlayerBoardLogicWhenHoldingCard")]
    public class PlayerBoardLogic : AreaLogic
    {
        public CardInstance inst;
        public SO.TransformVariable areaGrid;
        public GameElements.GE_Logic cardDownLogic;
        public SO.GameEvent onTargetSelection;
        public ZCCG.GameStates.State targetSelection;
        
        public override void Execute()
        {
            inst = Settings.gameManager.currentSelectedHolder.GetSelectedCard();

            if (inst == null)
                return;

            Card c = inst.viz.card;
            PlayerHolder p = Settings.gameManager.currentPlayer;
            bool canUse = p.CanUseCard(c);

            if (canUse)
            {
                if (c.cardType is Minion) 
                {
                    // Has battle cry? -> Effect targeting here

                    Settings.DropCreatureCard(inst.transform, p.currentHolder.boardGrid.value.transform, inst);
                    Debug.Log("This is the current player holder when dropping" + p.username );
                    inst.currentLogic = cardDownLogic;
                    inst.gameObject.SetActive(true);        
                
                }
                if (c.cardType is Weapon)
                {
                    // Has battle cry? -> Effect targeting here

                    Transform currentWeapon = p.currentHolder.weaponHolder.value.transform;    
                    //Destroy Current Weapon before adding a new one 
                    if (currentWeapon.childCount > 0)
                    {
                        Settings.gameManager.currentPlayer.SubtractHeroAttack(currentWeapon.GetComponentInChildren<CardInstance>().currentAttack);
                        Debug.Log("Subtracting attack: " + currentWeapon.GetComponentInChildren<CardInstance>().currentAttack);
                        Settings.gameManager.currentPlayer.heroStatsUI.UpdateAttack();
                        currentWeapon.GetChild(0).gameObject.GetComponent<CardInstance>().ResetCardStats();
                        currentWeapon.GetChild(0).gameObject.GetComponent<CardInstance>().SendToGraveyard();         
                    }

                    Settings.DropCreatureCard(inst.transform, p.currentHolder.weaponHolder.value.transform, inst);
                    inst.currentLogic = cardDownLogic;
                    Settings.gameManager.currentPlayer.AddHeroAttack(inst.currentAttack);
                    Settings.gameManager.currentPlayer.heroStatsUI.UpdateAttack();
            
                    inst.gameObject.SetActive(true);
                }
                else
                if(c.cardType is Spell)
                {
                    // Set spell manager able to cast a spell
                    Settings.spellManager.spellQueued = true;
                    // if spell has targeting, pick target first before casting spell.
                    if (c.hasTargeting)
                    {
                        // Debug.Log("Setting spell target");
                        Settings.gameManager.currentSelectedHolder.currentSelectedCard.gameObject.SetActive(false);
                        // Settings.gameManager.currentSelectedHolder.ResetSelectedCard();
                        // Settings.gameManager.currentSelectedHolder.SetSelectedPlayer(p);
                        // // Settings.gameManager.DrawTargetingLine(Settings.gameManager.currentPlayer.heroStatsUI.gameObject.transform.position);
                        // Debug.Log("Setting State: Target Selection");
                        // Settings.gameManager.SetState(targetSelection);
                        // Debug.Log("SetState: Target Selection");
                        // onTargetSelection.Raise();
                        // Debug.Log("select spell target!");

                    }
                    else
                    {
                        Debug.Log("Spell has No Targeting, Casting spell");
                        Settings.spellManager.CastSpell(inst.spellId, null, null);
                        Settings.manaManager.PayManaCost(inst.viz.card.cost);
                        p.handcards.Remove(inst);
                        inst.SendToGraveyard();
                        Debug.Log("Spell cast, and sent to GY");
                    }
                }
            }
            else
            {
            Settings.RegisterEvent("Not enough mana!", Color.red);
            }
            
            // Removes any destroyed cards from the board
            // Settings.gameManager.BoardCleanup();
        }
    }
}
