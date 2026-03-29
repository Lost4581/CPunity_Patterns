using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 100f;
    private float _currentHealth;

    void Start()
    {
        _currentHealth = _maxHealth;
        GameEventManager.Instance?.Notify("PlayerDamaged", _currentHealth);
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        _currentHealth = Mathf.Max(0, _currentHealth);

        GameEventManager.Instance?.Notify("PlayerDamaged", _currentHealth);

        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Игрок умер!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}