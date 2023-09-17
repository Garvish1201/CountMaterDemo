using UnityEngine;
using UnityEngine.UI;

public class LevelCompleteTracker : MonoBehaviour
{
    [SerializeField] private Slider levelCompleteSlider;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform finishPointTrasnform;

    private void OnEnable()
    {
        PlatformManager.LevelGenerationComplete += OnLevelGenerationComplete;
    }

    private void OnDisable()
    {
        PlatformManager.LevelGenerationComplete -= OnLevelGenerationComplete;
    }

    void OnLevelGenerationComplete () => SetSlider();

    // set the data for level complete slider
    private void SetSlider()
    {
        finishPointTrasnform = GameObject.FindGameObjectWithTag("Finish").transform;

        levelCompleteSlider.minValue = playerTransform.position.z;
        levelCompleteSlider.maxValue = finishPointTrasnform.position.z;
    }

    // update UI according to the player's position
    private void Update()
    {
        levelCompleteSlider.value = playerTransform.position.z;
    }
}
