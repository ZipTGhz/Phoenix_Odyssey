using Pathfinding;
using UnityEngine;

public abstract class EnemyAI : CustomMonoBehaviour
{
    protected Seeker _seeker;
    protected Path _path;

    [Header("TARGET")]
    [SerializeField]
    protected Transform _target;

    [Header("COMPONENTS")]
    [SerializeField]
    protected Rigidbody2D _rb;

    [SerializeField]
    protected Animator _animator;

    [SerializeField]
    protected Transform _gFXTransform;

    [Header("CONFIGS")]
    [SerializeField]
    protected float _verticalThreshold = 0.1f;

    [SerializeField]
    protected float _minDistance;

    [SerializeField]
    protected float _maxDistance;

    [SerializeField]
    protected float _repeatCallback = 0.5f;

    [SerializeField]
    protected float _moveSpeed = 750;

    [SerializeField]
    protected float _nextWaypointDistance = 1f;

    protected Vector2 _distanceToTarget = Vector2.positiveInfinity;
    protected int _currentWaypoint = 0;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        InvokeRepeating(nameof(UpdatePath), 0, _repeatCallback);
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        _target = GameObject.FindGameObjectWithTag("Player").transform;
        _seeker = GetComponent<Seeker>();
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponentInChildren<Animator>();
        _gFXTransform = GetComponentInChildren<Transform>();
    }

    protected virtual void UpdatePath()
    {
        if (_distanceToTarget.magnitude <= _maxDistance && _seeker.IsDone())
            _seeker.StartPath(_rb.position, _target.position, OnPathCompleted);
    }

    protected virtual void OnPathCompleted(Path p)
    {
        if (p.error)
            return;
        _path = p;
        _currentWaypoint = 0;
    }

    protected virtual void FixedUpdate()
    {
        if (_target == null)
            return;
        SetDistance();
        Move();
    }

    protected virtual void SetDistance()
    {
        _distanceToTarget = (Vector2)_target.position - _rb.position;
        _animator.SetFloat("MoveSpeed", _rb.velocity.magnitude);
        _animator.SetFloat("AttackRange", _distanceToTarget.magnitude);
    }

    protected virtual void Move() { }

    protected virtual void LookAtTarget()
    {
        if (_distanceToTarget.x > 0)
            _gFXTransform.localScale = new Vector3(1, 1, 0);
        else if (_distanceToTarget.x < 0)
            _gFXTransform.localScale = new Vector3(-1, 1, 0);
    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _minDistance);
        Gizmos.DrawWireSphere(transform.position, _maxDistance);
    }

    public void DisableUpdatePath()
    {
        CancelInvoke(nameof(UpdatePath));
    }
}
