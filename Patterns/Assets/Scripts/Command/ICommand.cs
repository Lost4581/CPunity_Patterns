public interface ICommand
{
    void Execute();
    void Undo();
    string GetDescription();
}
