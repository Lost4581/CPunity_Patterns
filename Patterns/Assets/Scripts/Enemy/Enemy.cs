using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 50f;
    [SerializeField] private string _enemyType = "Grunt";
    [SerializeField] private float _attackDamage = 10f;
    [SerializeField] private float _attackRange = 2f;
    [SerializeField] private float _attackCooldown = 2f;

    private float _currentHealth;
    private float _lastAttackTime;

    void Start()
    {
        _currentHealth = _maxHealth;
    }

    void Update()
    {
        AttackPlayerIfNear();
    }

    private void AttackPlayerIfNear()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance <= _attackRange && Time.time > _lastAttackTime + _attackCooldown)
        {
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(_attackDamage);
                Debug.Log($"{name} атаковал игрока на {_attackDamage} урона!");
            }

            _lastAttackTime = Time.time;
        }
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        Debug.Log($"{name} получил {damage} урона. HP: {_currentHealth}");

        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        GameEventManager.Instance?.Notify("EnemyKilled", _enemyType);
        Destroy(gameObject);
    }
}