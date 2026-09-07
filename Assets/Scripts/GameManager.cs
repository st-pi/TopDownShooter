using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField]
    private PlayerController m_PlayerPrefab;
    private PlayerController m_Player;

    [SerializeField]
    private EnemyController m_EnemyPrefab;
    private EnemyController m_Enemy;

    private HealthComponent m_PlayerHealth;

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

        m_Enemy = Instantiate(m_EnemyPrefab);
        m_Enemy.transform.position += new Vector3(2.0f, 6.5f, 0.0f);
    }

    void Start()
    {
        Init();
    }

    void Update()
    {

    }
}
