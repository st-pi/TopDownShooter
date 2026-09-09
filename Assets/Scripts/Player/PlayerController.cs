using UnityEngine;

[RequireComponent(typeof(HealthComponent))]
public class PlayerController : MonoBehaviour
{
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
        Debug.Log("Player received " + damageAmountTaken + " damage, new health: " + m_HealthComponent.Health);

        if (m_HealthComponent.Health <= 0)
        {
            GameManager.Instance.FinishGame();
            Destroy(gameObject);
        }
    }
}
