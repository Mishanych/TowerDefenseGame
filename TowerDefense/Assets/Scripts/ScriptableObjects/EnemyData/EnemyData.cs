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
    [SerializeField] private int _minDeathReward;
    [SerializeField] private int _maxDeathReward;


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

    public int MinDeathReward
    {
        get
        {
            return _minDeathReward;
        }
    }

    public int MaxDeathReward
    {
        get
        {
            return _maxDeathReward;
        }
    }
}