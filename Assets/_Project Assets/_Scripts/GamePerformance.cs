using UnityEngine;

public class GamePerformance : MonoBehaviour
{
    [SerializeField] private int targetFps;
    void Start()
    {
        Application.targetFrameRate = targetFps;
        QualitySettings.vSyncCount = 0;
    }
}
