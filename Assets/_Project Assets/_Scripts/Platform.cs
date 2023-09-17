using UnityEngine;

public class Platform : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private Vector3 size;

    public float GetLenght ()
    {
        return size.z;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, size);
    }
}
