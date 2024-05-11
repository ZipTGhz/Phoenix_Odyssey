using UnityEngine;

public class SkillEvent : MonoBehaviour
{
    //SYSTEM
    [SerializeField]
    Transform attackPoint;

    [SerializeField]
    LayerMask targetLayer;

    [SerializeField]
    // ObjectPooler fireBallPooler;

    PlayerStatus playerStatus;

    void Start()
    {
        playerStatus = GetComponentInParent<PlayerStatus>();
    }

    void DoAutoAttack()
    {
        var skillInfo = SkillManager.Instance.SkillList[0].skillInfoSO;
        var range = skillInfo.meleeAttackRange;
        var damage = skillInfo.skillDamage;

        //Detect all enemies in range of attack
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.position, range, targetLayer);

        //Damage them
        foreach (Collider2D enemy in enemies)
        {
            EnemyStatus e = enemy.GetComponentInChildren<EnemyStatus>();
            e.TakeDamage(damage);
        }
    }

    // void DoSkill()
    // {
    //     //Spawn a fire ball
    //     fireBallPooler
    //         .GetPooledObject()
    //         .GetComponent<FireBall>()
    //         .SetActive(attackPoint.position, attackPoint.rotation, 3, 30);
    //     // Instantiate(fireBallPrefab, attackPoint.position, attackPoint.rotation);
    //     playerStatus.UseMana(1);
    // }

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
