using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyOrbitMovement : MonoBehaviour
{
    [SerializeField]
    private float m_Speed = 3.0f;

    [SerializeField]
    private float m_StrafeSpeed = 2.0f;

    [SerializeField]
    private float m_MinDistance = 3.0f;

    [SerializeField]
    private float m_MaxDistance = 6.0f;

    [SerializeField]
    private float m_DistanceTolerance = 0.3f;

    private Rigidbody2D m_Rigidbody;
    private float m_PreferredDistance;
    private float m_OrbitDirection;

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        m_PreferredDistance = Random.Range(m_MinDistance, m_MaxDistance);
        m_OrbitDirection = Random.value < 0.5f ? -1.0f : 1.0f;
    }

    private void FixedUpdate()
    {
        if (!GameManager.Instance.TryGetPlayerPosition(out Vector2 playerPosition))
        {
            return;
        }

        Vector2 position = m_Rigidbody.position;
        Vector2 toPlayer = playerPosition - position;
        float distance = toPlayer.magnitude;

        if (distance < 0.0001f)
        {
            return;
        }

        Vector2 direction = toPlayer / distance;
        float offset = distance - m_PreferredDistance;
        Vector2 velocity;

        if (Mathf.Abs(offset) > m_DistanceTolerance)
        {
            velocity = direction * (Mathf.Sign(offset) * m_Speed);
        }
        else
        {
            velocity = Vector2.Perpendicular(direction) * (m_OrbitDirection * m_StrafeSpeed);
        }

        m_Rigidbody.MovePosition(position + (velocity * Time.fixedDeltaTime));
    }
}
