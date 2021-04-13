using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using ZCCG.GameStates;

namespace ZCCG
{
    [CreateAssetMenu(menuName = "Actions/SelectCardToAttack")]
    public class SelectTarget_Attack : Action
    {

        public State playerControlState;
        public SO.GameEvent onPlayerControlState;

        [System.NonSerialized]
        private bool isMinion;
        [System.NonSerialized]
        private bool isWeapon;
        [System.NonSerialized]
        private bool isSpell;
        [System.NonSerialized]
        private bool isHero;
        [System.NonSerialized]
        private bool tauntPresent;
        [System.NonSerialized]
        private CurrentSelectedHolder currentHolder;
        [System.NonSerialized]
        private PlayerHolder currentHero;
        [System.NonSerialized]
        private CardInstance currentCard;
        [System.NonSerialized]
        private CardType currentType;
        [System.NonSerialized]
        private Vector3 currentSelectedPosition;

        public override void Execute(float d)
        {
            Debug.Log("got here - Attack targeting State");

            isHero = false;
            isMinion = false;
            isSpell = false;
            isWeapon = false;

            currentHolder = Settings.gameManager.currentSelectedHolder;

            if(currentHolder.GetSelectedCard() != null)
            {
                Debug.Log("Try succeeded for card");
                currentCard = currentHolder.GetSelectedCard();
                currentType = currentCard.viz.card.cardType;
                

                if (currentType is Minion)
                {
                    isMinion = true;
                    currentSelectedPosition = currentCard.transform.position;
                }
                if (currentType is Spell)
                {
                    isSpell = true;
                    currentSelectedPosition = Settings.gameManager.currentPlayer.heroStatsUI.transform.position;
                }
                if (currentType is Weapon)
                {
                    isWeapon = true;
                    currentSelectedPosition = currentCard.transform.position;
                }

            }
            else
            {
                currentCard = null;
                currentType = null;
            }

            if (currentHolder.GetSelectedPlayer() != null)
            {
                Debug.Log("Try succeeded for Hero");
                currentHero = currentHolder.GetSelectedPlayer();
                currentSelectedPosition = currentHero.heroStatsUI.transform.position;
                isHero = true;
            }
            else
            {
                currentHero = null;
            }

            Settings.gameManager.DrawTargetingLine(currentSelectedPosition);

            //if (currentCardSelected.TryGetComponent(out CardInstance )

            if (Input.GetMouseButtonDown(0))
            {
                List<RaycastResult> results = Settings.GetUIObjs();

                foreach (RaycastResult r in results)
                {

                    CardInstance inst = r.gameObject.GetComponentInParent<CardInstance>();
                    PlayerHolder op = Settings.gameManager.otherPlayer;
                    PlayerHolder cp = Settings.gameManager.currentPlayer;
                    HeroManager hm = r.gameObject.GetComponentInParent<HeroManager>();

                    //Taunt Check
                    foreach (CardInstance ci in op.cardsDown)
                    {
                        if(ci.tags.TryGetValue("Taunt", out tauntPresent))
                        {
                            break;
                        }
                        else
                        {
                            tauntPresent = false;
                        }
                    }
                    
                    if (inst != null)
                    {   
                        if (op.cardsDown.Contains(inst))
                        {
                            Debug.Log("your target is a card instance on the other player's board");
                            if (validTarget(null, inst))
                            {
                                if(isHero)
                                {
                                    
                                    inst.SubtractCardHealth(currentHero.currentAttack);
                                    currentHero.SubtractHeroCurrentHealth(inst.currentAttack);
                                    currentHero.currentHolder.weaponHolder.value.GetComponentInChildren<CardInstance>().SubtractCardHealth(1);
                                    SetHasAttacked(currentHero, null);
                                    Debug.Log("attacking Card, isHero?: "+ isHero);
                                }
                                if(isMinion)
                                {
                                    Debug.Log("attacker's atk/health: "+ currentCard.currentAttack +"/"+ currentCard.currentHealth);
                                    Debug.Log("defender's atk/health: " + inst.currentAttack + "/" + inst.currentHealth);

                                    inst.SubtractCardHealth(currentCard.currentAttack);
                                    currentCard.SubtractCardHealth(inst.currentAttack);
                                    SetHasAttacked(currentHero, null);

                                }
                                if(isSpell)
                                {
                                    // TODO:
                                    // Got here!
                                    // 1. Fix Line draw origin
                                    // 2. Toggle off visibility of the card
                                    // 3. Cast the spell
                                    
                                    Settings.spellManager.CastSpell(currentCard.spellId, inst, null);
                                    Debug.Log("Casting a spell");
                                }
                            }
                        }
                        if (cp.cardsDown.Contains(inst))
                        {
                            Debug.Log("your target is a card instance on the the current player's board");
                        }
                    }
                    if (hm != null)
                    {
                        if (hm.player.Equals(op))
                        {
                            if(validTarget(op, null))
                            {
                                Debug.Log("your target is the other player's hero");
                                if (isHero)
                                {
                                    op.SubtractHeroCurrentHealth(currentHero.currentAttack);
                                    currentHero.currentHolder.weaponHolder.value.GetComponentInChildren<CardInstance>().SubtractCardHealth(1);
                                    SetHasAttacked(currentHero, null);
                                    isHero = false;
                                    Debug.Log("attacking Card, isHero?: " + isHero);
                                }
                                if (isMinion)
                                {
                                    op.SubtractHeroCurrentHealth(currentCard.currentAttack);
                                    SetHasAttacked(currentHero, null);
                                    isMinion = false;
                                }
                                if (isSpell)
                                {
                                    // TODO:
                                    // Got here!
                                    // 1. Fix Line draw origin
                                    // 2. Toggle off visibility of the card
                                    // 3. Cast the spell

                                    Settings.spellManager.CastSpell(currentCard.spellId, null, op);
                                    Debug.Log("Casting a spell");
                                }
                            }
                        }

                        if (hm.player.Equals(cp))
                        {
                            Debug.Log(" Your target is your own hero");
                            if(isSpell)
                            {
                                Settings.spellManager.CastSpell(currentCard.spellId, null, cp);
                                Debug.Log("Casting a spell");
                            }
                            else
                            Debug.Log("cant attack that target");
                        }

                        else
                        {
                            Debug.Log("No player was found");
                        }
                    }

                    Settings.gameManager.targetingLine.gameObject.SetActive(false);
                    currentHolder.ResetSelectedCard();
                    currentHolder.ResetSelectedPlayer();
                    tauntPresent = false;
                    Settings.gameManager.SetState(playerControlState);
                    onPlayerControlState.Raise();
                    return;
                
                }
            }

            if (Input.GetMouseButtonDown(1))
            {
                Debug.Log("Right Click, attack aborted");
                Settings.gameManager.targetingLine.gameObject.SetActive(false);
                currentHolder.ResetSelectedCard();
                currentHolder.ResetSelectedPlayer();
                tauntPresent = false;
                Settings.gameManager.SetState(playerControlState);
                onPlayerControlState.Raise();
                return;
            }

            void SetHasAttacked(PlayerHolder ph, CardInstance ci)
            {
                if (isMinion)
                {
                    currentCard.hasAttacked = true;
                    currentCard.isAsleep = true;
                    currentCard.viz.asleep.gameObject.SetActive(true);
                }
                else
                if (isHero)
                {
                    ph.hasAttacked = true;
                }
            }

            bool validTarget(PlayerHolder ph, CardInstance ci)
            {
                bool result = true;

                if (isHero || isMinion)
                {
                    if (ci != null && tauntPresent && !ci.tags.ContainsKey("Taunt"))
                    {
                        result = false;
                        Debug.Log("There is a minion with taunt on the board!");
                    }
                    if (ph != null && tauntPresent)
                    {
                        result = false;
                        Debug.Log("There is a minion with taunt on the board!");
                    }
                    if(ci != null && tauntPresent && ci.tags.ContainsKey("Taunt"))
                    {
                        result = true;
                    }
                    if(ci != null && ci.tags.ContainsKey("Stealth"))
                    {
                        result = false;
                    }
                    if(ph != null && ph.isStealth)
                    {
                        result = false;
                    }
                }


                return result;
            }
            // void ResolveAbilities()
            // {

            // }

        }
    }
}
