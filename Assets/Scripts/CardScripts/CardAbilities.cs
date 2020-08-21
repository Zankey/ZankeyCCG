using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZCCG
{
    public class CardAbilities : MonoBehaviour
    {
        CardInstance inst;

        private void Start() 
        {
            inst = this.GetComponent<CardInstance>();
        }

        // Ability return
        // public void ActivateAbility("Tag")
        // {
            //     switch("Tag")
            //     {
            //         case 1:
            //             Invoke(ability)
            //             break;
            //     }
            // }
    }
}