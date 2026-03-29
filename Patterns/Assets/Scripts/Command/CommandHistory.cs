using System.Collections.Generic;
using UnityEngine;

public class CommandHistory : MonoBehaviour
{
    private Stack<ICommand> _history = new Stack<ICommand>();
    private List<ICommand> _actionLog = new List<ICommand>();

    public static CommandHistory Instance { get; private set; }

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void ExecuteCommand(ICommand command)
    {
        command.Execute();
        _history.Push(command);
        _actionLog.Add(command);
        GameEventManager.Instance?.Notify("HistoryUpdated", _actionLog);
    }

    public void Undo()
    {
        if (_history.Count > 0)
        {
            ICommand command = _history.Pop();
            command.Undo();
            _actionLog.RemoveAt(_actionLog.Count - 1);
            GameEventManager.Instance?.Notify("HistoryUpdated", _actionLog);
        }
    }

    public List<ICommand> GetActionLog() => _actionLog;
}