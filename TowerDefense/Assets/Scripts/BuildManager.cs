using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildManager : MonoBehaviour
{
    [SerializeField] public GameObject BuildPlace;
    [SerializeField] public GameObject Tower;
    [SerializeField] private GameObject _buildMenu;
    
    public GameObject ExtraMenu;
    public List<GameObject> ListOfBuildOptions;

    [HideInInspector] public TowerData CurrentTowerData;
    [HideInInspector] public bool TowerIsBuilded = false;


    private Transform _target;
    private GameObject _nearestEnemy;
    private GameObject _ownAbility;
    private float _currCooldown;
    private bool _abilityUsed = false;

    private void Update()
    {
        if (CurrentTowerData != null)
        {
            //if (CurrentTowerData.CanShoot)
            //{
                if (AbleToShoot())
                    UpdateTarget();

                if (_currCooldown > 0)
                    _currCooldown -= Time.deltaTime;
            //}
            //else
            //{
            //    //if (!_abilityUsed && TowerIsBuilded)
            //    //{
            //    //    _ownAbility = CurrentTowerData.Ability;
            //    //    var ability = Instantiate(_ownAbility, transform);
            //    //    ability.transform.SetParent(transform, false);
            //    //    _abilityUsed = true;
            //    //    var garrisone = ability.GetComponent<WarriorGarrison>();
            //    //    var warriors = garrisone.AllWarriors;
            //    //    foreach(var warrior in warriors)
            //    //    {
            //    //        warrior.GetComponent<Warrior>()?.KillTarget(_target);
            //    //    }
            //    //}
            //}
            if (!CurrentTowerData.CanShoot)
            {
                if (!_abilityUsed && TowerIsBuilded)
                {
                    _ownAbility = CurrentTowerData.Ability;
                    var ability = Instantiate(_ownAbility, transform);
                    ability.transform.SetParent(transform, false);
                    _abilityUsed = true;
                    
                }
            }
        }

    }

    private bool AbleToShoot()
    {
        if (_currCooldown <= 0)
            return true;
        return false;
    }

    public void CheckBuildOptionIcons(BuildOptionSettings currentOption)
    {
        for (int i = 0; i < ListOfBuildOptions.Count; i++)
        {
            var buildOptionSettings = ListOfBuildOptions[i].GetComponent<BuildOptionSettings>();
            if(buildOptionSettings != currentOption)
            {
                buildOptionSettings.SetBuildOptions();
            }
        }
    }

    public void SellTower()
    {
        if (TowerIsBuilded)
        {
            Tower.GetComponent<Image>().enabled = false;
            ExtraMenu.SetActive(false);
            BuildPlace.SetActive(true);
            
            TowerIsBuilded = false;
            GameManager.Instance.MoneyAmount += CurrentTowerData.BuildPrice;
            Debug.LogWarning(GameManager.Instance.MoneyAmount);
            foreach (var option in ListOfBuildOptions)
            {
                option.GetComponent<BuildOptionSettings>().SetBuildOptions();
            }
        }
    }
    

    private void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float shortestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                _nearestEnemy = enemy;
            }
        }

        if (_nearestEnemy != null && shortestDistance <= CurrentTowerData.Range) 
        {
            _target = _nearestEnemy.transform;

            var enemy = _nearestEnemy.GetComponent<Enemy>();

            if (enemy != null && !enemy.IsDead)
            {
                if (CurrentTowerData.CanShoot)
                {
                    ShootAtTarget();
                }
                else
                {
                    var garrisone = GetComponentInChildren<WarriorGarrison>();
                    var warriors = garrisone.AllWarriors;
                    foreach (var warrior in warriors)
                    {
                        warrior.GetComponent<Warrior>()?.FindTarget(_target);
                    }
                }
            }
        }
        else
        {
            _target = null;
        }

    }

    private void ShootAtTarget()
    {
        _ownAbility = CurrentTowerData.Ability;
        if(_target != null)
        {
            var projectile = Instantiate(_ownAbility);
            projectile.transform.SetParent(transform, false);

            var mover = projectile.GetComponent<ProjectileMover>();

            if (mover != null)
            {
                mover.SetTarget(_target);
                mover.SetDamage(CurrentTowerData.MinDamage, CurrentTowerData.MaxDamage);
            }

            _currCooldown = CurrentTowerData.ShootInterval;
        }
    }    
}
