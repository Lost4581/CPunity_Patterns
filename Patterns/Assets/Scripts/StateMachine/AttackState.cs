using UnityEngine;

public class AttackState : IState
{
    private PlayerController _player;
    private float _timer = 0f;
    private const float _attackDuration = 0.4f;

    public AttackState(PlayerController player)
    {
        _player = player;
    }

    public string GetName() => "Атака";

    public void Enter()
    {
        _timer = 0f;
        _player.PerformAttack();

        if (_player.Rb != null)
            _player.Rb.linearVelocity *= 0.3f;
    }

    public void Execute()
    {
        _timer += Time.deltaTime;

        if (_timer >= _attackDuration)
        {
            if (_player.IsGrounded)
            {
                _player.ChangeState(_player.MoveInput.magnitude > 0.1f ?
                    _player.WalkState : _player.IdleState);
            }
            else
            {
                _player.ChangeState(_player.JumpState);
            }
        }
    }

    public void Exit() { }
}