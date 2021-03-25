using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ZCCG
{
    public class CurrentSelectedHolder : MonoBehaviour
    {
        public CardInstance currentSelectedCard;
        public CardInstance currentTargetCard;

        public PlayerHolder currentSelectedPlayer;
        public PlayerHolder currentTargetPlayer;
        

        bool isHeroSelected;
        bool isCardSelected;
        bool isHeroTarget;
        bool isCardTarget;

        private void Start() 
        {
            isHeroSelected = false;
            isCardSelected = false;
            isHeroTarget = false;
            isCardTarget = false;
            ResetAll();

        }

        // Selected

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


        // Targets


        public void SetTargetCard(CardInstance ci)
        {
            currentTargetCard = ci;
            ResetTargetPlayer();
            isCardTarget = true;
            isHeroTarget = false;
        }

        public void SetTargetPlayer(PlayerHolder cp)
        {
            currentTargetPlayer = cp;
            ResetTargetCard();
            isCardTarget = false;
            isHeroTarget = true;
        }

        public CardInstance GetTargetCard()
        {
            return currentTargetCard;
        }

        public PlayerHolder GetTargetPlayer()
        {
            return currentTargetPlayer;
        }

        public void ResetTargetCard()
        {
            currentTargetCard = null;
            isCardTarget = false;
        }

        public void ResetTargetPlayer()
        {
            currentTargetPlayer = null;
            isHeroTarget = false;
        }

        public void ResetAll()
        {
            ResetSelectedCard();
            ResetSelectedPlayer();
            ResetTargetCard();
            ResetTargetPlayer();
        }


    }
}