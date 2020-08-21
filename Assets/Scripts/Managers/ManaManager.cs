using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace ZCCG
{   

    public class ManaManager : MonoBehaviour
    {
        public GameObject emptyPrefab;
        public GameObject filledPrefab;

        public TMP_Text player_1_TmpCurrentMana;
        public TMP_Text player_1_TmpMaxMana;
        public TMP_Text player_2_TmpCurrentMana;
        public TMP_Text player_2_TmpMaxMana;

        public int player_1_MaxMana;
        public int player_1_CurrentMana;
        public int player_2_MaxMana;
        public int player_2_CurrentMana;

        private int player_1_EmptyMana;
        private int player_2_EmptyMana;

        public int turnIndex
        {
            get { return Settings.gameManager.turnIndex; }
        }

        // still need to handle:

        // enemy mana

        // public int overloadedMana;

        private void Start() {
            Settings.manaManager = this;
            player_1_MaxMana = 0;
            player_1_CurrentMana = 0;
            player_1_EmptyMana = 0;
            player_2_MaxMana = 0;
            player_2_CurrentMana = 0;
            player_2_EmptyMana = 0;
        }

        // mana cost for playing cards
        public void PayManaCost(int cost)
        {
            Settings.manaManager.RemoveFilledManaCrystal(cost);
            Settings.manaManager.UpdateManaOverlay();
        }

        public void UpdateManaOverlay()
        {
            if (Settings.gameManager.turns[turnIndex].player.username == "Player_1")
            {
                player_1_TmpCurrentMana.text = player_1_CurrentMana.ToString();
                player_1_TmpMaxMana.text = player_1_MaxMana.ToString();
            }
            else
            {
                player_2_TmpCurrentMana.text = player_2_CurrentMana.ToString();
                player_2_TmpMaxMana.text = player_2_MaxMana.ToString();
            }    
        }

        public void RefreshManaCrystals()
        {
            if (Settings.gameManager.turns[turnIndex].player.username == "Player_1")
            {
                player_1_EmptyMana = player_1_MaxMana - player_1_CurrentMana;
                for (int i = 0; i < player_1_EmptyMana; i++) // fills only used mana
                {
                    AddFilledManaCrystal();
                }
                player_1_CurrentMana = player_1_MaxMana;
            }
            else
            {
                player_2_EmptyMana = player_2_MaxMana - player_2_CurrentMana;
                for (int i = 0; i < player_2_EmptyMana; i++) // fills only used mana
                {
                    AddFilledManaCrystal();
                }
                player_2_CurrentMana = player_2_MaxMana;
            }
        }

        public void AddEmptyManaCrystal()
        {
            GameObject empty = Instantiate(emptyPrefab) as GameObject;
            
            if (Settings.gameManager.turns[turnIndex].player.username == "Player_1")
            {
                Settings.SetParentForMana(empty.transform, Settings.gameManager.playerCardHolder.emptyManaGrid.value.transform);
                player_1_MaxMana++;
            }
            else
            {
                Settings.SetParentForMana(empty.transform, Settings.gameManager.otherCardHolder.emptyManaGrid.value.transform);
                player_2_MaxMana++;
            }
        }

        public void RemoveEmptyManaCrystal()
        {

            if (Settings.gameManager.turns[turnIndex].player.username == "Player_1")
            {
                Transform emptyManaChild = Settings.gameManager.playerCardHolder.emptyManaGrid.value.transform.GetChild(0);
                Destroy(emptyManaChild.gameObject);
                player_1_MaxMana--;
            }
            else
            {
                Transform emptyManaChild = Settings.gameManager.otherCardHolder.emptyManaGrid.value.transform.GetChild(0);
                Destroy(emptyManaChild.gameObject);
                player_2_MaxMana--;
            }
        }

        public void AddFilledManaCrystal()
        {
            GameObject filled = Instantiate(filledPrefab) as GameObject;

            if (Settings.gameManager.turns[turnIndex].player.username == "Player_1")
            {
                Settings.SetParentForMana(filled.transform, Settings.gameManager.playerCardHolder.filledManaGrid.value.transform);
                player_1_CurrentMana++;
            }
            else
            {
                Settings.SetParentForMana(filled.transform, Settings.gameManager.otherCardHolder.filledManaGrid.value.transform);
                player_2_CurrentMana++;
            }
        }

        public void RemoveFilledManaCrystal(int cost)
        {
            
                if (Settings.gameManager.turns[turnIndex].player.username == "Player_1")
                {
                    for (int i = 0; i < cost; i++)
                    {
                        Transform filledManaChild = Settings.gameManager.playerCardHolder.filledManaGrid.value.transform.GetChild(i);
                        Destroy(filledManaChild.gameObject);
                        player_1_CurrentMana--;
                    }  
                }
                else
                {
                    for (int i = 0; i < cost; i++)
                    {
                        Transform filledManaChild = Settings.gameManager.otherCardHolder.filledManaGrid.value.transform.GetChild(i);
                        Destroy(filledManaChild.gameObject);
                        player_2_CurrentMana--;
                    }
                }
            
        }

        
        // public void OverloadMana(int i)
        // {
        //     overloadedMana = i; 
        // }
    }
}