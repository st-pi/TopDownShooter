using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer m_BackgroundRenderer;

    [SerializeField]
    private SpriteRenderer m_FillRenderer;

    HealthComponent m_HealthComponent;

    private void Awake()
    {
        m_HealthComponent = GetComponentInParent<HealthComponent>();
    }

    private void OnEnable()
    {
        m_HealthComponent.OnDamageTaken += OnHealthChanged;
        Refresh();
    }

    private void OnDisable()
    {
        m_HealthComponent.OnDamageTaken -= OnHealthChanged;
    }

    private void OnHealthChanged(int damageAmountTaken)
    {
        Refresh();
    }

    private void Refresh()
    {
        int health = m_HealthComponent.Health;
        int maxHealth = m_HealthComponent.MaxHealth;
        bool visible = health > 0 && health < maxHealth;

        m_BackgroundRenderer.enabled = visible;
        m_FillRenderer.enabled = visible;

        if (visible)
        {
            Vector3 localScale = m_FillRenderer.transform.localScale;
            localScale.x = Mathf.Clamp01((float)m_HealthComponent.Health / m_HealthComponent.MaxHealth);
            m_FillRenderer.transform.localScale = localScale;
        }
    }
}
