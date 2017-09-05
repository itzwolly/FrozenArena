using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DLLLibrary
{
    public class Coroutines
    {

        public static IEnumerator MoveTransformByVector(Transform pTransform, Action<GameObject> action,GameObject obj, Vector3 distance, float time)
        {
            Vector3 currentPos = pTransform.position;

            Vector3 newPos = currentPos + distance;
            float t = 0f;

            while (t < 1)
            {
                t += Time.deltaTime / time;
                pTransform.position = Vector3.Lerp(currentPos, newPos, t);
                yield return null;
            }
            action(obj);
        }
        public static IEnumerator MoveTransformByVector(Transform pTransform, Action action, Vector3 distance, float time)
        {
            Vector3 currentPos = pTransform.position;

            Vector3 newPos = currentPos + distance;
            float t = 0f;

            while (t < 1)
            {
                t += Time.deltaTime / time;
                pTransform.position = Vector3.Lerp(currentPos, newPos, t);
                yield return null;
            }
            action();
        }
        public static IEnumerator CallVoidAfterSeconds(Action action, float time)
        {
            yield return new WaitForSeconds(time);
            action();
        }
        public static IEnumerator CallVoidAfterSeconds(Action<GameObject> action, GameObject obj, float time)
        {
            yield return new WaitForSeconds(time);
            action(obj);
        }
    }

}

