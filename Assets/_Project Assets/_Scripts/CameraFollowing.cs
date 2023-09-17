using UnityEngine;

public class CameraFollowing : MonoBehaviour
{
    [Header (" Elements ")]
    [SerializeField] private Transform targetTransform;
    [SerializeField] private Vector3 positionOffset;
    [SerializeField] private Animator cameraAnimation;

    [Header (" Settings ")]
    [SerializeField] private float lerpValue;

    private void OnEnable()
    {
        Runners.HitStairs += OnHitStairs;
        ResetPyramid.ResetRunners += OnResetPyramid; 
    }

    private void OnDisable()
    {
        Runners.HitStairs -= OnHitStairs;
        ResetPyramid.ResetRunners -= OnResetPyramid; 
    }

    private void OnHitStairs(float _newheight)
    {
        positionOffset.y = _newheight;
        lerpValue = 0.06f;
    }

    private void OnResetPyramid()
    {
        positionOffset.y = 0;
    }

    private void LateUpdate()
    {
        Vector3 targetPosition = targetTransform.position + positionOffset;
        targetPosition.x = 0;

        transform.position = Vector3.Lerp(transform.position, targetPosition, lerpValue);        
    }

    public void RotateCamera ()
    {
        cameraAnimation.SetTrigger("Rotate");
    }
}
