using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    private PlayerController m_PlayerPrefab;
    private PlayerController m_Player;

    private void Init()
    {
        m_Player = Instantiate(m_PlayerPrefab);
    }

    void Start()
    {
        Init();
    }

    void Update()
    {

    }
}
