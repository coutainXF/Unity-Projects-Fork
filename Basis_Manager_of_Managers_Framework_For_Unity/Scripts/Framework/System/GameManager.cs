using UnityEngine;

public class GameManager : PersistentSingleton<GameManager>
{
    //��enable��ע�᷽����������disableʱȡ��ע�ᡣ
    public static System.Action onGameOver;
    public static GameState GameState
    {
        get => Instance._gameState;
        set => Instance._gameState = value;
    }
    [SerializeField] GameState _gameState = GameState.Playing;
}

public enum GameState
{
    Playing,
    Paused,
    GameOver
}
