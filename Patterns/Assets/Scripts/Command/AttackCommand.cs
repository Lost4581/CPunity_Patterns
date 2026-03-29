using UnityEngine;

public class AttackCommand : ICommand
{
    private PlayerController _player;
    private IWeapon _weapon;
    private Vector3 _direction;

    public AttackCommand(PlayerController player, IWeapon weapon, Vector3 direction)
    {
        _player = player;
        _weapon = weapon;
        _direction = direction;
    }

    public void Execute()
    {
        _weapon.Attack(_player.transform, _direction);
        GameEventManager.Instance?.Notify("PlayerAttacked", _weapon.GetDamage());
    }

    public void Undo()
    {
        Debug.Log("Отмена атаки (возврат ресурсов)");
    }

    public string GetDescription() => $"Атака {_weapon.GetDescription()}";
}