using UnityEngine;

public class ClickGame_Jump : MonoBehaviour
{
    public float jumpForce = 5f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float rayLength = 0.2f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Kiểm tra nếu đang đứng trên mặt đất bằng Raycast
        bool isGrounded = Physics2D.Raycast(groundCheck.position, Vector2.down, rayLength, groundLayer);

        // Vẽ tia ray trong Scene để dễ debug
        Debug.DrawRay(groundCheck.position, Vector2.down * rayLength, Color.red);

        // Chỉ nhảy nếu chạm đất
        if ((Input.GetKeyDown(KeyCode.Space) && isGrounded) || ( (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && isGrounded)))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }
}
