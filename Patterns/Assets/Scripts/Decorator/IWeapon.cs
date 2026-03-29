public interface IWeapon
{
    string GetDescription();
    float GetDamage();
    void Attack(UnityEngine.Transform origin, UnityEngine.Vector3 direction);
}
