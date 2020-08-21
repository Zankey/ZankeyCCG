using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ZCCG
{
    [CreateAssetMenu(menuName = "Turns/Enemy Turn Phase")]
    public class EnemyTurnPhase : Phase
    {
        public GameStates.State enemyControlState;

        public override bool IsComplete()
        {
            if (forceExit)
            {
                forceExit = false;
                return true;
            }

            return false;
        }

        public override void OnEndPhase()
        {
            if (isInit)
            {
                Settings.gameManager.SetState(null);
                isInit = false;
            }
        }

        public override void OnStartPhase()
        {
            if (!isInit)
            {
                Settings.gameManager.SetState(enemyControlState);
                Settings.gameManager.onPhaseChanged.Raise();
                isInit = true;
            }
        }
    }
}
