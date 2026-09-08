using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField]
    private Projectile m_ProjectilePrefab;

    [SerializeField]
    private Transform m_Muzzle;

    [SerializeField]
    private float m_FireRate = 2.5f;

    private float m_NextFireTime;

    public bool TryToFire()
    {
        if (Time.time < m_NextFireTime)
        {
            return false;
        }

        Instantiate(m_ProjectilePrefab, m_Muzzle.position, m_Muzzle.rotation);
        m_NextFireTime = Time.time + (1.0f / m_FireRate);

        return true;
    }

    protected void DelayNextFire(float delay)
    {
        m_NextFireTime = Time.time + delay;
    }

    protected virtual void Update()
    {
        TryToFire();
    }
}
