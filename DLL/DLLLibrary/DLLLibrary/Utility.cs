using System;
using System.Collections.Generic;
using UnityEngine;

namespace DLLLibrary
{
    public class Utility
    {
        public static GameObject RandomSelectFromList(List<GameObject> list)
        {
            System.Random rnd = new System.Random();
            return list[rnd.Next(0, list.Count)];
        }
    }
}
