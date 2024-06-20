using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlaneController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float searchRadius = 10;
    private Vector3 target;
    [SerializeField] int maxTime = 10, distanceThreshold = 1;

    public delegate void TargetReachedEventHandler();
    public event TargetReachedEventHandler OnTargetReached;

    public delegate void TimerElapsedEventHandler();
    public event TimerElapsedEventHandler OnTimerElapsed;

    private Coroutine changeTargetCoroutine;

    private void Start()
    {
        SetDestination();
        StartChangeTargetCoroutine();

        OnTargetReached += ChangeTarget;
        OnTimerElapsed += ChangeTarget;
    }

    private void Update()
    {
        float _distance = Vector3.Distance(transform.position, target);
        if(_distance < distanceThreshold)
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
