using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ZCCG.GameStates
{
    [CreateAssetMenu(menuName = "Actions/OnMouseClick")] 
    public class OnMouseClick : Action 
    {
        public override void Execute(float d)
        {
            if (Input.GetMouseButtonDown(0))
            {
                List<RaycastResult> results = Settings.GetUIObjs();

                foreach (RaycastResult r in results)
                {

                    CardInstance inst = r.gameObject.GetComponent<CardInstance>();
                    PlayerHolder p = Settings.gameManager.currentPlayer;
                    IClickable c = r.gameObject.GetComponentInParent<IClickable>();

                    if (c != null && !p.cardsDown.Contains(inst))
                    {
                        c.OnClick();
                        break;
                    }
                    if (p.cardsDown.Contains(inst))
                    {
                        if (inst.CanAttack())
                        {
                            Debug.Log("This can attack, -> SelectTarget() ");
                        }
                        else
                        {
                            Debug.Log("This card isAsleep = " + inst.isAsleep);
                        }
                    }
                }
            }
        }
    }
}
