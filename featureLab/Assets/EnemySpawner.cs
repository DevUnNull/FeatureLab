using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float speed = 3f;            // Tốc độ di chuyển
    public float endXRight = 10f;            // vị trí dừng lại 
    public float endXLeft = 1f;
    private Vector3 startPosition;

    void Start()
    {
        // Lưu vị trí ban đầu để reset lại
        startPosition = transform.position;
    }

    void Update()
    {
        if (transform.position.x < endXRight)
        {
            // Di chuyển sang phải mỗi frame
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }

    }
}
