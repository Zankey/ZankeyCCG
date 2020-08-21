using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


/// <summary>
/// This is the instance of each card at runtime, i want to store any changes to health/ atk/ ect here 
/// </summary>

namespace ZCCG
{
    public class CardInstance : MonoBehaviour, IClickable     
    {
        public CardViz viz;
        public CardType type;
        public string clanTag;
        public ZCCG.GameElements.GE_Logic currentLogic;
        public Dictionary<string, bool> tags = new Dictionary<string, bool>(32);

        public TMP_Text boardAttack;
        public TMP_Text boardHealth;
        public TMP_Text handHealth;
        public TMP_Text handAttack;
        public TMP_Text handCost;
        public TMP_Text weaponAttack;
        public TMP_Text weaponHealth;

        private string ownerUsername;

        public int currentCost;
        public int currentAttack;
        public int currentHealth;

        //Card States:

        public bool isAsleep;
        public bool isDead;
        public bool isDiscarded;
        public bool hasAttacked;
        public bool isTargetable;
        public bool isSilenced;
        public int windfuryCount = 1;

        //Card Tags:
        
        public bool isCharge;
        public bool isBattlecry;
        public bool isDeathrattle;
        public bool isDivineShield;
        public bool isEndOfTurn;
        public bool isFrozen;
        public bool isImmune;
        public bool isInspire;
        public bool isOnEvent;
        public bool isReborn;
        public bool isRush;
        public bool isStartOfTurn;
        public bool isStealth;
        public bool isSpellburst;
        public bool isSpellsAndHeroPowers;
        public bool isTaunt;
        public bool isWindfury;

        //Card Clans

        public bool clanBeast;
        public bool clanDemon;
        public bool clanMurloc;
        public bool clanPirate;
        public bool clanMech;
        public bool clanUndead;
        public bool clanDragon;
        public bool clanElemental;
        public bool clanTotem;
        

        void Start() 
        {
            viz = GetComponent<CardViz>();
            type = viz.card.cardType;
            clanTag = viz.clanTag.GetComponentInChildren<TMP_Text>().text;
            ResetCardStats();

            SetBaseTags();

            isAsleep = true;
            isTargetable = true;
            isDead = false;
            isDiscarded = false;
            hasAttacked = false;

            if (isAsleep)
            {
                viz.asleep.gameObject.SetActive(true);
            }

            if (tags.TryGetValue("Charge", out isCharge))
            {
                isAsleep = false;
                viz.asleep.gameObject.SetActive(false);
            }

            if (tags.TryGetValue("Frozen", out isFrozen))
            {
                isAsleep = false;
                viz.asleep.gameObject.SetActive(false);
                viz.frozen.gameObject.SetActive(true);
            }

        }

        public bool CanAttack()
        {
            bool result = false;

            if (viz.card.cardType.TypeAllowsForAttack(this))
            {
                result = true;
            }
            if (!hasAttacked && !isAsleep)
            {
                result = true;
            }

            if (isCharge)
            {
                result = true;
            }

            if (isAsleep)
                result = false;

            if (isFrozen)
                result = false;


            return result;
        }

        public bool ValidTarget()
        {
            bool result = true;

            if(isTargetable)
                result = true;

            if(!isTargetable)
                result = false;

            return result;

        }

        public void OnClick()
        {
            if (currentLogic == null)
            return;

            currentLogic.OnClick(this);
        }

        public void OnHighlight()
        {

            if (currentLogic == null)
                return;
            
            currentLogic.OnHighlight(this); 
        }

        public void ResetCardStats()
        {
            currentCost = viz.card.cost;
            currentAttack = viz.card.attack;
            currentHealth = viz.card.health;
        }

        public void AddCardAttack(int i)
        {
            currentAttack += i;
            UpdateCardStatViz();
        }
        public void SubtractCardAttack(int i)
        {
            currentAttack -= i;
            UpdateCardStatViz();
        }
        public void AddCardHealth(int i)
        {
            currentHealth += i;
            UpdateCardStatViz();
        }
        public void SubtractCardHealth(int i)
        {
            currentHealth -= i;
            if(currentHealth <= 0)
            {
                ResetCardStats();
                {
                    if(GetOwner().Equals(Settings.gameManager.currentPlayer.username))
                    {
                        SendToGraveyard();
                        Settings.gameManager.currentPlayer.cardsDown.Remove(this);
                    }
                    else
                    if(GetOwner().Equals(Settings.gameManager.otherPlayer.username))
                    {
                        SendToEnemyGraveyard();
                        Settings.gameManager.otherPlayer.cardsDown.Remove(this);
                    }
                }
            }
            UpdateCardStatViz();
        }
        public void AddCardCost(int i)
        {
            currentCost += i;
            UpdateCardStatViz();
        }
        public void SubtractCardCost(int i)
        {
            currentCost -= i;
            UpdateCardStatViz();
        }

        public void UpdateCardStatViz()
        {
            handAttack.text = currentAttack.ToString();
            handHealth.text = currentHealth.ToString();
            handCost.text = currentCost.ToString();

            boardAttack.text = currentAttack.ToString();
            boardHealth.text = currentHealth.ToString();

            weaponAttack.text = currentAttack.ToString();
            weaponHealth.text = currentHealth.ToString();
        }

        public void SendToGraveyard()
        {
            Settings.SetParentForCard(this.gameObject.transform, Settings.gameManager.currentPlayer.currentHolder.graveyardHolder.value.transform);
            //Trigger deathrattles
            isDead = true;
        }

        public void SendToEnemyGraveyard()
        {
            Settings.SetParentForCard(this.gameObject.transform, Settings.gameManager.otherPlayer.currentHolder.graveyardHolder.value.transform);
            //Trigger deathrattles
            isDead = true;
        }

        public string GetOwner()
        {
            return ownerUsername;
        }

        public void SetOwner(string id)
        {
            ownerUsername = id;
        }

        public void SetBaseTags()
        {
            tags.Clear();
            foreach (CardVizProperties prop in this.viz.properties)
            {
                if (prop.tag != null)
                {
                    SetTag(prop.tag, true);
                }
            }
        }

        public void SetTag(string t, bool b)
        {
           bool hasTag =  this.viz.CheckTags(t);

           if (hasTag)
           {
               switch (t)
               {
                    case ("Charge"):
                        tags.Add(t,b);
                        break;         
                    // ------------------------//

                    case ("Battlecry"):
                        tags.Add(t, b);
                        break;
                    // ------------------------//

                    case ("Deathrattle"):
                        tags.Add(t, b);
                        break;
                    // ------------------------//

                    case ("DivineShield"):
                        tags.Add(t, b);
                        break;
                    // ------------------------//

                    case ("EndOfTurn"):
                        tags.Add(t, b);
                        break;
                    // ------------------------//

                    case ("Frozen"):
                        tags.Add(t, b);
                        break;
                    // ------------------------//

                    case ("Immune"):
                        tags.Add(t, b);
                        break;
                    // ------------------------//

                    case ("Inspire"):
                        tags.Add(t, b);
                        break;
                    // ------------------------//

                    case ("OnEvent"):
                        tags.Add(t, b);
                        break;
                    // ------------------------//

                    case ("Reborn"):
                        tags.Add(t, b);
                        break;
                    // ------------------------//

                    case ("Rush"):
                        tags.Add(t, b);
                        break;
                    // ------------------------//

                    case ("StartOfTurn"):
                        tags.Add(t, b);
                        break;
                    // ------------------------//

                    case ("Stealth"):
                        tags.Add(t, b);
                        break;
                    // ------------------------//

                    case ("Spellburst"):
                        tags.Add(t, b);
                        break;
                    // ------------------------//

                    case ("SpellsAndHeroPowers"):
                        tags.Add(t, b);
                        break;
                    // ------------------------//

                    case ("Taunt"):
                        tags.Add(t,b);
                        break;
                    // ------------------------//

                    case ("Windfury"):
                        tags.Add(t, b);
                        break;
                    // ------------------------//

                    case ("Beast"):
                        tags.Add(t, b);
                        break;
                    // ------------------------//
                    case ("Demon"):
                        tags.Add(t, b);
                        break;
                    // ------------------------//
                    case ("Dragon"):
                        tags.Add(t, b);
                        break;
                    // ------------------------//
                    case ("Murloc"):
                        tags.Add(t, b);
                        break;
                    // ------------------------//
                    case ("Mech"):
                        tags.Add(t, b);
                        break;
                    // ------------------------//
                    case ("Pirate"):
                        tags.Add(t, b);
                        break;
                    // ------------------------//
                    case ("Totem"):
                        tags.Add(t, b);
                        break;
                    // ------------------------//
                    case ("Elemental"):
                        tags.Add(t, b);
                        break;
                    // ------------------------//
                    case ("Undead"):
                        tags.Add(t, b);
                        break;
                    // ------------------------//

                    default:
                        Debug.Log("Something went wrong in the SetTag() method");
                        break;
               }
           }
        }

    }
}