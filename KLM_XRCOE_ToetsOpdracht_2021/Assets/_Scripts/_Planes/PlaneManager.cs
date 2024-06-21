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
    }

    private void OnEnable()
    {
        GameManager.Hangars = hangars;

        if (planes.Length != hangars.Length)
        {
            Debug.LogError($"Amount of planes and hangars are not equal");
            return;
        }

        for (int i = 0; i <= planes.Length - 1; i++)
        {
            GameObject newPlane = Instantiate(planes[i].prefab, hangars[i].transform.position, Quaternion.identity, transform);

            PlaneData _planeData = newPlane.AddComponent<PlaneData>();
            _planeData.plane = planes[i];
            PlaneController planeController = newPlane.AddComponent<PlaneController>();
            planeController.planeData = _planeData;

            GameManager.AddPlane(planeController);
        }
    }
}