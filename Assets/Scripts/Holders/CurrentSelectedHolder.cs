using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ZCCG
{
    public class CurrentSelectedHolder : MonoBehaviour
    {
        public CardInstance currentSelectedCard;
        public PlayerHolder currentSelectedPlayer;

        bool isHeroSelected;
        bool isCardSelected;

        private void Start() 
        {
            isHeroSelected = false;
            isCardSelected = false;
            ResetSelectedCard();
            ResetSelectedPlayer();  
        }

        public void SetSelectedCard(CardInstance ci)
        {
            currentSelectedCard = ci;
            ResetSelectedPlayer();
            isCardSelected = true;
            isHeroSelected = false;
        }

        public void SetSelectedPlayer(PlayerHolder cp)
        {
            currentSelectedPlayer = cp;
            ResetSelectedCard();
            isCardSelected = false;
            isHeroSelected = true;
        }

        public CardInstance GetSelectedCard()
        {
            return currentSelectedCard;
        }

        public PlayerHolder GetSelectedPlayer()
        {
            return currentSelectedPlayer;
        }

        public void ResetSelectedCard()
        {
            currentSelectedCard = null;
            isCardSelected = false;
        }

        public void ResetSelectedPlayer()
        {
            currentSelectedPlayer = null;
            isHeroSelected = false;
        }


    }
}