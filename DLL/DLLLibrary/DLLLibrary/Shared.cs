using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.UI;
using UnityEngine;

namespace DLLLibrary
{
    public class Shared
    {
        public static void Select(Image obj)
        {
            Color col = obj.color;
            col.a = 1f;
            obj.color = col;
        }
        public static void Deselect(Image obj)
        {
            Color col = obj.color;
            col.a = 0.5f;
            obj.color = col;
        }
    }
}
