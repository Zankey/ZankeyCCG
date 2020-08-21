using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ZCCG
{
    [CreateAssetMenu(menuName = "Heroes/Deathknight")]
    public class DeathknightHero : HeroClass
    {
        [System.NonSerialized]
        public GameObject heroPowerHolder;
        public GameObject cardPrefab;
        public Card dkGhoul;
        public GameElements.GE_Logic cardDownLogic;
        [SerializeField]
        public Sprite heroPowerSprite;
        [SerializeField]
        public Sprite heroPortrait;
        public int cost;
        [System.NonSerialized]
        public bool heroPowerUsed;

        private void OnEnable()
        {
            cost = 2;
            heroPowerUsed = false;
        }

        public override void HeroPower()
        {
            PlayerHolder p = Settings.gameManager.currentPlayer;
            int currentMana;
            if (p.username == "Player_1")
                currentMana = Settings.manaManager.player_1_CurrentMana;
            else
                currentMana = Settings.manaManager.player_2_CurrentMana;

            if (currentMana >= 2 && !heroPowerUsed && Settings.gameManager.currentPlayer.cardsDown.Count < 7)
            {   
                Settings.manaManager.PayManaCost(cost);
                GameObject go = Instantiate(cardPrefab) as GameObject;
                CardViz v = go.GetComponent<CardViz>();
                v.LoadCard(dkGhoul);
                CardInstance inst = go.GetComponent<CardInstance>();
                inst.SetOwner(Settings.gameManager.currentPlayer.username);
                inst.isAsleep = true;
                inst.currentLogic = cardDownLogic;
                Settings.gameManager.currentPlayer.cardsDown.Add(inst);
                Settings.SetParentForCard(go.transform, Settings.gameManager.currentPlayer.currentHolder.boardGrid.value.transform);
                heroPowerUsed = true;
                Settings.gameManager.currentPlayer.powerHolder.heroPowerArtHolder.SetActive(false);
                Settings.gameManager.currentPlayer.powerHolder.heroPowerGem.SetActive(false);
                Settings.gameManager.currentPlayer.heroStatsUI.heroPowerCost.gameObject.SetActive(false);
            }
            
            

        }

        public override void HeroPowerDetails()
        {
            Debug.Log("Hero Power // Army of the Dead // Cost:2 // Summons a 1/1 Undead Ghoul.");
        }

        public override void SetHeroPowerCost(int i)
        {
            cost = i;
        }

        public override int GetHeroPowerCost()
        {
            return cost;
        }

        public override Sprite GetHeroPowerSprite()
        {
            return heroPowerSprite;
        }

        public override Sprite GetPortrait()
        {
            return heroPortrait;
        }

        public override void SetHeroPowerHolder(GameObject go)
        {
            heroPowerHolder = go;
        }

        public override void SetHeroPowerUsed(bool b)
        {
            heroPowerUsed = b;
        }

        public override bool GetHeroPowerUsed()
        {
            return heroPowerUsed;
        }
    }
}
