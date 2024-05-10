using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //SYSTEM
    Rigidbody2D rb;
    Animator animator;

    //MOVEMENT
    Vector2 _Direction;
    public float _MoveSpeed = 30f;
    bool isFacingRight = true;

    void Start()
    {
        Setup();
        OnCustomEnable();
    }

    void Setup()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }

    void OnCustomEnable()
    {
        InputManager.Instance.Input.Gameplay.Movement.performed += OnMovement;
        InputManager.Instance.Input.Gameplay.Movement.canceled += OnMovement;

        InputManager.Instance.Input.Gameplay.Attack.performed += OnAutoAttack;
        InputManager.Instance.Input.Gameplay.Attack.canceled += OnAutoAttack;

        InputManager.Instance.Input.Gameplay.FirstSkill.performed += OnFirstSkill;
        InputManager.Instance.Input.Gameplay.FirstSkill.canceled += OnFirstSkill;

        InputManager.Instance.Input.Gameplay.SecondSkill.performed += OnSecondSkill;
        InputManager.Instance.Input.Gameplay.SecondSkill.canceled += OnSecondSkill;

        InputManager.Instance.Input.Gameplay.UltimateSkill.performed += OnUltimateSkill;
        InputManager.Instance.Input.Gameplay.UltimateSkill.canceled += OnUltimateSkill;

        InputManager.Instance.Input.Gameplay.Pause.performed += OnPause;
    }

    void OnDisable()
    {
        InputManager.Instance.Input.Gameplay.Movement.performed -= OnMovement;
        InputManager.Instance.Input.Gameplay.Movement.canceled -= OnMovement;

        InputManager.Instance.Input.Gameplay.Attack.performed -= OnAutoAttack;
        InputManager.Instance.Input.Gameplay.Attack.canceled -= OnAutoAttack;

        InputManager.Instance.Input.Gameplay.FirstSkill.performed -= OnFirstSkill;
        InputManager.Instance.Input.Gameplay.FirstSkill.canceled -= OnFirstSkill;

        InputManager.Instance.Input.Gameplay.SecondSkill.performed -= OnSecondSkill;
        InputManager.Instance.Input.Gameplay.SecondSkill.canceled -= OnSecondSkill;

        InputManager.Instance.Input.Gameplay.UltimateSkill.performed -= OnUltimateSkill;
        InputManager.Instance.Input.Gameplay.UltimateSkill.canceled -= OnUltimateSkill;

        InputManager.Instance.Input.Gameplay.Pause.performed -= OnPause;
    }

    private void OnMovement(InputAction.CallbackContext context)
    {
        _Direction = context.ReadValue<Vector2>();
        animator.SetFloat("MoveSpeed", _Direction.magnitude);
        Flip();
    }

    private void OnAutoAttack(InputAction.CallbackContext context)
    {
        int index = (int)SkillType.AutoAttack;
        SkillManager.Instance.SkillList[index].isSkillPresses = context.ReadValueAsButton();
    }

    void OnFirstSkill(InputAction.CallbackContext context)
    {
        int index = (int)SkillType.FirstSkill;
        SkillManager.Instance.SkillList[index].isSkillPresses = context.ReadValueAsButton();
    }

    void OnSecondSkill(InputAction.CallbackContext context)
    {
        int index = (int)SkillType.SecondSkill;
        SkillManager.Instance.SkillList[index].isSkillPresses = context.ReadValueAsButton();
    }

    void OnUltimateSkill(InputAction.CallbackContext context)
    {
        int index = (int)SkillType.UltimateSkill;
        SkillManager.Instance.SkillList[index].isSkillPresses = context.ReadValueAsButton();
    }

    void OnPause(InputAction.CallbackContext context)
    {
        GameManager.Instance.ChangeState(GameState.Paused);
    }

    void Flip()
    {
        if (_Direction.x == 0)
            return;
        if (_Direction.x < 0 && isFacingRight || _Direction.x > 0 && !isFacingRight)
        {
            isFacingRight = !isFacingRight;
            transform.Rotate(0, 180, 0);
        }
    }

    void FixedUpdate()
    {
        rb.AddForce(_Direction * _MoveSpeed);
    }
}
