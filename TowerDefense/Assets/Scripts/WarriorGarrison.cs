using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorGarrison : MonoBehaviour
{
    [SerializeField] private GameObject _commonWarrior;
    [SerializeField] private int _amountOfWarriors;
    [SerializeField] private float _distanseBetweenWarriors;

    [HideInInspector] public List<GameObject> AllWarriors;
    [HideInInspector] public List<Vector3> PatrolPoints;
    


    private void Start()
    {
        AllWarriors = new List<GameObject>();
        PatrolPoints = new List<Vector3>();
        FindPatrolPoint();
        InstantiateWarrior(_amountOfWarriors);
        
    }


    private void Update()
    {
        if (AllWarriors.Count != _amountOfWarriors)
        {
            InstantiateWarrior(_amountOfWarriors - AllWarriors.Count);
            //_amountOfWarriors++;
        }
    }


    private void InstantiateWarrior(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            var warrior = Instantiate(_commonWarrior, transform.parent);
            warrior.transform.SetParent(transform.parent.parent);
            AllWarriors.Add(warrior);

            Transform newTransform = transform;
            //newTransform.localPosition = 
            warrior.GetComponent<Warrior>().Target = PatrolPoints[i];

        }
    }


    private void FindPatrolPoint()
    {
        var minDistanse = Mathf.Infinity;
        var indexOfPoint = 0;
        for (int i = 0; i < Waypoints.WPoints.Length; i++)
        {
            var currDistanse = Vector3.Distance(transform.parent.localPosition, Waypoints.WPoints[i].localPosition);
            if(currDistanse < minDistanse)
            {
                minDistanse = currDistanse;
                indexOfPoint = i;
            }
        }
        var triangleCenter = Waypoints.WPoints[indexOfPoint].localPosition;

        FindTriangleVertex(triangleCenter, _distanseBetweenWarriors);
        

    }

    private void FindTriangleVertex(Vector3 center, float side)
    {
        var insideRadius = side * Mathf.Sqrt(3) / 6;
        var outsideRadius = side * Mathf.Sqrt(3) / 3;
        var pointA = new Vector3(center.x, center.y + outsideRadius, 0f);
        var pointB = new Vector3(center.x - (side / 2), center.y - insideRadius, 0f);
        var pointC = new Vector3(center.x + (side / 2), center.y - insideRadius, 0f);

        PatrolPoints.Add(pointA);
        PatrolPoints.Add(pointB);
        PatrolPoints.Add(pointC);
    }
}
