using System;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField]
    private int m_MaxHealth = 100;
    private int m_Health;
    public int Health => m_Health;

    public event System.Action<int> OnDamageTaken;

    public void TakeDamage(int damageAmount)
    {
        if (m_Health > 0)
        {
            m_Health = Math.Clamp(m_Health - damageAmount, 0, m_MaxHealth);
            OnDamageTaken?.Invoke(damageAmount);
        }
    }

    public void AddHealth(int amount)
    {
        m_Health = Math.Clamp(m_Health + amount, 0, m_MaxHealth);
    }

    private void Awake()
    {
        m_Health = m_MaxHealth;
    }
}
