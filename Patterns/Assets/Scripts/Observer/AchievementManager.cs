using UnityEngine;
using System.Collections.Generic;

public class AchievementManager : MonoBehaviour, IObserver
{
    private HashSet<string> _unlocked = new HashSet<string>();
    private int _killCount = 0;

    void OnEnable()
    {
        if (GameEventManager.Instance != null)
            GameEventManager.Instance.AddObserver(this);
    }

    void OnDisable()
    {
        if (GameEventManager.Instance != null)
            GameEventManager.Instance.RemoveObserver(this);
    }

    public void OnNotify(string eventType, object data)
    {
        switch (eventType)
        {
            case "EnemyKilled":
                _killCount++;
                CheckKillAchievements(data as string);
                break;

            case "WeaponChanged":
                string weapon = data as string;
                if (weapon != null && weapon.Contains("Огонь") && weapon.Contains("Яд") &&
                    !_unlocked.Contains("MasterEnchanter"))
                {
                    Unlock("MasterEnchanter", "Мастер зачарований!");
                }
                break;
        }
    }

    private void CheckKillAchievements(string enemyType)
    {
        if (_killCount >= 1 && !_unlocked.Contains("FirstBlood"))
            Unlock("FirstBlood", "Первая кровь!");

        if (_killCount >= 3 && !_unlocked.Contains("Killer"))
            Unlock("Killer", "Убийца!");

        if (enemyType == "Boss" && !_unlocked.Contains("BossSlayer"))
            Unlock("BossSlayer", "Убийца боссов!");
    }

    private void Unlock(string id, string name)
    {
        _unlocked.Add(id);
        Debug.Log($"Достижение: {name}");
    }
}