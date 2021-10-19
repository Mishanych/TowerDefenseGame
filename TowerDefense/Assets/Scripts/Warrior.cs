using UnityEngine;

public class Warrior : MonoBehaviour
{
    [SerializeField] private PlayerWarriorData _playerWarriorData;
    [SerializeField] private Animator _animator;
    [SerializeField] private HealthbarBehaviour _healthbarBehaviour;


    [HideInInspector] public Vector3 Target;
    [HideInInspector] public bool IsDead = false;
    private WarriorGarrison _warriorGarrison;
    

    private float _speed;
    private int _currentHealthAmount;
    
    private bool _targetReached = false;
    private bool _findToKill = false;
    private bool _enemyIsNear = false;
    private Enemy _enemy;


    void Awake()
    {
        _warriorGarrison = transform.parent.gameObject.GetComponent<WarriorGarrison>();
       // _target = _warriorGarrison.PatrolPoint;
        _speed = _playerWarriorData.MovingSpeed;
        _currentHealthAmount = _playerWarriorData.HealthAmount;
        Target = Vector3.positiveInfinity;
    }


    private void Update()
    {
        _healthbarBehaviour.SetHealth(_currentHealthAmount, _playerWarriorData.HealthAmount);

        if (!IsDead)
            CheckIsAlive();
        //if(!_targetReached)
            Move(Target);
    }

    private void Move(Vector3 target)
    {
        if (target != Vector3.positiveInfinity)
        {
            _animator.SetBool("isRunning", true);
            var enemy = gameObject.GetComponent<Enemy>();
            if (enemy == null)
            {
                if (Vector2.Distance(transform.localPosition, target) <= 0.1f)
                {

                    //Target = Vector3.positiveInfinity;
                    _targetReached = true;
                    //if(_findToKill)
                    //{
                    //    KillTarget();
                    //}
                    _animator.SetBool("isStaying", true); 
                    _animator.SetBool("isRunning", false);
                }
                else
                {
                    Vector3 dir = target - transform.localPosition;
                    transform.Translate(dir.normalized * _speed * Time.deltaTime);
                   
                }
            }
            else
            {
                if (Vector2.Distance(transform.position, target) <= 0.1f)
                {
                    _animator.SetBool("isFightning", true);

                    var value = (int)Random.Range(_playerWarriorData.MinDamage, _playerWarriorData.MaxDamage + 1);
                    enemy.TakeDamage(value);
                }
            }

        }
    }
    private void CheckIsAlive()
    {
        if (_currentHealthAmount <= 0)
        {
            IsDead = true;
            _animator.SetBool("isDying", true);
            _healthbarBehaviour.gameObject.SetActive(false);

            Destroy(gameObject, 1f);
            _warriorGarrison.AllWarriors.Remove(gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        _currentHealthAmount -= damage;
    }

    public void FindTarget(Transform target)
    {
        //_animator.SetBool("isRunning", true);

        //    Vector3 dir = target.localPosition - transform.localPosition;
        //    transform.Translate(dir.normalized * _speed * Time.deltaTime);
        //Target = target.localPosition;
        _targetReached = false;
        //_findToKill = true;
        //_enemy = target.gameObject.GetComponent<Enemy>();

    }
    //private void KillTarget()
    //{
        
    //    if (_enemy != null)
    //    {
    //        while (!IsDead && !_enemy.IsDead)
    //        {
    //            _animator.SetBool("isFighting", false);
    //            var damage = (int)Random.Range(_playerWarriorData.MinDamage, _playerWarriorData.MaxDamage + 1f);
    //            _enemy.TakeDamage(damage);
    //        }
    //        _findToKill = false;
    //    }
    //}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            _enemyIsNear = true;

            var enemy = collision.gameObject.GetComponent<Enemy>();
            _animator.SetBool("isFighting", true);
            while (!IsDead && !enemy.IsDead)
            {
                var damage = Random.Range(_playerWarriorData.MinDamage, _playerWarriorData.MaxDamage + 1f);
                enemy.TakeDamage((int)damage);
            }
            _enemyIsNear = false;
        }
    }
}
