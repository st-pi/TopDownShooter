using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField]
    private PlayerController m_PlayerPrefab;
    private PlayerController m_Player;

    private HealthComponent m_PlayerHealth;

    [SerializeField]
    private GameObject m_EndScreen;

    public bool TryGetPlayerHealth(out int health, out int maxHealth)
    {
        if (m_PlayerHealth == null)
        {
            health = 0;
            maxHealth = 0;
            return false;
        }

        health = m_PlayerHealth.Health;
        maxHealth = m_PlayerHealth.MaxHealth;
        return true;
    }

    public bool TryGetPlayerPosition(out Vector2 position)
    {
        if (m_Player == null)
        {
            position = Vector2.zero;
            return false;
        }

        position = m_Player.transform.position;
        return true;
    }

    private void Awake()
    {
        Instance = this;
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    private void Init()
    {
        m_Player = Instantiate(m_PlayerPrefab);
        m_PlayerHealth = m_Player.GetComponent<HealthComponent>();
    }

    private void Start()
    {
        Init();
    }

    public void FinishGame()
    {
        // temp, this is to dangerous
        Time.timeScale = 0.0f;
        m_EndScreen.SetActive(true);
    }

    public void RestartGame()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
