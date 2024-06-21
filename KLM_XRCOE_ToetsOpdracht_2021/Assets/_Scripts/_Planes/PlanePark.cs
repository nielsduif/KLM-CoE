using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanePark : MonoBehaviour
{
    private RawImage image;

    private void Awake()
    {
        image = GetComponent<RawImage>();
        ShowIcon(false);
    }

    public void ShowIcon(bool _value)
    {
        image.enabled = _value;
    }
}
