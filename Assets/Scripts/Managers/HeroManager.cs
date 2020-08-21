using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


namespace ZCCG
{
    public class HeroManager : MonoBehaviour, IClickable
    {
        public PlayerHolder player;
        public HeroClass hero;
        public GameObject heroPowerHolder;
        public GameObject heroPowerArtHolder;
        public GameObject heroPowerGemIcon;
        public GameObject portraitHolder;
        public GameObject swordsIcon;
        public GameObject shieldIcon;
        public Text userName;
        public TMP_Text health;
        public TMP_Text attack;
        public TMP_Text armor;
        public TMP_Text heroPowerCost;

        public SO.GameEvent onTargetSelection;
        public ZCCG.GameStates.State targetSelection;


        private void Start() 
        {
            SpriteRenderer spriteComponent = portraitHolder.GetComponent<SpriteRenderer>();
            spriteComponent.sprite = hero.GetPortrait();
            player.heroStatsUI = this;
            UpdateAll();
        }

        public void UpdateAll()
        {
            UpdateUser();
            UpdateAttack();
            UpdateHealth();
            UpdateArmor();
            UpdateHeroPowerCost(hero.GetHeroPowerCost());
        }

        public void UpdateUser()
        {
            userName.text = player.username;
            heroPowerCost.text = hero.GetHeroPowerCost().ToString();
            hero.SetHeroPowerHolder(heroPowerHolder);
        }

        public void UpdateHealth()
        {
            health.text = player.currentHealth.ToString();
        }

        public void UpdateAttack()
        {
            if (player.currentAttack > 0)
            {
                attack.text = player.currentAttack.ToString();
                swordsIcon.SetActive(true);
            }
            else
            {
                attack.text = "";
                swordsIcon.SetActive(false);
            }
        }

        public void UpdateArmor()
        {
            if (player.currentArmor > 0)
            {
                armor.text = player.currentArmor.ToString();
                shieldIcon.SetActive(false);
            }
            else
            {
                armor.text = "";
                shieldIcon.SetActive(false);
            }

        }

        public void UpdateHeroPowerCost(int i)
        {
            hero.SetHeroPowerCost(i);
            heroPowerCost.text = hero.GetHeroPowerCost().ToString();
        }

        public void OnClick()
        {
            Debug.Log("You clicked the hero manager for: " + player.username);
            if (player.HeroCanAttack())
            {
                Debug.Log("This hero can attack, select target");
                Settings.gameManager.currentSelectedHolder.ResetSelectedCard();
                Settings.gameManager.currentSelectedHolder.SetSelectedPlayer(player);
                Settings.gameManager.SetState(targetSelection);  //SelectCardToAttack Event Action
                onTargetSelection.Raise();
                
            }

        }

        public void OnHighlight()
        {
            Debug.Log("Highlighting hero :" + player.username + ". show emotes here");
        }
    }
}
