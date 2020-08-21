using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZCCG
{

    [CreateAssetMenu(menuName = "Managers/Resource Manager")]
    public class ResourcesManager : ScriptableObject
    {
        public Element typeElement;

        public Card[] allCards;
        List<Card> cardsList = new List<Card>();

        public void Init()
        {
            cardsList.Clear();
            for (int i = 0; i < allCards.Length; i++)
            {
                cardsList.Add(allCards[i]);
            }
        }

        public Card GetCardInstance(Card c)
        {
            Card originalCard = GetCard(c);
            if (originalCard == null)
                return null;

            Card newInst = Instantiate(originalCard);
            newInst.name = originalCard.name;
            return newInst;    
        }

        Card GetCard(Card card)
        {
            if (cardsList.Contains(card))
                return card;
            else
            {
                Debug.Log("Card Does Not Exist");
                return null;
            }
        }
    }

}
