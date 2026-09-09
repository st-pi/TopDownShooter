using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
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
        Rect view = Camera.main.GetWorldRect();

        float minX = view.xMin + m_BoundsPadding.x;
        float maxX = view.xMax - m_BoundsPadding.x;
        float minY = view.yMin + m_BoundsPadding.y;
        float maxY = view.yMax - m_BoundsPadding.y;

        return new Vector2(Mathf.Clamp(position.x, minX, maxX), Mathf.Clamp(position.y, minY, maxY));
    }
}
