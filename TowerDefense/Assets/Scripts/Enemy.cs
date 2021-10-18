using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyData _enemyData;
    [SerializeField] private Animator _animator;
    [SerializeField] private HealthbarBehaviour _healthbarBehaviour;

    private int _currentHealthAmount;

    private float _speed;
    private int _waypointIndex = 0;
    private Transform _target;

    private void Start()
    {
        _speed = _enemyData.MovingSpeed;
        _target = Waypoints.WPoints[0];
        _animator.SetInteger("state", 1);
        _currentHealthAmount = _enemyData.HealthAmount;
       
    }

    private void Update()
    {
        _healthbarBehaviour.SetHealth(_currentHealthAmount, _enemyData.HealthAmount);
        CheckIsAlive();

        Vector3 dir = _target.position - transform.position;
        //float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        
        transform.Translate(dir.normalized * _speed * Time.deltaTime, Space.World);


        if(Vector3.Distance(transform.position, _target.position) <= 0.4f)
        {
            GetNextWaypoint();
        }
    }

    private void GetNextWaypoint()
    {
        if(_waypointIndex >= Waypoints.WPoints.Length - 1)
        {
            GameManager.Instance.HealthAmount--;
            Destroy(gameObject);
            return;
        }

        _waypointIndex++;
        _target = Waypoints.WPoints[_waypointIndex];
    }

    public void TakeDamage(int damage)
    {
        _currentHealthAmount -= damage;
    }
    
    private void CheckIsAlive()
    {
        if (_currentHealthAmount <= 0)
        {
            _animator.SetInteger("state", 0);
            _animator.SetInteger("state", 3);
            Destroy(gameObject, 0.5f);
            //GameManager.Instance.MoneyAmount ++
        }
    }
}
