using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private static List<PlaneController> Planes = new List<PlaneController>();
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
        for (int i = 0; i < Hangars.Length; i++)
        {
            Hangars[i].ID = i;
        }

        for (int i = 0; i < Planes.Count; i++)
        {
            Planes[i].planeData.id = i;
        }
    }

    public static void AddPlane(PlaneController plane)
    {
        Planes.Add(plane);
    }
}
