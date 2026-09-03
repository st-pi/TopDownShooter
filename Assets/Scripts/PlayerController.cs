using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private InputActionReference m_MoveAction;

    [SerializeField]
    private float m_Speed = 6.0f;

    [SerializeField]
    private Vector2 m_BoundsPadding = new Vector2(0.5f, 0.5f);

    private Rigidbody2D m_Rigidbody;
    private Vector2 m_MoveInput;

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        m_MoveAction.action.Enable();
    }

    private void OnDisable()
    {
        m_MoveAction.action.Disable();
    }

    private void Update()
    {
        m_MoveInput = m_MoveAction.action.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        Vector2 target = m_Rigidbody.position + (m_MoveInput * (m_Speed * Time.fixedDeltaTime));
        m_Rigidbody.MovePosition(ClampToView(target));
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
}
