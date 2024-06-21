using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlaneController : MonoBehaviour
{
    private NavMeshAgent agent;
    [SerializeField] private float searchRadius = 10;
    private Vector3 target;
    [SerializeField] int maxTime = 10;

    public PlaneData planeData { get; set; }

    #region Events
    public delegate void TargetReachedEventHandler();
    public event TargetReachedEventHandler OnTargetReached;

    public delegate void TimerElapsedEventHandler();
    public event TimerElapsedEventHandler OnTimerElapsed;
    #endregion

    private Coroutine changeTargetCoroutine;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        name = $"{planeData.plane.brand}-{planeData.plane.type}-{planeData.id}";

        SetDestination();
        StartChangeTargetCoroutine();

        OnTargetReached += ChangeTarget;
        OnTimerElapsed += ChangeTarget;
    }

    private void Update()
    {
        if(agent.remainingDistance < agent.stoppingDistance)
        {
            OnTargetReached?.Invoke();
        }
    }

    private void SetDestination()
    {
        target = CalculateRandomTarget();
        agent.SetDestination(target);
    }

    private Vector3 CalculateRandomTarget()
    {
        Vector3 _randomDirection = transform.position + (Random.insideUnitSphere * searchRadius);

        NavMeshHit hit;
        if (NavMesh.SamplePosition(_randomDirection, out hit, searchRadius, NavMesh.AllAreas))
        {
            return hit.position;
        }
        else
        {
            return CalculateRandomTarget();
        }
    }

    private void ChangeTarget()
    {
        if(changeTargetCoroutine != null)
        {
            StopCoroutine(changeTargetCoroutine);
        }

        SetDestination();
        StartChangeTargetCoroutine();
    }

    private void StartChangeTargetCoroutine()
    {
        changeTargetCoroutine = StartCoroutine(ChangeTargetAfterTime());
    }

    private IEnumerator ChangeTargetAfterTime()
    {
        yield return new WaitForSeconds(maxTime);
        OnTimerElapsed?.Invoke();
    }
}
