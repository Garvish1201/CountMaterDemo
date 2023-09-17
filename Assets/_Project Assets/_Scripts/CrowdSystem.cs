using UnityEngine;
using System.Collections;

public class CrowdSystem : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private PlayerMovement playerMovementInstance;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private Transform crowdHolder;
    [SerializeReference] private GameObject playerPrefab;
    [SerializeReference] private Animator insertingEffectAnimator;
    [SerializeReference] private AnimationValue animationValueInstance;

    [Header (" Settings ")]
    [SerializeField] private float radius;
    [SerializeField] private float angle;

    WaitForSeconds pointThreeSeconds = new WaitForSeconds(0.005f);

    #region Listner For Event
    private void OnEnable()
    {
        DoorSystem.RecivingBonus += OnRecivingBonus;
        DoorSystem.RecivingLoss += OnRecivingLoss;
        ResetPyramid.ResetRunners += OnResetPyramid;
        EnemyHolder.AttackCompleted += OnAttackCompleted;
    }

    private void OnDisable()
    {
        DoorSystem.RecivingBonus -= OnRecivingBonus;
        DoorSystem.RecivingLoss -= OnRecivingLoss;
        ResetPyramid.ResetRunners -= OnResetPyramid;
        EnemyHolder.AttackCompleted -= OnAttackCompleted;
    }
    #endregion

    private void Start() => SetPlayerLocalPosition();

    void OnAttackCompleted() => uiManager.SetPlayerCount(crowdHolder.childCount);

    void OnResetPyramid() => StartCoroutine(nameof(InsertingEffect));

    private void Update()
    {
        uiManager.SetPlayerCount(crowdHolder.childCount);

        //if (playerMovementInstance.finished) return;
        //SetPlayerLocalPosition();
    }

    // BONUS
    private void OnRecivingBonus(int _playersToAdd)
    {
        StartCoroutine(nameof(InsertingEffect));

        for (int i = 0; i < _playersToAdd; i++)
        {
            GameObject instanceObj = Instantiate(playerPrefab, crowdHolder);
            instanceObj.transform.SetAsFirstSibling();
            instanceObj.GetComponent<Animator>().SetTrigger("Run");
        }
        uiManager.SetPlayerCount(crowdHolder.childCount);
    }

    IEnumerator InsertingEffect()
    {
        float time = 0.7f;
        insertingEffectAnimator.SetTrigger("Animate");
        while (time > 0)
        {
            radius = animationValueInstance.animationValue;
            SetPlayerLocalPosition();

            time -= Time.deltaTime;
            yield return null;
        }
    }

    // LOSS
    private void OnRecivingLoss(int _playersToDestory)
    {
        StartCoroutine(nameof(InsertingEffect));
        float childCount = crowdHolder.childCount;
        if (childCount > _playersToDestory)
        {
            for (int i = 0; i < _playersToDestory; i++)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }
        uiManager.SetPlayerCount(crowdHolder.childCount);
    }

    // set player's position according to GOLDEN RATIO
    public void SetPlayerLocalPosition()
    {
        if (crowdHolder.childCount == 0) return;

        for (int i = 0; i < crowdHolder.childCount; i++)
        {
            Vector3 newLocalPosition = GetLocalPosition(i);
            crowdHolder.GetChild(i).localPosition = newLocalPosition; 
        }
    }

    // GOLDEN RATION calculation
    private Vector3 GetLocalPosition (int _index)
    {
        float x = radius * Mathf.Sqrt(_index) * Mathf.Cos(Mathf.Deg2Rad * angle * _index);
        float z = radius * Mathf.Sqrt(_index) * Mathf.Sin(Mathf.Deg2Rad * angle * _index);

        return new Vector3(x, 0, z);
    }

    public float GetCrowdRadius ()
    {
        return radius * Mathf.Sqrt(crowdHolder.childCount);  
    }
}
