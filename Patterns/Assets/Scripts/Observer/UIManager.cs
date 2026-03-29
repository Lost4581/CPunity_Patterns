using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour, IObserver
{
    [SerializeField] private TMP_Text weaponText;
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private TMP_Text stateText;
    [SerializeField] private TMP_Text historyText;
    [SerializeField] private TMP_Text logText;

    private StringBuilder _log = new StringBuilder();

    void Start()
    {
        if (GameEventManager.Instance != null)
            GameEventManager.Instance.AddObserver(this);
        else
            Debug.LogError("GameEventManager.Instance is null!");
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
            case "WeaponChanged":
                if (weaponText != null) weaponText.text = $"Оружие: {data}";
                break;

            case "PlayerDamaged":
                if (healthText != null) healthText.text = $"HP: {data}";
                AddToLog($"Получен урон: {data}");
                break;

            case "StateChanged":
                if (stateText != null) stateText.text = $"Состояние: {data}";
                break;

            case "HistoryUpdated":
                UpdateHistory(data as List<ICommand>);
                break;

            case "EnemyKilled":
                AddToLog($"Убит враг: {data}");
                break;

            case "PlayerAttacked":
                AddToLog($"Атака на {data} урона");
                break;
        }
    }

    private void UpdateHistory(List<ICommand> commands)
    {
        if (commands == null || historyText == null) return;

        StringBuilder sb = new StringBuilder("История действий:\n");
        int start = Mathf.Max(0, commands.Count - 5);
        for (int i = commands.Count - 1; i >= start; i--)
        {
            sb.AppendLine($"{i + 1}. {commands[i].GetDescription()}");
        }
        historyText.text = sb.ToString();
    }

    private void AddToLog(string message)
    {
        if (logText == null) return;

        _log.AppendLine($"[{Time.time:F1}] {message}");
        if (_log.Length > 500) _log.Remove(0, 200);
        logText.text = _log.ToString();
    }
}