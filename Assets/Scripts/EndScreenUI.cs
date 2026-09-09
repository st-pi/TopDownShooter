using TMPro;
using UnityEngine;

public class EndScreenUI : MonoBehaviour
{
    [SerializeField]
    private TMP_Text m_ScoreText;

    private void OnEnable()
    {
        m_ScoreText.text = $"Your final score: {GameManager.Instance.GameScore}";
    }
}
