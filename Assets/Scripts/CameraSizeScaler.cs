using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSizeScaler : MonoBehaviour
{
    private float referenceOrtographicRatio = 19.98674f;

    void Start()
    {
        StartCoroutine(UpdateOrthographicSize());
    }

    IEnumerator UpdateOrthographicSize()
    {
        while (true) {
            float aspectRatio = (float)Screen.height / (float)Screen.width;
            Camera.main.orthographicSize = referenceOrtographicRatio * aspectRatio;
            yield return new WaitForSeconds(.1f);
        }
    }
}
