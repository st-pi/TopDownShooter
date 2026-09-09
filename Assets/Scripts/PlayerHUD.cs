using TMPro;
using UnityEngine;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField]
    private Transform m_FillTransform;

    [SerializeField]
    private TMP_Text m_LifeText;

    [SerializeField]
    private TMP_Text m_ScoreText;

    private int m_DisplayedHealth = -1;

    private void OnEnable()
    {
        GameManager.Instance.OnScoreChanged += UpdateScore;
        UpdateScore(GameManager.Instance.GameScore);
    }

    private void OnDisable()
    {
        if (GameManager.Instance)
        {
            GameManager.Instance.OnScoreChanged -= UpdateScore;
        }

    }

    private void Update()
    {
        if (!GameManager.Instance.TryGetPlayerHealth(out int health, out int maxHealth))
        {
            return;
        }

        if (health == m_DisplayedHealth)
        {
            return;
        }

        var scale = m_FillTransform.localScale;
        scale.x = (float)health / maxHealth;
        m_FillTransform.localScale = scale;
        m_DisplayedHealth = health;
        m_LifeText.text = $"{health} / {maxHealth}";
    }

    private void UpdateScore(int newScore)
    {
        m_ScoreText.text = $"Score: {newScore}";
    }
}
