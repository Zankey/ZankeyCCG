using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ZCCG.GameStates
{
    [CreateAssetMenu(menuName = "Actions/MouseOverDetection")]
    public class MouseOverDetection : Action
    {
        public override void Execute(float d)
        {
            
            // creates a list of everything you are mousing over, and lets you know if it has any of the IClickable methods (Edit: added all funtionality to Settings.cs)
            List<RaycastResult> results = Settings.GetUIObjs();

            IClickable c = null;

            foreach (RaycastResult r in results)
            {
                c = r.gameObject.GetComponentInParent<IClickable>();    
                if(c != null)                    
                {
                    c.OnHighlight();
                    break;
                }

            }
        }
    }
}
