using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float m_Speed = 12.0f;

    private Rigidbody2D m_Rigidbody;
    private SpriteRenderer m_Renderer;

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
        m_Renderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        m_Rigidbody.linearVelocity = transform.up * m_Speed;
    }

    private void FixedUpdate()
    {
        if (IsOutsideView())
        {
            Destroy(gameObject);
        }
    }

    private bool IsOutsideView()
    {
        Camera camera = Camera.main;
        float halfHeight = camera.orthographicSize;
        float halfWidth = halfHeight * camera.aspect;
        Vector2 center = camera.transform.position;
        Bounds bounds = m_Renderer.bounds;

        return bounds.max.x < center.x - halfWidth
            || bounds.min.x > center.x + halfWidth
            || bounds.max.y < center.y - halfHeight
            || bounds.min.y > center.y + halfHeight;
    }
}
