using UnityEngine;

[RequireComponent(typeof(HealthComponent))]
[RequireComponent(typeof(Rigidbody2D))]
public class EnemyController : MonoBehaviour
{
    public event System.Action<EnemyController> OnEnemyDied;

    [SerializeField]
    private float m_TurnSpeed = 180.0f;

    [SerializeField]
    private int m_ScoreWorth = 5;

    private HealthComponent m_HealthComponent;
    private Rigidbody2D m_Rigidbody;

    private void Awake()
    {
        m_HealthComponent = GetComponent<HealthComponent>();
        m_Rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        m_HealthComponent.OnDamageTaken += OnDamageTaken;
    }

    private void OnDisable()
    {
        m_HealthComponent.OnDamageTaken -= OnDamageTaken;
    }

    private void FixedUpdate()
    {
        if (!GameManager.Instance.TryGetPlayerPosition(out Vector2 playerPosition))
        {
            return;
        }

        Vector2 direction = playerPosition - m_Rigidbody.position;

        if (direction.sqrMagnitude < 0.0001f)
        {
            return;
        }

        float targetAngle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) + 90.0f;
        float angle = Mathf.MoveTowardsAngle(m_Rigidbody.rotation, targetAngle, m_TurnSpeed * Time.fixedDeltaTime);

        m_Rigidbody.MoveRotation(angle);
    }

    private void OnDamageTaken(int damageAmountTaken)
    {
        Debug.Log("Enemy received " + damageAmountTaken + " damage, new health: " + m_HealthComponent.Health);

        if (m_HealthComponent.Health <= 0)
        {
            GameManager.Instance.AddScore(m_ScoreWorth);
            Die();
        }
    }

    public void Die()
    {
        OnEnemyDied?.Invoke(this);
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
