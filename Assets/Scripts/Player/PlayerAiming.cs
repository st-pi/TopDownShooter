using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAiming : MonoBehaviour
{
    [SerializeField]
    private InputActionReference m_AimAction;

    private void OnEnable()
    {
        m_AimAction.action.Enable();
    }

    private void OnDisable()
    {
        m_AimAction.action.Disable();
    }

    private void Update()
    {
        Vector2 screenPosition = m_AimAction.action.ReadValue<Vector2>();
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        Vector2 direction = worldPosition - (Vector2)transform.position;

        if (direction.sqrMagnitude < 0.0001f)
        {
            return;
        }

        transform.up = direction;
    }
}
