using UnityEngine;
using GameAnalyticsSDK;

public class GameAnalyticsManager : MonoBehaviour
{
    GameManager gameManagerInstance;


    private void Awake()
    {
        if (!GameAnalytics.Initialized)
        {
            GameAnalytics.Initialize();
        }
    }

    private void Start() => gameManagerInstance = GameManager.instance;

    public void _LevelStarted ()
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, $"Level {gameManagerInstance.currentLevel}");
        Debug.Log("Level Started Event Sent");

    }

    public void _LevelCompleted()
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, $"Level {gameManagerInstance.currentLevel}");
        Debug.Log("Level Completed Event Sent");
    }

    public void _LevelFailed()
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, $"Level {gameManagerInstance.currentLevel}");
        Debug.Log("Level Failed Event Sent");
    }
}
