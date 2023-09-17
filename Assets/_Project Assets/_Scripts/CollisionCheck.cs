using UnityEngine;
using UnityEngine.Events;

// check if the player is colliding with the doors or not
public class CollisionCheck : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] TrackPlayerPosition trackInstance; 
    [SerializeField] private Transform colliderTransform;
    [SerializeField] private Collider[] hitColliders;

    [Header (" Settings ")]
    [SerializeField] private string doorsTag;
    [SerializeField] private string finishTag;
    [SerializeField] private string levelCompleteTag;
    [SerializeField] private float colliderRadius;

    [Header(" Events ")]
    [SerializeField] private UnityEvent LevelCompleted;

    bool isChecking = true;    

    private void OnTriggerEnter(Collider collider)
    {
        // check for final level complete
        if (collider.CompareTag(levelCompleteTag))
        {
            GameManager.instance.LevelCompleted();
        }
        if (!isChecking) return;    

        // check if the player hits the door
        if (collider.CompareTag(doorsTag))
        {
            DoorSystem doorSystem = collider.GetComponent<DoorSystem>();
            doorSystem.DisableCollider();

            // get player position 
            float playerPosition = trackInstance.GetPosition();
            doorSystem.GetResults(playerPosition);
        }
        else if (collider.CompareTag(finishTag))
        {
            collider.enabled = false;
            LevelCompleted?.Invoke();
            isChecking = false;
        }
        
    }
}
