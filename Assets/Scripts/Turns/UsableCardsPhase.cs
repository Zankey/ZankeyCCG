using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//checks to see if cards need to be woken up/unfrozen

namespace ZCCG
{
    [CreateAssetMenu(menuName = "Turns/Usable Cards Phase")]
    public class UsableCardsPhase : Phase
    {
        public Condition sleepingCardsCheck;

        public override bool IsComplete()
        {
            sleepingCardsCheck.Check();
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
