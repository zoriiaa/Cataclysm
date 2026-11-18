using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    [Header("Attacking")]
    public int attackDamage = 10;
    public float attackRange = 1f;
    public Transform attackPoint;
    public LayerMask enemyLayer;
    [Header("Cooldown")]
    public float attackCooldown = 0.5f;
    private float nextAttackTime = 0f;

    void Update()
    {

    }
    public void Attack(InputAction.CallbackContext context)
    {
        if (context.performed && Time.time >= nextAttackTime){
            Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

            foreach (Collider2D enemy in enemies)
            {
                if (enemy.gameObject == gameObject) continue;
                PlayerHealth health = enemy.GetComponent<PlayerHealth>();
                if (health != null)
                {
                    health.TakeDamage(attackDamage);
                    PlayerKnockback knockback = enemy.GetComponent<PlayerKnockback>();
                    if (knockback != null)
                    {
                        knockback.PlayerFeedback(gameObject);
                    }
                }
            }
            nextAttackTime = Time.time + attackCooldown;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
