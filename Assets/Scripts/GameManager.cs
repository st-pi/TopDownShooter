using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    private PlayerController m_PlayerPrefab;
    private PlayerController m_Player;

    [SerializeField]
    private EnemyController m_EnemyPrefab;
    private EnemyController m_Enemy;

    private void Init()
    {
        m_Player = Instantiate(m_PlayerPrefab);

        m_Enemy = Instantiate(m_EnemyPrefab);
        m_Enemy.transform.position += new Vector3(2.0f, 3.0f, 0.0f);
    }

    void Start()
    {
        Init();
    }

    void Update()
    {

    }
}
