using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New PlayerWarriorData", menuName = "PlayerWarrior Data", order = 55)]
public class PlayerWarriorData : ScriptableObject
{
    [SerializeField] private string _warriorName;
    [SerializeField] private int _healthAmount;
    [SerializeField] private float _movingSpeed;
    [SerializeField] private float _minDamage;
    [SerializeField] private float _maxDamage;


    public string WarriorName
    {
        get
        {
            return _warriorName;
        }
    }

    public int HealthAmount
    {
        get
        {
            return _healthAmount;
        }
    }

    public float MovingSpeed
    {
        get
        {
            return _movingSpeed;
        }
    }

    public float MinDamage
    {
        get
        {
            return _minDamage;
        }
    }

    public float MaxDamage
    {
        get
        {
            return _maxDamage;
        }
    }
}
