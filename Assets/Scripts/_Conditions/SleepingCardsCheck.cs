using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// checks the sleeping status
// TODO:  checks other turn status like frozen

namespace ZCCG.GameStates
{
    [CreateAssetMenu(menuName = "Condition/Sleeping Cards Check")]
    public class SleepingCardsCheck : Condition
    {   
        public override bool Check()      
        {

            GameManager gm = GameManager.singleton;
            PlayerHolder p = gm.currentPlayer;

            int count = p.cardsDown.Count;
            for (int i = 0; i < p.cardsDown.Count; i++)
            {
                if(p.cardsDown[i].isAsleep)
                    count--;
            }
            
            if(count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
