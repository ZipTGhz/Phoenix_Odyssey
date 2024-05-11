using UnityEngine;

public class PlayerController : CustomMonoBehaviour
{
    //SYSTEM
    Rigidbody2D _rb;
    Animator _animator;

    //MOVEMENT
    [SerializeField]
    float _moveSpeed = 1500;

    Vector2 _direction;
    bool _isFacingRight = true;

    protected override void LoadComponents()
    {
        if (_rb || _animator)
            return;

        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponentInChildren<Animator>();
        Debug.Log(transform.name, gameObject);
    }

    void FixedUpdate()
    {
        GetDirection();
        Move();
        Flip();
    }

    void GetDirection()
    {
        _direction = InputManager.Instance.MoveDirection;
    }

    void Move()
    {
        _rb.AddForce(_direction * _moveSpeed * Time.fixedDeltaTime);
        _animator.SetFloat("MoveSpeed", _direction.magnitude);
    }

    void Flip()
    {
        if (_direction.x < 0 && _isFacingRight || _direction.x > 0 && !_isFacingRight)
        {
            _isFacingRight = !_isFacingRight;
            transform.Rotate(0, 180, 0);
        }
    }
}
