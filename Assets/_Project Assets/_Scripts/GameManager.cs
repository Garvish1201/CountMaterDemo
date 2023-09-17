using System;
using UnityEditor;
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
    [SerializeField] private GameAnalyticsManager analyticsManagerInstance;
    
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
    }

#if UNITY_EDITOR
    [MenuItem("PlayerPrefs / Clear PlayerPerf")]
    static void ClearPlayerPerf()
    {
        PlayerPrefs.DeleteAll();
    } 
#endif

    public void CheckForGameOver()
    {
        if (gameState == GameState.InGame)
        {
            if (playerMovement.finished)
            {
                if (playerMovement.crowdHolder.childCount <= 0)
                {
                    LevelCompleted();
                }
            }
            else
            {
                if (playerMovement.crowdHolder.childCount <= 0)
                {
                    LevelFailed();
                }
            }
        }
    }

    public void LevelCompleted()
    {
        gameState = GameState.LevelCompleted;
        GameStateChange?.Invoke(GameState.LevelCompleted);

        analyticsManagerInstance._LevelCompleted();
        OnLevelComplete();
    }    

    public void LevelFailed()
    {
        gameState = GameState.GameOver;
        GameStateChange?.Invoke(GameState.GameOver);

        analyticsManagerInstance._LevelFailed();
    }

    private void OnLevelComplete ()
    {
        currentLevel++;
        PlayerPrefs.SetInt(levelCompleteTag, currentLevel);
    }
}
