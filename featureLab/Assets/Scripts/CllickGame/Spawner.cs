using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject columnPrefab;         // Prefab cột
    public Transform spawnStartPoint;       // Vị trí spawn ban đầu
    public float spawnDelay = 2.5f;         // Thời gian giữa 2 lần spawn
    public float verticalOffset = 1.5f;     // Khoảng cách giữa 2 cột theo chiều dọc

    private Vector3 lastSpawnPosition;

    // Start is called before the first frame update
    void Start()
    {
        lastSpawnPosition = spawnStartPoint.position;
        SpawnColumn();
    }

    void SpawnColumn()
    {
        // Tạo cột mới tại vị trí mới, cao hơn cột trước
        Vector3 newSpawnPosition = new Vector3(lastSpawnPosition.x, lastSpawnPosition.y + verticalOffset, 0f);
        GameObject newColumn = Instantiate(columnPrefab, newSpawnPosition, Quaternion.identity);
        //Instantiate(original, position, rotation);  Quaternion.identity tương đương  = 0

        // Cập nhật vị trí cột vừa spawn
        lastSpawnPosition = newSpawnPosition;

        // Gọi lại sau thời gian delay
        Invoke("SpawnColumn", spawnDelay);
    }

}
