using UnityEngine;

public class BoardGenerator : MonoBehaviour
{
    public GameObject tilePrefab;     // Prefab ô vuông
    public int rows = 8;              // Số hàng
    public int columns = 8;           // Số cột
    public float tileSize = 1.1f;     // Khoảng cách giữa các ô

    void Start()
    {
        GenerateBoard();
    }

    void GenerateBoard()
    {
        for (int x = 0; x < columns; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                Vector3 spawnPos = new Vector3(x * tileSize, y * tileSize, 0);
                GameObject tile = Instantiate(tilePrefab, spawnPos, Quaternion.identity);
                tile.transform.parent = this.transform;  // Gộp vào 1 GameObject cho dễ quản lý
                tile.name = $"Tile_{x}_{y}";
            }
        }

        // Center the board
        float boardWidth = columns * tileSize;
        float boardHeight = rows * tileSize;
        this.transform.position = new Vector3(-boardWidth / 2f + tileSize / 2f, -boardHeight / 2f + tileSize / 2f, 0);
    }
}
