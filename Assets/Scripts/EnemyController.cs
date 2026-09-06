using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(HealthComponent))]
public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private float m_Speed = 3.0f;

    [SerializeField]
    private float m_StopY = 2.0f;

    [SerializeField]
    private Projectile m_ProjectilePrefab;

    [SerializeField]
    private Transform m_Muzzle;

    [SerializeField]
    private float m_FireRate = 10.0f;

    [SerializeField]
    private float m_MinFireDelay = 0.5f;

    [SerializeField]
    private float m_MaxFireDelay = 1.5f;

    private HealthComponent m_HealthComponent;
    private Rigidbody2D m_Rigidbody;
    private float m_NextFireTime;

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
        m_HealthComponent = GetComponent<HealthComponent>();
    }

    private void OnEnable()
    {
        m_HealthComponent.OnDamageTaken += OnDamageTaken;
        m_NextFireTime = Time.time + Random.Range(m_MinFireDelay, m_MaxFireDelay);
    }

    private void OnDisable()
    {
        m_HealthComponent.OnDamageTaken -= OnDamageTaken;
    }

    private void Update()
    {
        if (Time.time >= m_NextFireTime)
        {
            Fire();
        }
    }

    private void FixedUpdate()
    {
        Vector2 position = m_Rigidbody.position;
        float targetX = position.x;

        if (GameManager.Instance.TryGetPlayerPosition(out Vector2 playerPosition))
        {
            targetX = playerPosition.x;
        }

        Vector2 target = new Vector2(targetX, m_StopY);

        m_Rigidbody.MovePosition(Vector2.MoveTowards(position, target, m_Speed * Time.fixedDeltaTime));
    }

    private void Fire()
    {
        Instantiate(m_ProjectilePrefab, m_Muzzle.position, m_Muzzle.rotation);
        m_NextFireTime = Time.time + (1.0f / m_FireRate);
    }

    private void OnDamageTaken(int damageAmountTaken)
    {
        Debug.Log("Enemy received " + damageAmountTaken + " damage, new health: " + m_HealthComponent.Health);

        if (m_HealthComponent.Health <= 0)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
