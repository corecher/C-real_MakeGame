using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("공격 설정")]
    public Transform attackPoint;    // 공격의 중심점이 될 오브젝트
    public float attackRange = 0.5f; // 공격 반지름 범위
    public LayerMask enemyLayers;   // 공격할 적들의 레이어
    public int attackDamage = 20;    // 공격력

    [Header("공격 입력")]
    public KeyCode attackKey = KeyCode.Z;
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(attackKey))
        {
            Attack();
        }
    }

    void Attack()
    {
        animator.SetTrigger("Attack");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            EnemyState enemys  = enemy.gameObject.GetComponent<EnemyState>();
            enemys.GetDamage(attackDamage);
        }
    }
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.color = Color.yellow;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
