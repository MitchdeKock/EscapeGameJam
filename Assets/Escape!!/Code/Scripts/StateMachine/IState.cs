public interface IState
{
    void TickCooldown();
    void Tick();
    void OnEnter();
    void OnExit();
}