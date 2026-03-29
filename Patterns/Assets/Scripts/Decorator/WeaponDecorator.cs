public abstract class WeaponDecorator : IWeapon
{
    protected IWeapon _wrapped;

    public WeaponDecorator(IWeapon weapon)
    {
        _wrapped = weapon;
    }

    public virtual string GetDescription() => _wrapped.GetDescription();
    public virtual float GetDamage() => _wrapped.GetDamage();

    public virtual void Attack(UnityEngine.Transform origin, UnityEngine.Vector3 direction)
    {
        _wrapped.Attack(origin, direction);
    }
}
