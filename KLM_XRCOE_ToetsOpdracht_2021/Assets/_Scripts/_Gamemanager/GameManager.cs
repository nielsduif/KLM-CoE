using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public static PlaneManager PlaneManager { get; set; }
    public static Hangar[] Hangars { get; set; }

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
    }

    private void Start()
    {
        PlaneManager.SpawnPlanes();

        for (int i = 0; i < Hangars.Length; i++)
        {
            Hangars[i].ID = i;
        }

        for (int i = 0; i < PlaneManager.planes.Count; i++)
        {
            PlaneController plane = PlaneManager.planes[i];
            plane.planeData.ID = i;
            plane.SetObjectName();
        }

        PlaneManager.AssignPlaneHangar();
    }

    public void TogglePlaneLights()
    {
        PlaneManager.TogglePlanesLight();
    }

    public void ParkPlanes()
    {
        PlaneManager.ParkPlanes();
    }
}
