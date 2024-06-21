using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public static Plane[] Planes { get; private set; }
    public static Hangar[] Hangars { get; private set; }

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
    }

    public static void InitializeGameObjects(Plane[] _planes, Hangar[] _hangars)
    {
        Planes = _planes;
        Hangars = _hangars;
    }
}
