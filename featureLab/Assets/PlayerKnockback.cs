using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKnockback : MonoBehaviour
{
    public float knockbackForce = 500f; // lực đẩy
    public float upwardForce = 300f;    // lực đẩy lên trên

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("EnemySqawner"))
        {
            // Lấy hướng từ vật đụng tới player
            Vector2 direction = (transform.position - collision.transform.position).normalized;

            // Tạo lực đẩy kết hợp cả ngang và lên
            Vector2 force = new Vector2(direction.x * knockbackForce, upwardForce);

            // Xóa vận tốc cũ để đẩy mượt hơn
            rb.velocity = Vector2.zero;

            // Thêm lực đẩy vào Rigidbody
            rb.AddForce(force);
        }
    }
}
