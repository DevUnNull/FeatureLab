using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T_EnemySpawner : MonoBehaviour
{
    [Header("Enemy")]
    public GameObject[] enemyPrefabs;          // Danh sách các enemy prefab
    private GameObject currentEnemyPrefab;     // Prefab hiện tại đang dùng để spawn
    public float spawnInterval = 2f;

    [Header("Gold")]
    public int gold;

    private BoardGenerator board;

    private void Start()
    {
        board = FindObjectOfType<BoardGenerator>();
        if (board == null)
        {
            Debug.LogError("Không tìm thấy BoardGenerator!");
            return;
        }

        // Chọn enemy đầu tiên ngẫu nhiên
        currentEnemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

        StartCoroutine(SpawnLoop());
        StartCoroutine(ChangeEnemyEvery5Seconds());
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    IEnumerator ChangeEnemyEvery5Seconds()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);

            // Random enemy mới (khác với cái cũ)
            GameObject newPrefab;
            do
            {
                newPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
            } while (newPrefab == currentEnemyPrefab && enemyPrefabs.Length > 1);

            currentEnemyPrefab = newPrefab;

            Debug.Log($"🔁 Đổi sang enemy mới: {currentEnemyPrefab.name}");
        }
    }

    void SpawnEnemy()
    {
        GameObject enemy = Instantiate(currentEnemyPrefab, transform.position, Quaternion.identity);

        EnemyHealth priceData = enemy.GetComponent<EnemyHealth>();
        gold = priceData.gold;

        List<Transform> path = board.GetBorderTiles();

        EnemyMovement movement = enemy.GetComponent<EnemyMovement>();
        movement.enabled = true;
        movement.SendMessage("SetWaypoints", path);
    }
}
