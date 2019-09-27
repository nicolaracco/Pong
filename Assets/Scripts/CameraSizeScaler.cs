using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [ExecuteAlways]
public class CameraSizeScaler : MonoBehaviour
{
    private float referenceOrtographicRatio = 17.78196f;

    void Start()
    {
        float currentAspect = (float)Screen.height / (float)Screen.width;
        Camera.main.orthographicSize = referenceOrtographicRatio * currentAspect;
    }
}
