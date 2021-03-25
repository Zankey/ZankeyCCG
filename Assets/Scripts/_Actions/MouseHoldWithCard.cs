using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ZCCG.GameStates
{
    [CreateAssetMenu(menuName = "Actions/MouseHoldWithCard")]
    public class MouseHoldWithCard : Action
    {
        public CardInstance inst;
        public State playerControlState;

        public SO.GameEvent onPlayerControlState;
        
        //public ZCCG.GameStates.State abilityTargetSelection;

        public override void Execute(float d)
        {
            inst = Settings.gameManager.currentSelectedHolder.GetSelectedCard();

            bool mouseIsDown = Input.GetMouseButton(0);

            if (!mouseIsDown)
            {
                
                List<RaycastResult> results = Settings.GetUIObjs();

                foreach (RaycastResult r in results)
                {
                    GameElements.Area a = r.gameObject.GetComponentInParent<GameElements.Area>();
                    if (a != null)
                    {
                        a.OnDrop();
                        break;
                    }
                }

                inst.gameObject.SetActive(true);
                Settings.gameManager.currentSelectedHolder.ResetSelectedCard();
                Settings.gameManager.SetState(playerControlState);
                onPlayerControlState.Raise();
                return;
            }

        }

    }
}