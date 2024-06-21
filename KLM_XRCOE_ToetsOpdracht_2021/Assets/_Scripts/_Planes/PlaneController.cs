using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlaneController : MonoBehaviour
{
    private NavMeshAgent agent;
    [SerializeField] private float searchRadius = 10;
    [SerializeField] int maxTime = 10;
    private Light spotLight;
    public Hangar parkHangar { get; set; }
    private PlaneState planeState = PlaneState.Routine;

    enum PlaneState
    {
        Routine,
        Parking
    }

    public void SetObjectName()
    {
        gameObject.name = $"{planeData.plane.brand}-{planeData.plane.type}-{planeData.ID}";
    }

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
        spotLight = GetComponentInChildren<Light>();

        SetDestination(CalculateRandomTarget());
        StartChangeTargetCoroutine();

        OnTargetReached += ChangeTarget;
        OnTimerElapsed += ChangeTarget;
    }

    private void Update()
    {
        switch (planeState)
        {
            case PlaneState.Routine:
                if (agent.remainingDistance < agent.stoppingDistance)
                {
                    OnTargetReached?.Invoke();
                }
                break;
            case PlaneState.Parking:
                StopAllCoroutines();
                break;
        }       
    }

    private void SetDestination(Vector3 _target)
    {
        agent.SetDestination(_target);
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

        SetDestination(CalculateRandomTarget());
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

    public void ToggleLight()
    {
        spotLight.enabled = !spotLight.enabled;
    }

    public void ParkPlane()
    {
        SetDestination(parkHangar.transform.position);
        planeState = PlaneState.Parking;
    }
}
