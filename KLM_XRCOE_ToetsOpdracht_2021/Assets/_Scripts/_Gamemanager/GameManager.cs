using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public static PlaneManager PlaneManager { get; set; }
    public static Hangar[] Hangars { get; set; }
    [SerializeField]
    private PlanePark[] parkImages;

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

        for (int i = 0; i < parkImages.Length; i++)
        {
            parkImages[i].ID = i;
            parkImages[i].gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            ParkPlanes();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            TogglePlaneLights();
        }
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
