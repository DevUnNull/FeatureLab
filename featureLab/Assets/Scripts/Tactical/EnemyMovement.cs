using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private List<Transform> waypoints;
    private int currentIndex = 0;
    public float speed = 2f;

    void Start()
    {
        // Tìm đối tượng BoardGenerator
        BoardGenerator board = FindObjectOfType<BoardGenerator>();
        if (board != null)
        {
            waypoints = board.GetBorderTiles();  // Lấy đường đi viền ngoài
        }
    }
    public void SetWaypoints(List<Transform> waypointsList)
    {
        waypoints = waypointsList;
        currentIndex = 0;
    }

    void Update()
    {
        if (waypoints == null || waypoints.Count == 0) return;

        Transform target = waypoints[currentIndex];
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target.position) < 0.01f)
        {
            currentIndex++;

            if (currentIndex >= waypoints.Count)
            {
                Destroy(gameObject); // Kết thúc vòng di chuyển
            }
        }
    }
}
