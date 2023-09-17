using System;
using UnityEngine;

public class Runners : MonoBehaviour
{
    public static Action<float> HitStairs;

    [Header(" Elements ")]
    [SerializeField] private Rigidbody runnerRigidbody;
    [SerializeField] private GameObject splash;
    [SerializeField] private Transform splashSpawnTransform;
    public Animator runnerAnimator;

    private void OnTriggerEnter(Collider collider)
    {
        // check if the runner hit stairs after crossing finish line
        if (collider.CompareTag ("Stairs"))
        {
            transform.SetParent(null);
            runnerAnimator.SetTrigger("Idel");
            HitStairs?.Invoke(transform.position.y);
        }

        else if (collider.CompareTag ("EnemyWall"))
        {
            Destroy(gameObject);
        }

        else if (collider.CompareTag("Pit"))
        {
            runnerRigidbody.useGravity = true;
            float randomForce = UnityEngine.Random.Range(1000, 4000);
            runnerRigidbody.AddForce(transform.forward * randomForce * Time.deltaTime, ForceMode.Impulse);
            transform.SetParent (null);
        }
    }

    // when the runner is destroyed
    private void OnDestroy()
    {
        if (!this.gameObject.scene.isLoaded) return;

        float randomRotaion = UnityEngine.Random.Range(0, 360);
        float randomScale = UnityEngine.Random.Range(0.04f, 0.07f);
        Vector3 scaleVec = new Vector3(randomScale, randomScale, randomScale);
        GameObject splashInstance = Instantiate(splash, splashSpawnTransform.position, Quaternion.Euler(-90, randomRotaion, 0));
        splashInstance.transform.localScale = scaleVec;
    }
}
