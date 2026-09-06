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
