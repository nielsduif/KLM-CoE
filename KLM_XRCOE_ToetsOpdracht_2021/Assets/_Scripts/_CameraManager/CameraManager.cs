using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance { get; private set; }

    [SerializeField]
    private Camera mainCamera;

    [SerializeField]
    private static List<CameraTransform> cameraTransforms = new List<CameraTransform>();
    private int current = 0;

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

    public static void AddCameraTransform(CameraTransform _camTransform)
    {
        cameraTransforms.Add(_camTransform);
    }

    private void Start()
    {
        SetCameraTransform(current);
    }

    public void ChangeToCameraTransform(int _nextIndex)
    {
        current = (current + _nextIndex + cameraTransforms.Count) % cameraTransforms.Count;
        SetCameraTransform(current);
    }

    private void SetCameraTransform(int _index)
    {
        mainCamera.transform.parent = cameraTransforms[_index].transform;
        mainCamera.transform.localPosition = Vector3.zero;
        mainCamera.transform.localRotation = Quaternion.identity;
        mainCamera.transform.localScale = Vector3.one;
    }
}