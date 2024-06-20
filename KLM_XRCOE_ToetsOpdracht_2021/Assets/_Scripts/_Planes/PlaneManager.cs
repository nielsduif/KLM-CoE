using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneManager : MonoBehaviour
{
    public static PlaneManager Instance { get; private set; }

    [SerializeField] Plane[] planes;
    [SerializeField] Hangar[] hangars;

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
        if (planes.Length != hangars.Length)
        {
            Debug.LogError($"Amount of planes and hangars are not equal");
            return;
        }

        for (int i = 0; i <= planes.Length - 1; i++)
        {
            Plane _plane = planes[i];
            GameObject _GOPlane = planes[i].prefab;
            _GOPlane.name = $"{_plane.objectName}{i}";
            Instantiate(_GOPlane, hangars[i].SpawnPosition, Quaternion.identity, transform);
        }
    }
}