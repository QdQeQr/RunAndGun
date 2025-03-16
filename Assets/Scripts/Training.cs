using System;
using System.Collections.Generic;
using UnityEditor.U2D.Animation;
using UnityEngine;
using UnityEngine.Rendering;

namespace DefaultNamespace
{
    public class Training:MonoBehaviour

    {
        private void Start()
        {
            int ii = 0;
            List<int> cifri2and0 = new List<int>();
            for (int i = 5; i < 20; i++)
            {
                cifri2and0.Add(i);
            }
            for (int i = 0; i < cifri2and0.Count; i++)
            {
                print(cifri2and0[i]);
            }
        }
    }
}