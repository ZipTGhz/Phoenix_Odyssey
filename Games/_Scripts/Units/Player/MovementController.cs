using UnityEngine;
using UnityEngine.InputSystem;

public class MovementController : MonoBehaviour
{
    //SYSTEM
    Rigidbody2D rb;
    Animator animator;

    //MOVEMENT
    Vector2 _Direction;
    public float _MoveSpeed = 1;
    bool isFacingRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }

    void OnEnable()
    {
        InputManager.Instance.Input.Gameplay.Movement.performed += OnMovement;
        InputManager.Instance.Input.Gameplay.Movement.canceled += OnMovement;
        InputManager.Instance.Input.Enable();
    }

    void OnDisable()
    {
        InputManager.Instance.Input.Gameplay.Movement.performed -= OnMovement;
        InputManager.Instance.Input.Gameplay.Movement.canceled -= OnMovement;
        InputManager.Instance.Input.Disable();
    }

    private void OnMovement(InputAction.CallbackContext context)
    {
        _Direction = context.ReadValue<Vector2>();
        animator.SetFloat("MoveSpeed", _Direction.magnitude);
        Flip();
    }

    private void Flip()
    {
        if (_Direction.x == 0)
            return;
        if (_Direction.x < 0 && isFacingRight || _Direction.x > 0 && !isFacingRight)
        {
            isFacingRight = !isFacingRight;
            transform.Rotate(0, 180, 0);
        }
    }

    private void FixedUpdate()
    {
        rb.AddForce(_Direction * _MoveSpeed);
    }
}
