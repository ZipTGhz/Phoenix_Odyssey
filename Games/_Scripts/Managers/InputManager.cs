using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    //SINGLETON
    public static InputManager Instance { get; private set; }

    //SYSTEM
    PlayerInputs _input;

    //VARIABLES
    public Vector2 MoveDirection { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            _input = new PlayerInputs();
            OnDelayEnable();
        }
        else
        {
            Debug.LogError("More than one InputManger!");
        }
    }

    void OnDelayEnable()
    {
        _input.Enable();

        _input.Gameplay.Movement.performed += OnMovement;
        _input.Gameplay.Movement.canceled += OnMovement;

        _input.Gameplay.Attack.performed += OnAutoAttack;
        _input.Gameplay.Attack.canceled += OnAutoAttack;

        _input.Gameplay.FirstSkill.performed += OnFirstSkill;
        _input.Gameplay.FirstSkill.canceled += OnFirstSkill;

        _input.Gameplay.SecondSkill.performed += OnSecondSkill;
        _input.Gameplay.SecondSkill.canceled += OnSecondSkill;

        _input.Gameplay.UltimateSkill.performed += OnUltimateSkill;
        _input.Gameplay.UltimateSkill.canceled += OnUltimateSkill;

        _input.Gameplay.Pause.performed += OnPause;
    }

    void OnDisable()
    {
        _input.Gameplay.Movement.performed -= OnMovement;
        _input.Gameplay.Movement.canceled -= OnMovement;

        _input.Gameplay.Attack.performed -= OnAutoAttack;
        _input.Gameplay.Attack.canceled -= OnAutoAttack;

        _input.Gameplay.FirstSkill.performed -= OnFirstSkill;
        _input.Gameplay.FirstSkill.canceled -= OnFirstSkill;

        _input.Gameplay.SecondSkill.performed -= OnSecondSkill;
        _input.Gameplay.SecondSkill.canceled -= OnSecondSkill;

        _input.Gameplay.UltimateSkill.performed -= OnUltimateSkill;
        _input.Gameplay.UltimateSkill.canceled -= OnUltimateSkill;

        _input.Gameplay.Pause.performed -= OnPause;

        _input.Disable();
    }

    private void OnMovement(InputAction.CallbackContext context)
    {
        MoveDirection = context.ReadValue<Vector2>();
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
        if (UIManager.Instance.Pause.activeInHierarchy == false)
            GameManager.Instance.ChangeState(GameState.Paused);
        else
            GameManager.Instance.ChangeState(GameState.Playing);
    }
}
