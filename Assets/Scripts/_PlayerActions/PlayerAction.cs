using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ZCCG
{
    public abstract class PlayerAction : ScriptableObject
    {
        public abstract void Execute(PlayerHolder player);
    }
}