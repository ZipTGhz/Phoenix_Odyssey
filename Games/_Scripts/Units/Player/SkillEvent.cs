using UnityEngine;

public class SkillEvent : MonoBehaviour
{
    //SYSTEM
    [SerializeField]
    Transform attackPoint;

    [SerializeField]
    LayerMask targetLayer;

    [SerializeField]
    ObjectPooler fireBallPooler;

    PlayerStatus playerStatus;

    void Start()
    {
        playerStatus = GetComponentInParent<PlayerStatus>();
    }

    void DoAutoAttack()
    {
        //Detect all enemies in range of attack
        Collider2D[] enemies = Physics2D.OverlapCircleAll(
            attackPoint.position,
            SkillManager.Instance.GetSkill((int)SkillType.AutoAttack).meleeAttackRange,
            targetLayer
        );

        //Damage them
        foreach (Collider2D enemy in enemies)
        {
            EnemyStatus e = enemy.GetComponentInChildren<EnemyStatus>();
            e.TakeDamage(skillManager.GetSkill((int)SkillType.AutoAttack).skillDamage);
        }
    }

    void DoSkill()
    {
        //Spawn a fire ball
        fireBallPooler
            .GetPooledObject()
            .GetComponent<FireBall>()
            .SetActive(attackPoint.position, attackPoint.rotation, 3, 30);
        // Instantiate(fireBallPrefab, attackPoint.position, attackPoint.rotation);
        playerStatus.UseMana(1);
    }

    void DoFirstSkill() { }

    void DoSecondSkill() { }

    void DoUltimateSkill() { }

    void OnDrawGizmos()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, 0.5f);
    }
}
