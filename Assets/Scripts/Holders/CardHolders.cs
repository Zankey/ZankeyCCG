using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZCCG
{
    [CreateAssetMenu(menuName = "Holders/Card Holder")]
    public class CardHolders : ScriptableObject
    {
        public SO.TransformVariable handGrid;
        public SO.TransformVariable boardGrid;
        public SO.TransformVariable emptyManaGrid;
        public SO.TransformVariable filledManaGrid;
        public SO.TransformVariable weaponHolder;
        public SO.TransformVariable graveyardHolder;

        public void LoadPlayer(PlayerHolder p, HeroManager heroStats)
        {
            foreach (CardInstance c in p.cardsDown)
            {
                Settings.SetParentForCard(c.viz.gameObject.transform, boardGrid.value.transform);
            }

            foreach (CardInstance c in p.handcards)
            {
                Settings.SetParentForCard(c.viz.gameObject.transform, handGrid.value.transform);
            }

            heroStats = p.heroStatsUI;
        }
    }
}