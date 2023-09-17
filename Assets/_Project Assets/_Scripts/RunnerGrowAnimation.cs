using UnityEngine;

public class RunnerGrowAnimation : MonoBehaviour
{
    [Header(" Settings")]
    [SerializeField] private bool animate = false;
    [SerializeField] private Vector3 startSize;
    [SerializeField] private Vector3 endSize;
    [SerializeField] private float time;

    [Space]
    [SerializeField] private float min;
    [SerializeField] private float max;

    private void Start()
    {
        time = Random.Range(min, max);
        transform.localScale = Vector3.zero;
    }

    private void Update()
    {
        if (!animate) return;

        transform.localScale = Vector3.MoveTowards(transform.localScale, endSize, time * Time.deltaTime);
    }
}
