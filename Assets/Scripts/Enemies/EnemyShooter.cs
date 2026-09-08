using UnityEngine;

public class EnemyShooter : Shooter
{
    [SerializeField]
    private float m_MinFireDelay = 0.5f;

    [SerializeField]
    private float m_MaxFireDelay = 1.5f;

    private void OnEnable()
    {
        DelayNextFire(Random.Range(m_MinFireDelay, m_MaxFireDelay));
    }
}
