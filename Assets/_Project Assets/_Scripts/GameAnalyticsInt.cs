using UnityEngine;
using GameAnalyticsSDK;

public class GameAnalyticsInt : MonoBehaviour
{
    GameManager gameManagerInstance;

    
    private void Awake()
    {
        GameAnalytics.Initialize();
    }

    private void Start()
    {
        gameManagerInstance = GameManager.instance;
    }

    public void _GameStarted ()
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, $"Level {gameManagerInstance.currentLevel}");
    }
}
