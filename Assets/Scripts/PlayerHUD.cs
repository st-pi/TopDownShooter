using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField]
    private Image m_HealthFill;

    [SerializeField]
    private TMP_Text m_LifeText;

    private int m_DisplayedHealth = -1;

    private void Update()
    {
        if (!GameManager.Instance.TryGetPlayerHealth(out int health, out int maxHealth))
        {
            return;
        }

        m_HealthFill.fillAmount = (float)health / maxHealth;

        if (health != m_DisplayedHealth)
        {
            m_DisplayedHealth = health;
            m_LifeText.text = $"{health} / {maxHealth}";
        }
    }
}
