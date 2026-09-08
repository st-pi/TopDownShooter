using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyChaseMovement : MonoBehaviour
{
    [SerializeField]
    private float m_Speed = 3.0f;

    private Rigidbody2D m_Rigidbody;

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.TryGetPlayerPosition(out Vector2 playerPosition))
        {
            Vector2 position = m_Rigidbody.position;
            m_Rigidbody.MovePosition(Vector2.MoveTowards(position, playerPosition, m_Speed * Time.fixedDeltaTime));
        }
    }
}
