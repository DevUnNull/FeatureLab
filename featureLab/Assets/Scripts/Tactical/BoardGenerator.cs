using UnityEngine;
using System.Collections.Generic;

public class BoardGenerator : MonoBehaviour
{
    public GameObject tilePrefab;
    public int rows = 8;
    public int columns = 8;
    public float tileSize = 1.1f;

    private GameObject[,] tiles;  // Lưu lại toàn bộ tile

    void Start()
    {
        GenerateBoard();
    }



    void GenerateBoard()
    {
        tiles = new GameObject[columns, rows];

        for (int x = 0; x < columns; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                Vector3 spawnPos = new Vector3(x * tileSize, y * tileSize, 0);
                GameObject tile = Instantiate(tilePrefab, spawnPos, Quaternion.identity);
                tile.transform.parent = this.transform;
                tile.name = $"Tile_{x}_{y}";

                tiles[x, y] = tile;
            }
        }

        // Center the board
        float boardWidth = columns * tileSize;
        float boardHeight = rows * tileSize;
        this.transform.position = new Vector3(-boardWidth / 2f + tileSize / 2f, -boardHeight / 2f + tileSize / 2f, 0);
    }

    // ✅ Trả về danh sách các tile viền ngoài (theo chiều kim đồng hồ)
    public List<Transform> GetBorderTiles()
    {
        List<Transform> borderTiles = new List<Transform>();

        // Trên cùng: left → right
        for (int x = 0; x < columns; x++)
            borderTiles.Add(tiles[x, rows - 1].transform);

        // Phải: top → bottom
        for (int y = rows - 2; y >= 0; y--)
            borderTiles.Add(tiles[columns - 1, y].transform);

        // Dưới cùng: right → left
        for (int x = columns - 2; x >= 0; x--)
            borderTiles.Add(tiles[x, 0].transform);

        // Trái: bottom → top (bỏ tile đầu vì đã có)
        for (int y = 1; y < rows - 1; y++)
            borderTiles.Add(tiles[0, y].transform);

        return borderTiles;
    }

}
