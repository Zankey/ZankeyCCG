using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZCCG.GameElements;

namespace ZCCG
{
    [CreateAssetMenu(menuName = "Holders/Player Holder")]
    public class PlayerHolder : ScriptableObject
    {
        public string username;
        public Sprite playerPortrait;
        public Color playerColor;
        [System.NonSerialized]
        public int maxHealth = 30;
        [System.NonSerialized]
        public int currentHealth = 30;
        [System.NonSerialized]
        public int currentAttack = 0;
        [System.NonSerialized]
        public int currentArmor = 0;
        [System.NonSerialized]
        public int currentSpellDamage = 0;
        [System.NonSerialized]
        public int fatigueCount = 1;
        [System.NonSerialized]
        public HeroManager heroStatsUI;
        [System.NonSerialized]
        public HeroPowerHolder powerHolder;

        public Card[] startingDeck;

        public bool isHumanPlayer;

        [System.NonSerialized]
        public bool hasAttacked = false;
        [System.NonSerialized]
        public bool isStealth = false;
        [System.NonSerialized]
        public bool isSpellsAndHeroPowers = false;

        public GE_Logic handLogic;
        public GE_Logic boardLogic;

        [System.NonSerialized]
        public CardHolders currentHolder;

        [System.NonSerialized]
        public List<CardInstance> handcards = new List<CardInstance>();
        [System.NonSerialized]
        public List<CardInstance> cardsDown = new List<CardInstance>();
        [System.NonSerialized]
        public List<CardInstance> graveyard = new List<CardInstance>();
        [System.NonSerialized]
        public List<CardInstance> deck = new List<CardInstance>();

        public int playerMinionCount
        {
            get { return currentHolder.boardGrid.value.GetComponentsInChildren<CardViz>().Length; }
        }

        public GameManager gm
        {
            get { return Settings.gameManager; }
        }

        public void DropCard(CardInstance inst) 
        {
            if(handcards.Contains(inst))
            {
                handcards.Remove(inst);
                Debug.Log("Removing hand card inst");
            }
            if(inst.viz.card.cardType is Weapon)
            {
                Debug.Log("dropping a weapon");
                return;
            }
            else
                cardsDown.Add(inst);
                Debug.Log("Minion added to cardsDown");
            
        }

        public bool CanUseCard(Card c)
        {
            
            bool result = false;

            int currentMana;
            if(gm.currentPlayer.username == "Player_1")
            {
                currentMana = Settings.manaManager.player_1_CurrentMana;
            }
            else
            {
                currentMana = Settings.manaManager.player_2_CurrentMana;
            }

            if (c.cardType is Weapon || c.cardType is Spell)           
            {
                if(c.cost <= currentMana)
                {
                    result = true;
                }
            }
            else
            {
                if (c.cardType is Minion && playerMinionCount < 7)        // Handles max Minions on board
                {
                    if (c.cost <= currentMana)
                    {
                        result = true;
                    }
                }
                else
                {
                    Settings.RegisterEvent("Too Many Minions!", Color.red);
                }
            }
            return result;
        }

        public void SubtractHeroCurrentHealth(int damage)
        {

            int damageAfterArmor = damage - currentArmor;

            if (damageAfterArmor >= 0)
            {
                SubtractHeroArmor(damage);
                currentHealth -= damageAfterArmor;
            }

            if (damageAfterArmor < 0)
                SubtractHeroArmor(damage);
            

            if(heroStatsUI != null)
                heroStatsUI.UpdateHealth();
        }

        public void AddHeroCurrentHealth(int heal)
        {
            currentHealth += heal;
            // int overhealing = currentHealth - maxHealth
            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }

            if (heroStatsUI != null)
                heroStatsUI.UpdateHealth();
            //possibly return overhealing sometime
        }

        public void AddHeroMaxHealth(int health)
        {
            maxHealth += health;
        }

        public void SubtractHeroMaxHealth(int health)
        {
            maxHealth -= health;
            if (currentHealth > maxHealth)
                currentHealth = maxHealth;

            if (heroStatsUI != null)
                heroStatsUI.UpdateHealth();
        }

        public void AddHeroAttack(int atk)
        {
            currentAttack += atk;

            if (heroStatsUI != null)
                heroStatsUI.UpdateAttack();
            
        }

        public void SubtractHeroAttack(int atk)
        {
            currentAttack -= atk;
            if (currentAttack < 0)
                currentAttack = 0;

            if (heroStatsUI != null)
                heroStatsUI.UpdateAttack();
        }

        public void AddHeroSpellDamage(int sd)
        {
            currentSpellDamage += sd;

            if (heroStatsUI != null)
                heroStatsUI.UpdateAttack();

        }

        public void SubtractHeroSpellDamage(int sd)
        {
            currentSpellDamage -= sd;
            if (currentSpellDamage < 0)
                currentSpellDamage = 0;

            if (heroStatsUI != null)
                heroStatsUI.UpdateAttack();
        }

        public void AddHeroArmor(int arm)
        {
            currentArmor += arm;

            if (heroStatsUI != null)
                heroStatsUI.UpdateArmor();

        }

        public void SubtractHeroArmor(int arm)
        {
            currentArmor -= arm;

            if (currentArmor < 0)
                currentArmor = 0;

            if (heroStatsUI != null)
                heroStatsUI.UpdateArmor();

        }

        public bool HeroCanAttack()
        {
            bool result = false;

            if (gm.currentPlayer == this)
            {
                if (currentAttack > 0 && !hasAttacked)
                {
                    result = true;
                }

                if (currentAttack > 0 && hasAttacked)
                {
                    Debug.Log("This hero has already attacked");
                }
            }

            return result;
        }


    }
}