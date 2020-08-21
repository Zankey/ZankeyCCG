using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace ZCCG
{   
    public abstract class CardType : ScriptableObject 
    {
        public string typeName;
        public bool canAttack;
        //public typelogic logic

        public virtual void OnSetType(CardViz viz)
        {
            Element t = Settings.GetResourcesManager().typeElement;
            CardVizProperties instType = viz.GetProperty(t);   
            instType.type.text = typeName;
        }

        public bool TypeAllowsForAttack(CardInstance inst)
        {
            ///e.g. Flying Type can attack even if flatfooted / asleep
            ///bool r = logic.Execute(inst) -> if (inst.isAsleep)/inst.isAsleep = false   return true;
            /// 
            /// Charge -- same as above
            /// +
            /// Rush -- same but only minions for validTarget() method ------ not yet implemeted-----


            if (canAttack)
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