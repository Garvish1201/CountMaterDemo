using UnityEngine;

public class AnimatedObstacles : MonoBehaviour
{
    public enum ObstaclesType
    {
        colliderType
    }
    [Header(" Elements ")]
    [SerializeField] ObstaclesType obstaclesType;
    [SerializeField] private Animator animatorToTrigger;
    [SerializeField] private GameObject colliderToDestroy;
    [SerializeField] private Collider thisCollider;
    [SerializeField] private Material onClickedMat;

    [Header(" Settings ")]
    [SerializeField] private string tagsToLookFor;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag(tagsToLookFor))
        {
            PlayerEntered();
        }
    }

    void PlayerEntered ()
    {
        animatorToTrigger.SetTrigger("Animate");

        switch(obstaclesType)
        {
            case ObstaclesType.colliderType:
                Destroy(colliderToDestroy.gameObject);
                thisCollider.enabled = false;
                GetComponent<MeshRenderer>().material = onClickedMat;
                break;
        }
    }
}   
