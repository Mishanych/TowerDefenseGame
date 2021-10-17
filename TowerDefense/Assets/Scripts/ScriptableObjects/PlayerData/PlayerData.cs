using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New PlayerData", menuName = "Player Data", order = 51)]
public class PlayerData : ScriptableObject
{
    [SerializeField] private int _healthAmount;
    [SerializeField] private int _moneyAmount;
    [SerializeField] private int _enemyWaves;


    public int HealthAmount
    {
        get
        {
            return _healthAmount;
        }
    }

    public int MoneyAmount
    {
        get
        {
            return _moneyAmount;
        }
    }

    public int EnemyWaves
    {
        get
        {
            return _enemyWaves;
        }
    }
}
