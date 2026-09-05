using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(HealthComponent))]
public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private float m_Speed = 6.0f;

    [SerializeField]
    private Vector2 m_BoundsPadding = new Vector2(0.5f, 0.5f);

    [SerializeField]
    private Projectile m_ProjectilePrefab;

    [SerializeField]
    private Transform m_Muzzle;

    [SerializeField]
    private float m_FireRate = 10.0f;

    private HealthComponent m_HealthComponent;
    private Rigidbody2D m_Rigidbody;
    // private Vector2 m_MoveInput;
    private float m_NextFireTime;

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
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

    private void Update()
    {
        if (Time.time >= m_NextFireTime)
        {
            Fire();
        }
    }

    private void FixedUpdate()
    {
        // Vector2 target = m_Rigidbody.position + (m_MoveInput * (m_Speed * Time.fixedDeltaTime));
        // m_Rigidbody.MovePosition(ClampToView(target));
    }

    private void Fire()
    {
        Instantiate(m_ProjectilePrefab, m_Muzzle.position, m_Muzzle.rotation);
        m_NextFireTime = Time.time + (1.0f / m_FireRate);
    }

    private Vector2 ClampToView(Vector2 position)
    {
        Camera camera = Camera.main;
        float halfHeight = camera.orthographicSize;
        float halfWidth = halfHeight * camera.aspect;
        Vector2 center = camera.transform.position;

        float minX = center.x - halfWidth + m_BoundsPadding.x;
        float maxX = center.x + halfWidth - m_BoundsPadding.x;
        float minY = center.y - halfHeight + m_BoundsPadding.y;
        float maxY = center.y + halfHeight - m_BoundsPadding.y;

        return new Vector2(Mathf.Clamp(position.x, minX, maxX), Mathf.Clamp(position.y, minY, maxY));
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
