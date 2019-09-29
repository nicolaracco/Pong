using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSizeScaler : MonoBehaviour
{
    private float referenceOrtographicRatio = 17.77633f;

    private float currentAspectRatio;

    void Start()
    {
        float currentAspect = (float)Screen.height / (float)Screen.width;
        Camera.main.orthographicSize = referenceOrtographicRatio * currentAspect;
    }
}
