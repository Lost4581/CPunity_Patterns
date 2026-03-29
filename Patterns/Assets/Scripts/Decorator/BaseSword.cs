using UnityEngine;

public class BaseSword : IWeapon
{
    public string GetDescription() => "Стальной меч";
    public float GetDamage() => 15f;

    public void Attack(Transform origin, Vector3 direction)
    {
        Debug.Log($"Удар мечом! Урон: {GetDamage()}");

        if (Physics.Raycast(origin.position, direction, out RaycastHit hit, 2f))
        {
            var enemy = hit.collider.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(GetDamage());
            }
        }
    }
}