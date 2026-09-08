using UnityEngine;

[RequireComponent(typeof(HealthComponent))]
public class EnemyController : MonoBehaviour
{
    public event System.Action<EnemyController> OnEnemyDied;

    private HealthComponent m_HealthComponent;

    private void Awake()
    {
        m_HealthComponent = GetComponent<HealthComponent>();
    }

    private void OnEnable()
    {
        m_HealthComponent.OnDamageTaken += OnDamageTaken;
    }

    private void OnDisable()
    {
        m_HealthComponent.OnDamageTaken -= OnDamageTaken;
    }

    private void OnDamageTaken(int damageAmountTaken)
    {
        Debug.Log("Enemy received " + damageAmountTaken + " damage, new health: " + m_HealthComponent.Health);

        if (m_HealthComponent.Health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        OnEnemyDied?.Invoke(this);
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
