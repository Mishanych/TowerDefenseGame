using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;


public class ProjectileMover : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;
    private Transform _target;
    private int _projectileDamage;

    private void Update()
    {
        Move();
    }


    public void SetTarget(Transform enemy)
    {
        _target = enemy;
    }

    public void SetDamage(int minDamage, int maxDamage)
    {
        Random rnd = new Random();
        _projectileDamage = rnd.Next(minDamage, maxDamage + 1);
    }

    private void Move()
    {
        if(_target != null)
        {
            if (Vector2.Distance(transform.position, _target.position) <= 0.1f)
            {
                var enemy = _target.gameObject.GetComponent<Enemy>();
                enemy.TakeDamage(_projectileDamage);
                Destroy(gameObject);
            }
            else
            {
                Vector3 dir = _target.position - transform.position;
                transform.Translate(dir.normalized * _speed * Time.deltaTime, Space.World);

                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }

        }
        else
        {
            Destroy(gameObject);
        }
    }
}
