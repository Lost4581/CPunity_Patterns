using UnityEngine;

public class MoveCommand : ICommand
{
    private PlayerController _player;
    private Vector3 _direction;
    private Vector3 _previousPosition;

    public MoveCommand(PlayerController player, Vector3 direction)
    {
        _player = player;
        _direction = direction;
    }

    public void Execute()
    {
        _previousPosition = _player.transform.position;
        _player.transform.position += _direction * _player.MoveSpeed * Time.deltaTime;
        GameEventManager.Instance?.Notify("PlayerMoved", _direction);
    }

    public void Undo()
    {
        _player.transform.position = _previousPosition;
        GameEventManager.Instance?.Notify("MoveUndone", null);
    }

    public string GetDescription() => $"Движение {_direction}";
}