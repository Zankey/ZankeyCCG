using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZCCG
{
    [CreateAssetMenu(menuName = "Actions/Player Action/Reset Board Cards")]
    public class ResetBoardCards : PlayerAction
    {
        public override void Execute(PlayerHolder player)
        {
            foreach (CardInstance c in player.cardsDown)
            {
                if(c.viz.card.cardType is Weapon || c.viz.card.cardType is Spell)
                {
                    continue;
                }
                if(c.isAsleep)
                {
                    c.viz.asleep.gameObject.SetActive(false);
                    c.isAsleep = false;
                    c.hasAttacked = false;
                }
                c.isFrozen = false;
                c.viz.frozen.gameObject.SetActive(false);
            }
        }
    }
}
