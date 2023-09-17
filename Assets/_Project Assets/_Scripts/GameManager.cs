using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static Action<GameState> GameStateChange;
    public static GameManager instance;

    public enum GameState
    {
        Menu,
        InGame,
        GameOver,
        LevelCompleted
    }
    [Header (" Elements ")]
    public GameState gameState;
    public PlayerMovement playerMovement;  
    
    [Header (" Level Settings")]
    public string levelCompleteTag;
    public int currentLevel;
    public int maxLevel;

    private void Awake()
    {
        instance = this;
        currentLevel = PlayerPrefs.GetInt(levelCompleteTag, 0);
        if (currentLevel > maxLevel)
        {
            currentLevel = maxLevel;
        }
    }

    private void Start()
    {
        gameState = GameState.Menu;
        GameStateChange?.Invoke(GameState.Menu);
    }

    public void ChangeStateToStart()
    {
        gameState = GameState.InGame;
        GameStateChange?.Invoke(GameState.InGame);
    }

    private void Update()
    {
        CheckForGameOver();

        if(Input.GetKeyDown(KeyCode.Space))
        {
            PlayerPrefs.DeleteAll();
        }
    }

    public void CheckForGameOver()
    {
        if (gameState == GameState.InGame)
        {
            if (playerMovement.finished)
            {
                if (playerMovement.crowdHolder.childCount <= 0)
                {
                    gameState = GameState.LevelCompleted;
                    GameStateChange?.Invoke(GameState.LevelCompleted);
                    OnLevelComplete();
                }
            }
            else
            {
                if (playerMovement.crowdHolder.childCount <= 0)
                {
                    gameState = GameState.GameOver;
                    GameStateChange?.Invoke(GameState.GameOver);
                }
            }
        }
    }

    public void LevelCompleted()
    {
        gameState = GameState.LevelCompleted;
        GameStateChange?.Invoke(GameState.LevelCompleted);

        OnLevelComplete();
    }    

    private void OnLevelComplete ()
    {
        currentLevel++;
        PlayerPrefs.SetInt(levelCompleteTag, currentLevel);
    }
}
