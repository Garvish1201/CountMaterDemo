using TMPro;
using System;
using UnityEngine;
using System.Collections.Generic;

public class EnemyHolder : MonoBehaviour
{
    public static Action AttackCompleted;

    [Header(" Elements ")]
    [SerializeField] public PlayerMovement playerMovementInstance;
    [SerializeField] public Transform enemyHolder;
    [SerializeField] public Transform crowdHolder;
    [SerializeField] public List<Enemy> enemyTrasnforms;
    [SerializeField] public Enemy enemyPrefab;
    [SerializeField] public GameObject textHolder;
    [SerializeField] public TMP_Text T_enemyCount;
    
    [Header (" Settings ")]
    [SerializeField] public int playerCount;
    [SerializeField] public int enemyCount;
    [SerializeField] public bool playerFound = false;

    [Space]
    [SerializeField] public float radius;
    [SerializeField] public float angle;

    float range;


    private void Start()
    {
        // get enemy and runners count for attacking
        for (int i = 0; i < enemyCount; i++)
        {
            Enemy enemyInstace = Instantiate(enemyPrefab, enemyHolder);
            enemyTrasnforms.Add (enemyInstace);
        }
        SetEnemyLocalPosition();
        T_enemyCount.text = enemyCount.ToString();
    }

    private void Update()
    {
        // check for count status, if count is zero attack is completed
        if (enemyHolder.childCount == 0)
        {
            AttackCompleted?.Invoke();
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter(Collider collider)
    {
        if (playerFound) return;

        // if player get's close start attacking
        if (collider.CompareTag ("Player"))
        {
            playerMovementInstance = collider.GetComponent<PlayerMovement>();

            CrowdSystem crowdSystemInstance = playerMovementInstance.GetComponent<CrowdSystem>();

            range = crowdSystemInstance.GetCrowdRadius();
            playerMovementInstance.underAttack = true;
            crowdHolder = playerMovementInstance.crowdHolder;

            playerCount = crowdHolder.childCount;
            AssignPlayer();
            textHolder.gameObject.SetActive(false);   

            playerFound = true;
        }
    }

    private void AssignPlayer()
    {
        int loopCount;

        // if player count is more than enemy
        if (enemyTrasnforms.Count < crowdHolder.childCount)
            loopCount = enemyTrasnforms.Count;
        // if player count is less than enemy
        else
            loopCount = crowdHolder.childCount;
        
        Debug.Log($"Assigning enemy: {enemyTrasnforms.Count}");
        for (int i = 0; i < loopCount; i++)
        {
            // assigin a runner target to each enemy they will be attacking
            enemyTrasnforms[i].SetTarget(crowdHolder.GetChild(i), range);
        }
    }

    // set enemy in order after spawning
    public void SetEnemyLocalPosition()
    {
        for (int i = 0; i < enemyHolder.childCount; i++)
        {
            Vector3 newLocalPosition = GetLocalPosition(i);
            enemyHolder.GetChild(i).localPosition = newLocalPosition;
        }
    }

    // GOLDEN RATION calculation
    private Vector3 GetLocalPosition(int _index)
    {
        float x = radius * Mathf.Sqrt(_index) * Mathf.Cos(Mathf.Deg2Rad * angle * _index);
        float z = radius * Mathf.Sqrt(_index) * Mathf.Sin(Mathf.Deg2Rad * angle * _index);

        return new Vector3(x, 0, z);
    }
}
