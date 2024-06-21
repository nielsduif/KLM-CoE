using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneManager : MonoBehaviour
{
    public static PlaneManager Instance { get; private set; }

    [SerializeField] private Plane[] planeTypes;
    [SerializeField] private Hangar[] hangars;
    [SerializeField] private PlanePark[] icons;
    public static List<PlaneController> planes { get; private set; } = new List<PlaneController>();
    Dictionary<int, Hangar> hangarDict = new Dictionary<int, Hangar>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        GameManager.Hangars = hangars;
        GameManager.PlaneManager = this;
    }

    public void SpawnPlanes()
    {
        if (planeTypes.Length != hangars.Length)
        {
            Debug.LogError($"Amount of planes and hangars are not equal");
            return;
        }

        for (int i = 0; i <= planeTypes.Length - 1; i++)
        {
            GameObject newPlane = Instantiate(planeTypes[i].prefab, hangars[i].transform.position, Quaternion.identity, transform);

            PlaneData _planeData = newPlane.AddComponent<PlaneData>();
            _planeData.plane = planeTypes[i];
            PlaneController _planeController = newPlane.AddComponent<PlaneController>();
            _planeController.planeData = _planeData;
            planes.Add(_planeController);

            icons[i].ID = i;
            _planeController.planePark = icons[i];
        }
    }

    public void AssignPlaneHangar()
    {
        foreach (Hangar hangar in hangars)
        {
            hangarDict[hangar.ID] = hangar;
        }

        foreach (PlaneController plane in planes)
        {
            plane.parkHangar = hangarDict[plane.planeData.ID];
        }
    }

    public void TogglePlanesLight()
    {
        foreach (PlaneController plane in planes)
        {
            plane.ToggleLight();
        }
    }

    public void ParkPlanes()
    {
        foreach (PlaneController plane in planes)
        {
            plane.ParkPlane();
        }
    }

    public void RoutinePlanes()
    {
        foreach (PlaneController plane in planes)
        {
            plane.FollowRoutnine();
        }
    }
}