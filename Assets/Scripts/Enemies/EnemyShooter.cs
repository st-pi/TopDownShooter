using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    [SerializeField]
    private Projectile m_ProjectilePrefab;

    [SerializeField]
    private Transform m_Muzzle;

    [SerializeField]
    private float m_FireRate = 2.5f;

    [SerializeField]
    private float m_MinFireDelay = 0.5f;

    [SerializeField]
    private float m_MaxFireDelay = 1.5f;
    private float m_NextFireTime;

    private void OnEnable()
    {
        m_NextFireTime = Time.time + Random.Range(m_MinFireDelay, m_MaxFireDelay);
    }

    private void Update()
    {
        if (Time.time >= m_NextFireTime)
        {
            Fire();
        }
    }

    private void Fire()
    {
        Instantiate(m_ProjectilePrefab, m_Muzzle.position, m_Muzzle.rotation);
        m_NextFireTime = Time.time + (1.0f / m_FireRate);
    }
}
