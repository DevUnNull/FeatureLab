using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T_EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;             // Prefab của enemy
    public float spawnInterval = 2f;           // Khoảng thời gian giữa các lần spawn

    private BoardGenerator board;

    private void Start()
    {
        board = FindObjectOfType<BoardGenerator>(); // Tìm BoardGenerator trong Scene

        if (board == null)
        {
            Debug.LogError("Không tìm thấy BoardGenerator!");
            return;
        }

        StartCoroutine(SpawnLoopDelayed()); // Bắt đầu vòng lặp spawn
    }

    IEnumerator SpawnLoopDelayed()
    {
        // Đợi 1 frame để BoardGenerator khởi tạo xong
        yield return null;

        // Hoặc đợi 0.1 giây nếu cần chắc chắn
        // yield return new WaitForSeconds(0.1f);

        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnEnemy()
    {
        // Tạo enemy tại vị trí hiện tại của spawner
        GameObject enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);

        // Lấy đường đi từ board
        List<Transform> path = board.GetBorderTiles();

        // Gán đường đi cho enemy
        EnemyMovement movement = enemy.GetComponent<EnemyMovement>();
        movement.enabled = true;
        movement.SendMessage("SetWaypoints", path);
    }
}
