using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("EnemySqawner"))
        {
            Debug.Log("Bị va chạm! Game Over!");
            // Sau này: gọi UI, reset level, v.v.
        }
    }
}
