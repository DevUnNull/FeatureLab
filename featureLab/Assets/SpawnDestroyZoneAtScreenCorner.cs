using UnityEngine;

public class SpawnDestroyZoneAtScreenCorner : MonoBehaviour
{
    public GameObject destroyTilePrefab;

    void Start()
    {
        SpawnDestroyZone();
    }

    void SpawnDestroyZone()
    {
        // Viewport (1, 0) là góc phải dưới cùng
        Vector3 screenBottomRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 10f)); // z = 10 để chắc chắn nằm trước camera
        screenBottomRight.z = 0f; // giữ z = 0 để nằm trên mặt 2D

        // Offset để không dính sát mép màn hình
        Vector3 spawnPos = screenBottomRight + new Vector3(-1f, 1f, 0); // lệch lên một chút cho đẹp

        GameObject zone = Instantiate(destroyTilePrefab, spawnPos, Quaternion.identity);
        zone.name = "DestroyZone";
        zone.tag = "DestroyZone";

        // Thêm collider trigger nếu chưa có
        if (zone.GetComponent<Collider2D>() == null)
        {
            var col = zone.AddComponent<BoxCollider2D>();
            col.isTrigger = true;
        }

        // Gắn script xử lý xóa tướng nếu chưa có
        if (zone.GetComponent<DestroyUnitTrigger>() == null)
        {
            zone.AddComponent<DestroyUnitTrigger>();
        }
    }
}
