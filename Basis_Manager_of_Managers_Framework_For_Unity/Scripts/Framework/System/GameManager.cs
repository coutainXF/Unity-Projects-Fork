using UnityEngine;

public class GameManager : PersistentSingleton<GameManager>
{
    //在enable中注册方法函数，在disable时取消注册。
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
