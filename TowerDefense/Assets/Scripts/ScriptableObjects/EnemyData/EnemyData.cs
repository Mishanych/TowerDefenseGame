using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New EnemyData", menuName = "Enemy Data", order = 53)]
public class EnemyData : ScriptableObject
{
    [SerializeField] private string _enemyName;
    [SerializeField] private int _healthAmount;
    [SerializeField] private float _movingSpeed;
    [SerializeField] private float _minDamage;
    [SerializeField] private float _maxDamage;


    public string EnemyName
    {
        get
        {
            return _enemyName;
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

    public double MinDamage
    {
        get
        {
            return _minDamage;
        }
    }

    public double MaxDamage
    {
        get
        {
            return _maxDamage;
        }
    }
}