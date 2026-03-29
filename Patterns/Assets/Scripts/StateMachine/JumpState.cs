using UnityEngine;

public class JumpState : IState
{
    private PlayerController _player;
    private bool _hasJumped = false;

    public JumpState(PlayerController player)
    {
        _player = player;
    }

    public string GetName() => "Прыжок";

    public void Enter()
    {
        _hasJumped = false;
    }

    public void Execute()
    {
        if (!_hasJumped && _player.Rb != null)
        {
            _player.Rb.AddForce(Vector3.up * _player.JumpForce, ForceMode.Impulse);
            _hasJumped = true;
        }

        Vector3 airControl = new Vector3(_player.MoveInput.x, 0, _player.MoveInput.y);
        if (_player.Rb != null)
        {
            _player.Rb.linearVelocity = new Vector3(
                airControl.x * _player.MoveSpeed * 0.5f,
                _player.Rb.linearVelocity.y,
                airControl.y * _player.MoveSpeed * 0.5f
            );
        }

        if (_player.IsGrounded && _player.Rb != null && _player.Rb.linearVelocity.y < 0.1f && _hasJumped)
        {
            _player.ChangeState(_player.IdleState);
            return;
        }

        if (_player.AttackPressed)
        {
            _player.ChangeState(_player.AttackState);
            return;
        }
    }

    public void Exit() { }
}