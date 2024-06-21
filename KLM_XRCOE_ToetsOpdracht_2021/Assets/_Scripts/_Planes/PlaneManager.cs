using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneManager : MonoBehaviour
{
    public static PlaneManager Instance { get; private set; }

    [SerializeField] private Plane[] planeTypes;
    [SerializeField] private Hangar[] hangars;
    public static List<PlaneController> planes { get; private set; } = new List<PlaneController>();

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
        }
    }

    public void AssignPlaneHangar()
    {
        for (int i = 0; i < planes.Count; i++)
        {
            planes[i].parkHangar = hangars[i];
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
}