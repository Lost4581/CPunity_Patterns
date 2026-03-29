using UnityEngine;

public class IdleState : IState
{
    private PlayerController _player;

    public IdleState(PlayerController player)
    {
        _player = player;
    }

    public string GetName() => "Ожидание";

    public void Enter()
    {
        if (_player.Rb != null)
            _player.Rb.linearVelocity = new Vector3(0, _player.Rb.linearVelocity.y, 0);
    }

    public void Execute()
    {
        if (_player.MoveInput.magnitude > 0.1f)
        {
            _player.ChangeState(_player.WalkState);
            return;
        }

        if (_player.JumpPressed && _player.IsGrounded)
        {
            _player.ChangeState(_player.JumpState);
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