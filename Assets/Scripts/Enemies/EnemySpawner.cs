using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private enum SpawnSide
    {
        Top,
        Bottom,
        Left,
        Right,
    }

    private static readonly int s_SpawnSideCount = System.Enum.GetValues(typeof(SpawnSide)).Length;

    [SerializeField]
    private List<EnemyController> m_EnemyPrefabs;

    [SerializeField]
    private float m_SpawnInterval = 2.0f;

    [SerializeField]
    private int m_MaxEnemies = 8;

    [SerializeField]
    private float m_SpawnMargin = 1.5f;

    private readonly List<EnemyController> m_Enemies = new List<EnemyController>();
    private float m_NextSpawnTime;

    private void Update()
    {
        if (Time.time < m_NextSpawnTime || m_Enemies.Count >= m_MaxEnemies)
        {
            return;
        }

        Spawn();
        m_NextSpawnTime = Time.time + m_SpawnInterval;
    }

    private void Spawn()
    {
        EnemyController prefab = m_EnemyPrefabs[Random.Range(0, m_EnemyPrefabs.Count)];
        EnemyController enemy = Instantiate(prefab, GetSpawnPosition(), Quaternion.identity);

        enemy.OnEnemyDied += RemoveEnemy;
        m_Enemies.Add(enemy);
    }

    private void RemoveEnemy(EnemyController enemy)
    {
        enemy.OnEnemyDied -= RemoveEnemy;
        m_Enemies.Remove(enemy);
    }

    private Vector2 GetSpawnPosition()
    {
        Rect view = Camera.main.GetWorldRect();
        Vector2 center = view.center;

        float outerX = (view.width * 0.5f) + m_SpawnMargin;
        float outerY = (view.height * 0.5f) + m_SpawnMargin;

        SpawnSide side = (SpawnSide)Random.Range(0, s_SpawnSideCount);

        return side switch
        {
            SpawnSide.Top => new Vector2(center.x + Random.Range(-outerX, outerX), center.y + outerY),
            SpawnSide.Bottom => new Vector2(center.x + Random.Range(-outerX, outerX), center.y - outerY),
            SpawnSide.Left => new Vector2(center.x - outerX, center.y + Random.Range(-outerY, outerY)),
            //SpawnSide.Right
            _ => new Vector2(center.x + outerX, center.y + Random.Range(-outerY, outerY))
        };
    }
}
