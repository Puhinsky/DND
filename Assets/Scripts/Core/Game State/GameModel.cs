namespace Puhinsky.DND.Core.GameState
{
    public class GameModel
    {
        public ReactiveProperty<GameStateType> State { get; private set; } = new(GameStateType.Setup);
    }
}
