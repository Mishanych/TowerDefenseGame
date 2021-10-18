using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New WaveData", menuName = "Wave Data", order = 54)]
public class WaveData : ScriptableObject
{
    [SerializeField] private float _waveDuration;
    [SerializeField] private List<EnemyInWaveInfo> _listOfEnemiesInWaveInfo;

    public float WaveDuration
    {
        get
        {
            return _waveDuration;
        }
    }

    public List<EnemyInWaveInfo> ListOfEnemiesInWaveInfo
    {
        get
        {
            return _listOfEnemiesInWaveInfo;
        }
    }
}

[Serializable]
public class EnemyInWaveInfo
{
    public GameObject Enemy;
    public int NumberOfEnemies;
    public int IndexOfSpawnPoint;
}
