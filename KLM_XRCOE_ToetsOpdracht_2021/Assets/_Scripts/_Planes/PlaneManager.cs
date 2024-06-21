using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneManager : MonoBehaviour
{
    public static PlaneManager Instance { get; private set; }

    [SerializeField] private Plane[] planes;
    [SerializeField] private Hangar[] hangars;

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

        GameManager.InitializeGameObjects(planes, hangars);
    }

    private void Start()
    {
        if (planes.Length != hangars.Length)
        {
            Debug.LogError($"Amount of planes and hangars are not equal");
            return;
        }

        for (int i = 0; i <= planes.Length - 1; i++)
        {
            Instantiate(planes[i].prefab, hangars[i].SpawnPosition, Quaternion.identity, transform);
        }
    }
}