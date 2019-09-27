using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasSizeScaler : MonoBehaviour
{
    void Start()
    {
        OnGUI();
    }
    
    void OnGUI()
    {
        float ratio = (float)Screen.width / (float)Screen.height;
        GetComponent<Canvas>().scaleFactor = ratio / 1.75f;
    }
}
