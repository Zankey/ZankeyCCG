using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace ZCCG
{
    public class HeroPowerHolder : MonoBehaviour, IClickable
    {
        public PlayerHolder player;
        public HeroManager heroManagerHolder;
        public GameObject heroPowerArtHolder;
        public GameObject heroPowerGem;

        private void Start() 
        {
            player.powerHolder = this;
            SpriteRenderer spriteComponent = heroPowerArtHolder.GetComponent<SpriteRenderer>();
            spriteComponent.sprite = heroManagerHolder.hero.GetHeroPowerSprite();
        }


        public void OnClick()
        {
            if (heroManagerHolder.player.Equals(Settings.gameManager.currentPlayer))
            {
                heroManagerHolder.player.heroStatsUI.hero.HeroPower();
            }
        }

        public void OnHighlight()
        {
            if (heroManagerHolder.player.Equals(Settings.gameManager.currentPlayer))
            {
                heroManagerHolder.player.heroStatsUI.hero.HeroPowerDetails();
            }
        }
    }
}
