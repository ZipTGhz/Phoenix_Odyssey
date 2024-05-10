using Pathfinding;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    //TARGET
    Transform target;

    //SYSTEM
    Path path;
    Seeker seeker;
    Rigidbody2D rb;
    public Animator animator;
    public Transform enemyTransform;

    //CONFIG
    public float verticalThreshold = 0.1f;
    public float minDistance;
    public float maxDistance;
    public float repeatCallback = 0.5f;
    public float moveSpeed = 750;
    public float nextWaypointDistance = 1f;

    Vector2 distanceToTarget = Vector2.positiveInfinity;
    int currentWaypoint = 0;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        InvokeRepeating("UpdatePath", 0, repeatCallback);
    }

    void UpdatePath()
    {
        if (target == null)
            CancelInvoke("UpdatePath");
        else if (distanceToTarget.magnitude <= maxDistance && seeker.IsDone())
            seeker.StartPath(rb.position, target.position, OnPathCompleted);
    }

    void OnPathCompleted(Path p)
    {
        if (p.error)
            return;
        path = p;
        currentWaypoint = 0;
    }

    void FixedUpdate()
    {
        if (target == null)
            return;
        SetDistance();
        Move();
    }

    void SetDistance()
    {
        distanceToTarget = (Vector2)target.position - rb.position;
        animator.SetFloat("MoveSpeed", rb.velocity.magnitude);
        animator.SetFloat("AttackRange", distanceToTarget.magnitude);
    }

    void Move()
    {
        if (
            path == null
            || currentWaypoint >= path.vectorPath.Count
            || distanceToTarget.magnitude >= maxDistance
        )
            return;

        LookAtTarget();
        // If the magnitude of the distance to the target is <= the minimum distance
        // we move the enemy vertically
        if (distanceToTarget.magnitude <= minDistance)
        {
            if (Mathf.Abs(target.position.y - rb.position.y) >= verticalThreshold)
            {
                Vector2 verticalDirection = new Vector2(0, target.position.y - rb.position.y);
                verticalDirection.Normalize();
                Vector2 verticalForce = verticalDirection * moveSpeed * Time.fixedDeltaTime;
                rb.AddForce(verticalForce);
            }
        }
        else
        {
            Vector2 direction = (Vector2)path.vectorPath[currentWaypoint] - rb.position;
            direction.Normalize();
            Vector2 force = direction * moveSpeed * Time.fixedDeltaTime;
            rb.AddForce(force);
            float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
            if (distance < nextWaypointDistance)
                ++currentWaypoint;
        }
    }

    void LookAtTarget()
    {
        if (distanceToTarget.x > 0)
            enemyTransform.localScale = new Vector3(1, 1, 0);
        else if (distanceToTarget.x < 0)
            enemyTransform.localScale = new Vector3(-1, 1, 0);
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, minDistance);
        Gizmos.DrawWireSphere(transform.position, maxDistance);
    }
}
