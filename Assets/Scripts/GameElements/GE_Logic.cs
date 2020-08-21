using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZCCG.GameElements
{
    public abstract class GE_Logic : ScriptableObject
    {
        public abstract void OnClick(CardInstance inst);

        public abstract void OnHighlight(CardInstance inst);
    }
}