using UnityEngine;

public class FireEnchantment : WeaponDecorator
{
    public FireEnchantment(IWeapon weapon) : base(weapon) { }

    public override string GetDescription() => base.GetDescription() + " [Огонь]";
    public override float GetDamage() => base.GetDamage() + 8f;

    public override void Attack(Transform origin, Vector3 direction)
    {
        base.Attack(origin, direction);
        Debug.Log("Дополнительный огненный урон!");
    }
}