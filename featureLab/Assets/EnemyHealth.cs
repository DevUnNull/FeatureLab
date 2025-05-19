using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 30f;
    private float currentHealth;
    public int gold =1;
    public int goldAffter;
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;

        Debug.Log($"Enemy bị đánh! Máu còn: {currentHealth}");

        if (currentHealth <= 0f)
        {
            Die();
            GoldManager.Instance.AddGold(gold);
        }
    }

    void Die()
    {
        Debug.Log("Enemy chết.");
        Destroy(gameObject);
    }
}
