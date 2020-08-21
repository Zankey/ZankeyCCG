using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZCCG
{
    public abstract class Condition : ScriptableObject
    {
        public abstract bool Check(); 
    }
}