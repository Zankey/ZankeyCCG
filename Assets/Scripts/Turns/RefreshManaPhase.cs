using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Mana is refreshed to max, and visuals updated. 
//Hero's hasAttacked field reset to false;

namespace ZCCG
{
    [CreateAssetMenu(menuName = "Turns/Refresh Mana Phase")]   
    public class RefreshManaPhase : Phase
    {

        public int turnIndex
        {
            get { return Settings.gameManager.turnIndex; }
        }
        
        public override bool IsComplete()
        {

            if(Settings.gameManager.turns[turnIndex].player.username == "Player_1")
            {
                if (Settings.manaManager.player_1_MaxMana < 10)
                {
                    Settings.manaManager.AddEmptyManaCrystal(); 
                }
            }
            if (Settings.gameManager.turns[turnIndex].player.username == "Player_2")
            {
                if (Settings.manaManager.player_2_MaxMana < 10)
                {
                    Settings.manaManager.AddEmptyManaCrystal();
                }
            }

            Settings.manaManager.RefreshManaCrystals();  //TODO in RefreshManaCrystals() handle overloaded cards

            Settings.manaManager.UpdateManaOverlay();

            Settings.gameManager.turns[turnIndex].player.hasAttacked = false;
            Settings.gameManager.turns[turnIndex].player.heroStatsUI.hero.SetHeroPowerUsed(false);
            Settings.gameManager.turns[turnIndex].player.heroStatsUI.heroPowerArtHolder.SetActive(true);
            Settings.gameManager.turns[turnIndex].player.heroStatsUI.heroPowerGemIcon.SetActive(true);
            Settings.gameManager.turns[turnIndex].player.heroStatsUI.heroPowerCost.gameObject.SetActive(true);

            return true;
        }

        public override void OnEndPhase()
        {
        }

        public override void OnStartPhase()
        {
        }
    }
}