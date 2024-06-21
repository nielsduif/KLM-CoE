using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Hangar : MonoBehaviour
{
    private TMP_Text text;
    private int id;

    private string Text
    {
        set { text.text = value; }
    }

    public int ID
    {
        get { return id; }
        set { 
                id = value;
                Text = $"{id}";    
            }
    }

    private void Awake()
    {
        text = GetComponentInChildren<TMP_Text>();
    }
}
