using UnityEngine;

public class PoisonEnchantment : WeaponDecorator
{
    public PoisonEnchantment(IWeapon weapon) : base(weapon) { }

    public override string GetDescription() => base.GetDescription() + " [яд]";
    public override float GetDamage() => base.GetDamage() + 5f;

    public override void Attack(Transform origin, Vector3 direction)
    {
        base.Attack(origin, direction);
        Debug.Log("ќтравление цели!");
    }
}