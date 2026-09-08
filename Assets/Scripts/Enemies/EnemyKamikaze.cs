using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(EnemyController))]
public class EnemyKamikaze : MonoBehaviour
{
    [SerializeField]
    private int m_DamageOnExplosion = 5;

    [SerializeField]
    private ParticleSystem m_ImpactEffect;

    [SerializeField]
    private LayerMask m_TargetLayer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((m_TargetLayer.value & (1 << collision.gameObject.layer)) == 0)
        {
            return;
        }

        if (!collision.TryGetComponent(out HealthComponent otherHealth))
        {
            return;
        }

        otherHealth.TakeDamage(m_DamageOnExplosion);
        Instantiate(m_ImpactEffect, transform.position, transform.rotation);

        GetComponent<EnemyController>().Die();
    }
}
