using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Boxfight2.Utilities
{
    public static class Utilities
    {
        static List<Transform> InstantiationPoints = new List<Transform>();
        public static Transform GetRandomInstantiatorPoint()
        {
            if (InstantiationPoints.Count < 1)
            {
                Debug.Log("[DEBUG] No InstantiationPoints cached at all! Finding the new ones and adding them ;3");
                Instantiator[] points = GameObject.FindObjectsOfType<Instantiator>();
                foreach (Instantiator insts in points)
                {
                    InstantiationPoints.Add(insts.gameObject.transform);
                }
            }

            return InstantiationPoints[Random.Range(0, InstantiationPoints.Count)];
        }
    }
}
