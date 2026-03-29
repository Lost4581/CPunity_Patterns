using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Настройки")]
    public float MoveSpeed = 5f;
    public float JumpForce = 8f;

    [Header("Состояния")]
    public IState CurrentState { get; private set; }
    public IdleState IdleState { get; private set; }
    public WalkState WalkState { get; private set; }
    public JumpState JumpState { get; private set; }
    public AttackState AttackState { get; private set; }

    public Rigidbody Rb;
    public bool IsGrounded;
    public Vector2 MoveInput;
    public bool JumpPressed;
    public bool AttackPressed;

    private IWeapon _currentWeapon;

    private Keyboard _keyboard;
    private Mouse _mouse;

    void Awake()
    {
        Rb = GetComponent<Rigidbody>();

        IdleState = new IdleState(this);
        WalkState = new WalkState(this);
        JumpState = new JumpState(this);
        AttackState = new AttackState(this);

        SetWeapon(new BaseSword());

        _keyboard = Keyboard.current;
        _mouse = Mouse.current;
    }

    void Start()
    {
        ChangeState(IdleState);
    }

    void Update()
    {
        ReadInput();

        IsGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f);

        CurrentState?.Execute();

        if (_keyboard.digit1Key.wasPressedThisFrame) SetWeapon(new BaseSword());
        if (_keyboard.digit2Key.wasPressedThisFrame) SetWeapon(new FireEnchantment(_currentWeapon));
        if (_keyboard.digit3Key.wasPressedThisFrame) SetWeapon(new PoisonEnchantment(_currentWeapon));

        if (_keyboard.zKey.wasPressedThisFrame)
        {
            CommandHistory.Instance?.Undo();
        }
    }

    private void ReadInput()
    {
        Vector2 move = Vector2.zero;

        if (_keyboard.wKey.isPressed) move.y += 1;
        if (_keyboard.sKey.isPressed) move.y -= 1;
        if (_keyboard.aKey.isPressed) move.x -= 1;
        if (_keyboard.dKey.isPressed) move.x += 1;

        MoveInput = move;

        JumpPressed = _keyboard.spaceKey.wasPressedThisFrame;
        AttackPressed = _mouse.leftButton.wasPressedThisFrame;
    }

    public void ChangeState(IState newState)
    {
        if (CurrentState == newState) return;

        CurrentState?.Exit();
        CurrentState = newState;
        CurrentState.Enter();

        GameEventManager.Instance?.Notify("StateChanged", CurrentState.GetName());
    }

    public void SetWeapon(IWeapon weapon)
    {
        _currentWeapon = weapon;
        GameEventManager.Instance?.Notify("WeaponChanged", weapon.GetDescription());
    }

    public IWeapon GetCurrentWeapon() => _currentWeapon;

    public void PerformAttack()
    {
        Vector3 direction = transform.forward;
        var cmd = new AttackCommand(this, _currentWeapon, direction);
        CommandHistory.Instance?.ExecuteCommand(cmd);
    }

    public void PerformMove(Vector3 direction)
    {
        var cmd = new MoveCommand(this, direction);
        CommandHistory.Instance?.ExecuteCommand(cmd);
    }
}