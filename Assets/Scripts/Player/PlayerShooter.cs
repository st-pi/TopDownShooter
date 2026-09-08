using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooter : MonoBehaviour
{
    [SerializeField]
    private InputActionReference m_AttackAction;

    [SerializeField]
    private Projectile m_ProjectilePrefab;

    [SerializeField]
    private Transform m_Muzzle;

    [SerializeField]
    private float m_FireRate = 6.0f;

    private float m_NextFireTime;

    private void OnEnable()
    {
        m_AttackAction.action.Enable();
    }

    private void OnDisable()
    {
        m_AttackAction.action.Disable();
    }

    private void Update()
    {
        if (m_AttackAction.action.IsPressed() && Time.time >= m_NextFireTime)
        {
            Instantiate(m_ProjectilePrefab, m_Muzzle.position, m_Muzzle.rotation);
            m_NextFireTime = Time.time + (1.0f / m_FireRate);
        }
    }
}
