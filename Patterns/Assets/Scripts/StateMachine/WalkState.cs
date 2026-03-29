using UnityEngine;

public class WalkState : IState
{
    private PlayerController _player;

    public WalkState(PlayerController player)
    {
        _player = player;
    }

    public string GetName() => "ױמהüבא";

    public void Enter() { }

    public void Execute()
    {
        Vector3 direction = new Vector3(_player.MoveInput.x, 0, _player.MoveInput.y);

        if (direction.magnitude > 0.1f)
        {
            _player.PerformMove(direction.normalized);
            _player.transform.rotation = Quaternion.LookRotation(direction);
        }

        if (_player.MoveInput.magnitude < 0.1f)
        {
            _player.ChangeState(_player.IdleState);
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