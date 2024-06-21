using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Plane", menuName = "Planes")]
public class Plane : ScriptableObject
{
    public enum Type
    {
        DeHavillandDH16,
        FokkerFVII,
        DouglasDC2
    }

    public enum Brand
    {
        KLM
    }

    public GameObject prefab;
    public Type type;
    public Brand brand;
}
