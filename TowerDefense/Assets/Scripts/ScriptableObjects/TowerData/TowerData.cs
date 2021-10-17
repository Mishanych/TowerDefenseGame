using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//used for displaying icons in build menu
[Serializable]
public class BuildOptionData
{
    public Sprite TowerIcon;
    public string BuildPriceText;
}


[CreateAssetMenu(fileName = "New TowerData", menuName = "Tower Data", order = 52)]

//used for displaying building
public class TowerData : ScriptableObject
{
    [SerializeField] private string _towerName;
    [SerializeField] private Sprite _towerImage;
    [SerializeField] private int _buildPrice;
    [SerializeField] private double _range;
    [SerializeField] private double _shootInterval;
    [SerializeField] private int _minDamage;
    [SerializeField] private int _maxDamage;
    public BuildOptionData BuildOptionData;


    public string TowerName
    {
        get
        {
            return _towerName;
        }
    }

    public Sprite TowerIcon
    {
        get
        {
            return _towerImage;
        }
    }

    public int BuildPrice
    {
        get
        {
            return _buildPrice;
        }
    }

    public double Range
    {
        get
        {
            return _range;
        }
    }

    public double ShootInterval
    {
        get
        {
            return _shootInterval;
        }
    }

    public int MinDamage
    {
        get
        {
            return _minDamage;
        }
    }
    
    public int MaxDamage
    {
        get
        {
            return _maxDamage;
        }
    }
}