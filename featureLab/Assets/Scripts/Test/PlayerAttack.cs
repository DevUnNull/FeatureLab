using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float attackDamgePlayer = 5f;
    public float attackRange = 1f;
    public Transform pointAttack;
    public LayerMask enemyLayer;


    public void PerformAttack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(pointAttack.position, attackRange, enemyLayer);
        foreach (Collider2D enemy in hitEnemies)
        {
            heatEnemy health = enemy.GetComponent<heatEnemy>();
            if (health != null)
            {
                health.TakeDamage(attackDamgePlayer);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (pointAttack == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(pointAttack.position, attackRange);
    }
}
