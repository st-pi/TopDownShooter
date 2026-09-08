using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooter : Shooter
{
    [SerializeField]
    private InputActionReference m_AttackAction;

    private void OnEnable()
    {
        m_AttackAction.action.Enable();
    }

    private void OnDisable()
    {
        m_AttackAction.action.Disable();
    }

    protected override void Update()
    {
        if (m_AttackAction.action.IsPressed())
        {
            TryToFire();
        }
    }
}
