﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SO;

namespace SA
{
    public class AssignTransform : MonoBehaviour
    {
        public TransformVariable transformVariable;

		private void OnEnable() //changing between OnEnable() / Awake() / OnStat()
		{
			transformVariable.value = this.transform;
			Destroy(this);
		}

	}
}
