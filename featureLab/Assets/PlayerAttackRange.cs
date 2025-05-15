using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    [Header("Attack Settings")]
    public float attackRange = 1f;
    public float attackDamage = 10f;
    public Transform attackPoint;
    public LayerMask enemyLayer;

    [Header("Timing")]
    public float attackCooldown = 0.5f;
    private float lastAttackTime;

    // Gọi hàm này từ animation event hoặc nút bấm
    public void PerformAttack()
    {
        if (Time.time < lastAttackTime + attackCooldown) return;

        // Tìm tất cả enemy trong phạm vi đánh
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        foreach (Collider2D enemyCollider in hitEnemies)
        {
            EnemyHealth enemy = enemyCollider.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage(attackDamage);
            }
        }

        lastAttackTime = Time.time;
    }

    // Vẽ vòng tấn công trong Editor
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
