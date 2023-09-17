using UnityEngine;

public class SoundManager : MonoBehaviour
{
    GameManager gameManagerInstance;
    [Header (" Elements ")]
    [SerializeField] private AudioSource buttonClickSource;
    [SerializeField] private AudioClip buttonClickClip;

    [Space]
    [SerializeField] private AudioSource powerUpSource;
    [SerializeField] private AudioClip powerUpClip;

    [Space]
    [SerializeField] private AudioSource levelCompleteSource;
    [SerializeField] private AudioClip levelCompleteClip;

    private void OnEnable()
    {
        DoorSystem.RecivingBonus += _PlayPowerUpSound;
        GameManager.GameStateChange += _OnGameComplete;
    }

    private void OnDisable()
    {
        DoorSystem.RecivingBonus -= _PlayPowerUpSound;
        GameManager.GameStateChange -= _OnGameComplete;
    }

    private void Start()
    {
        gameManagerInstance = GameManager.instance;
    }

    public void _PlayButtonClickSound()
    {
        buttonClickSource.PlayOneShot(buttonClickClip);
    }

    public void _PlayPowerUpSound(int i)
    {
        powerUpSource.PlayOneShot(powerUpClip);
    }

    public void _PlayerLevelCompleteSound()
    {
        levelCompleteSource.PlayOneShot(levelCompleteClip);

    }

    public void _OnGameComplete (GameManager.GameState gameState)
    {
        if (gameState == GameManager.GameState.LevelCompleted)
        {
            _PlayerLevelCompleteSound();
        }
    }
}
