using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyHoverMovement : MonoBehaviour
{
    [SerializeField]
    private float m_Speed = 3.0f;

    [SerializeField]
    private float m_StopY = 2.0f;

    private Rigidbody2D m_Rigidbody;

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
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
}
