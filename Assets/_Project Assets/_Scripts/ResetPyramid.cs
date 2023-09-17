using System;
using UnityEngine;

public class ResetPyramid : MonoBehaviour
{
    public static Action ResetRunners;
    [SerializeField] private Collider thisCollider;
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag ("Runners"))
        {
            ResetRunners?.Invoke();
            thisCollider.enabled = false;
        }
    }
}
