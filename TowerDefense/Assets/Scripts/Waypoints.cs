using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    public static Transform[] WPoints;

    private void Awake()
    {
        WPoints = new Transform[transform.childCount];
        for(int i = 0; i < WPoints.Length; i++)
        {
            WPoints[i] = transform.GetChild(i);
        }
    }
}
