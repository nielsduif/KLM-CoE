using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hangar : MonoBehaviour
{
    private Vector3 _spawnPosition;

    private void Awake()
    {
        _spawnPosition = new Vector3(transform.position.x, 0, transform.position.z);
    }

    public Vector3 SpawnPosition
    {
        get { return _spawnPosition; }
    }
}
