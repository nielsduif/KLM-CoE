using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTransform : MonoBehaviour
{
    private void Awake()
    {
        CameraManager.AddCameraTransform(this);
    }
}
