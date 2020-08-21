using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZCCG
{

    public class CurrentSelected : MonoBehaviour
    {
        public CardInstance currentCard;
        public CardViz cardViz;

        Transform mTransform;

        public void LoadCard()
        {
            currentCard = Settings.gameManager.currentSelectedHolder.GetSelectedCard();

            if (currentCard == null)
                return;

            currentCard.gameObject.SetActive(true);
            cardViz.LoadCard(currentCard.viz.card);
            cardViz.gameObject.SetActive(true);
        }

        public void CloseCard()
        {
            cardViz.gameObject.SetActive(false);
        }

        private void Start() 
        {
            mTransform = this.transform;
        }

        void Update() 
        {  
            mTransform.position = Input.mousePosition;
        }

    }
}