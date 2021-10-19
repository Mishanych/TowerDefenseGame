using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyData _enemyData;
    [SerializeField] private Animator _animator;
    [SerializeField] private HealthbarBehaviour _healthbarBehaviour;
    public bool IsDead = false;


    private int _currentHealthAmount;
    private float _speed;
    private int _waypointIndex = 0;
    private Transform _target;
    private bool _defenderIsNear = false;
    

    private void Start()
    {
        _speed = _enemyData.MovingSpeed;
        _target = Waypoints.WPoints[0];
        _animator.SetBool("isRunning", true);
        _currentHealthAmount = _enemyData.HealthAmount;
       
    }

    private void Update()
    {
        _healthbarBehaviour.SetHealth(_currentHealthAmount, _enemyData.HealthAmount);

        if (!IsDead)
            CheckIsAlive();

        Vector3 dir = _target.position - transform.position;
        //float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        if (!IsDead && !_defenderIsNear)
        {
            transform.Translate(dir.normalized * _speed * Time.deltaTime, Space.World);

            if (Vector3.Distance(transform.position, _target.position) <= 0.4f)
            {
                GetNextWaypoint();
            }
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
            IsDead = true;
            _animator.SetBool("isDying", true);
            _healthbarBehaviour.gameObject.SetActive(false);

            var rnd = new System.Random();
            var reward = rnd.Next(_enemyData.MinDeathReward, _enemyData.MaxDeathReward + 1);
            GameManager.Instance.MoneyAmount += reward;

            Destroy(gameObject, 1f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Defender")
        {
            Debug.LogWarning("DEFENDER");
            _defenderIsNear = true;
            
            var warrior = collision.gameObject.GetComponent<Warrior>();
            _animator.SetBool("isFighting", true);
            while (!IsDead && !warrior.IsDead)
            {
                var damage = Random.Range(_enemyData.MinDamage, _enemyData.MaxDamage + 1f);
                warrior.TakeDamage((int)damage);
            }
            _defenderIsNear = false;
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Defender")
        {
            Debug.LogWarning("DEFENDER");
            _defenderIsNear = true;

            var warrior = collision.gameObject.GetComponent<Warrior>();
            _animator.SetBool("isFighting", true);
            while (!IsDead && !warrior.IsDead)
            {
                var damage = Random.Range(_enemyData.MinDamage, _enemyData.MaxDamage + 1f);
                warrior.TakeDamage((int)damage);
            }
            _defenderIsNear = false;
        }
    }
}
