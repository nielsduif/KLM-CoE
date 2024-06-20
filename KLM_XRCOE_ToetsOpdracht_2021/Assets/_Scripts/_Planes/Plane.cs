using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Plane", menuName = "Planes")]
public class Plane : ScriptableObject
{
    public enum Type
    {
        DeHavillandDH16
    }

    public enum Brand
    {
        KLM
    }

    public string objectName = "Plane";
    public GameObject prefab;
    public Type type;
    public Brand brand;
}
