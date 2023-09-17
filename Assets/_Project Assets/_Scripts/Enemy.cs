using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum EnemyState
    {
        Idel,
        Running
    }
    [Header (" Elements ")]
    [SerializeField] private EnemyState enemyState;
    [SerializeField] private Animator enemyAnimation;
    public Transform targetPlayer;

    [Header(" Settings ")]
    [SerializeField] private float enemySpeed;
    [SerializeField] private float range;

    private void Update()
    {
        // check for the state
        switch (enemyState)
        {
            case EnemyState.Idel:
                break;
            // running towawrds the player 
            case EnemyState.Running:
                transform.position = Vector3.MoveTowards(transform.position, targetPlayer.position, Time.deltaTime * enemySpeed);
                enemyAnimation.SetTrigger("Run");

                // check for the distance, if they get close destory both the player and enemy
                float distance = Vector3.Distance (transform.position, targetPlayer.position); 
                if (distance < range)
                {
                    Destroy(targetPlayer.gameObject);
                    Destroy(gameObject);
                }
                break;
        }
    }

    // get comment for enemy manager
    public void SetTarget (Transform targetTransform, float _range)
    {
        targetPlayer = targetTransform;
        enemyState = EnemyState.Running;

        range = _range;
    }
}
