using UnityEngine;
using UnityEngine.Tilemaps;

public class DragAndDrop : MonoBehaviour
{
    // keo tha
    private Vector3 offset;              // Khoảng cách từ điểm chuột đến tâm quân cờ khi bắt đầu kéo
    private bool isDragging = false;     // Biến kiểm tra đang kéo hay không
    private Vector2 previousPosition;    // Vị trí trước đó của quân cờ (để quay về nếu thả sai)

    // mua
    private bool isPlacedOnBoard = false;
    public bool isBuy = true;
    private Transform originalParent;

    //mua tru tien
    private PricePlayer priceData;
    private int price;

    private void Start()
    {
        // Ghi lại vị trí ban đầu khi game bắt đầu
        previousPosition = transform.position;
        priceData = GetComponent<PricePlayer>(); // lấy script gắn trên prefab
        price = priceData != null ? priceData.price : 0; // nếu không có thì giá = 0
    }

    private void OnMouseDown()
    {
        GetComponent<Animator>().enabled = false;
        originalParent = transform.parent;
        transform.SetParent(null); // tách khỏi slot để không bị reset



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

        if (closestTile != null)
        {
            // Nếu chưa mua và không đủ tiền → từ chối mua
            if (!isBuy && !GoldManager.Instance.HasEnoughGold(price))
            {
                Debug.Log("❌ Không đủ tiền mua " + gameObject.name + ", cần " + price);
                transform.position = previousPosition; // trở về vị trí cũ
                return; // ❗ THOÁT LUÔN → không đặt lên bàn
            }

            // ✅ Chỉ vào đây khi đủ tiền hoặc đã mua
            transform.position = closestTile.position;
            previousPosition = closestTile.position;
            isPlacedOnBoard = true;

            if (!isBuy)
            {
                isBuy = true;
                GoldManager.Instance.SpendGold(price);
            }

            GetComponent<Animator>().enabled = true;
        }

        else
        {
            if (!isPlacedOnBoard)
            {
                // Nếu chưa từng được đặt vào bàn thì xoá (ví dụ mới kéo ra từ shop)
                Debug.Log("Kéo ra nhưng không thả vào bàn → huỷ");
                transform.position = previousPosition;
            }
            else
            {
                // Đã từng được đặt → trả về vị trí gần nhất
                transform.position = previousPosition;
            }
        }

    }


}
