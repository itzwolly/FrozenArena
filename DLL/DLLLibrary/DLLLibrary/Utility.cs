using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DLLLibrary
{
    public class Utility
    {
        public static GameObject RandomSelectFromList(List<GameObject> list)
        {
            System.Random rnd = new System.Random();
            if (list.Count > 0) {
                return list[rnd.Next(0, list.Count)];
            }
            return null;
        }

        public static void RestartLevel()
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }
}
