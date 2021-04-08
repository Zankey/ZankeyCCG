using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZCCG
{

    [CreateAssetMenu(menuName = "Areas/PlayerBoardLogicWhenHoldingCard")]
    public class PlayerBoardLogic : AreaLogic
    {
        public CardInstance inst;
        public SO.TransformVariable areaGrid;
        public GameElements.GE_Logic cardDownLogic;
        
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
                    
                    Settings.DropCreatureCard(inst.transform, p.currentHolder.boardGrid.value.transform, inst);
                    Debug.Log("This is the current player holder when dropping" + p.username );
                    inst.currentLogic = cardDownLogic;
                    inst.gameObject.SetActive(true);        
                
                }
                if (c.cardType is Weapon)
                {
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
                    if (c.hasTargeting)
                    {
                        Debug.Log("Spell Targeting");
                        Settings.spellManager.SetSpellTarget();
                    }
                    else
                    {
                        Debug.Log("Spell has No Targeting");
                        Settings.spellManager.CastSpell(inst.spellId);
                    }

                    Settings.manaManager.PayManaCost(inst.viz.card.cost);
                    p.handcards.Remove(inst);
                    inst.SendToGraveyard();
                    Debug.Log("Spell cast, and sent to GY");
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
