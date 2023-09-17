using UnityEngine;

public class DestoryOnTime : MonoBehaviour
{
    [SerializeField] private float timeToDestroy = 1f;

    private void Start()
    {
        Destroy(gameObject, timeToDestroy);
    }
}
