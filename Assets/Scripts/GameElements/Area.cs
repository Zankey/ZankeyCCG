﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZCCG.GameElements
{
    public class Area : MonoBehaviour
    {
        public AreaLogic logic;
          
        public void OnDrop()
        {
            logic.Execute();
        }
    }
}