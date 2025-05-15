using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private Vector3 offset;              // Khoảng cách từ điểm chuột đến tâm quân cờ khi bắt đầu kéo
    private bool isDragging = false;     // Biến kiểm tra đang kéo hay không
    private Vector2 previousPosition;    // Vị trí trước đó của quân cờ (để quay về nếu thả sai)

    private void Start()
    {
        // Ghi lại vị trí ban đầu khi game bắt đầu
        previousPosition = transform.position;
    }

    private void OnMouseDown()
    {
        // Lấy tọa độ chuột trong thế giới và tính offset so với vị trí quân cờ
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);//
        offset = transform.position - mouseWorld;//
        offset.z = 0; // Đảm bảo không thay đổi trục Z
        isDragging = true; // Bắt đầu kéo
    }

    private void OnMouseDrag()
    {
        // Nếu đang kéo thì cập nhật vị trí quân cờ theo chuột
        if (!isDragging) return;

        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 newPosition = mouseWorld + offset;
        newPosition.z = 0; // Giữ trục Z cố định
        transform.position = newPosition; // Cập nhật vị trí mới
    }

    private void OnMouseUp()
    {
        // Thả chuột => ngưng kéo
        isDragging = false;

        // Tìm tất cả Collider trong bán kính 0.5 quanh quân cờ (tile gần nhất)
        Collider2D[] nearbyColliders = Physics2D.OverlapCircleAll(transform.position, 0.5f);
        Transform closestTile = null;       // Tile gần nhất
        float shortestDistance = Mathf.Infinity; // Khoảng cách gần nhất khởi đầu là vô cực

        // Lặp qua từng collider
        foreach (var col in nearbyColliders)
        {
            // Chỉ xét những collider có tag là "Tile"
            if (!col.CompareTag("Tile")) continue;

            // Tính khoảng cách đến tile này
            float distance = Vector2.Distance(transform.position, col.transform.position);

            // Nếu tile này gần hơn tile trước đó thì lưu lại
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                closestTile = col.transform;
            }
        }

        // Nếu tìm được tile hợp lệ
        if (closestTile != null)
        {
            // Snap quân cờ về giữa tile
            transform.position = closestTile.position;

            // Ghi lại vị trí này là vị trí hợp lệ mới nhất
            previousPosition = closestTile.position;
        }
        else
        {
            // Nếu không thả vào tile nào, trả lại vị trí cũ
            Debug.Log("Thả ra ngoài bàn cờ.");
            transform.position = previousPosition;
        }
    }
}
