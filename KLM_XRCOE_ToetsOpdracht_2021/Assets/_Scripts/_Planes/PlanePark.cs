using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanePark : MonoBehaviour
{
    public int ID { get; set; }

    private RawImage image;

    private void Awake()
    {
        image = GetComponent<RawImage>();
        image.enabled = false;
    }

    public void ShowIcon(bool _value)
    {
        image.enabled = _value;
    }
}
