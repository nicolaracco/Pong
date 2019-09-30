using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pong.LayoutBehaviours
{
    public class CameraSizeScaler : MonoBehaviour
    {
        private float referenceOrthographicRatio = 19.98674f;

        void Start()
        {
            StartCoroutine(UpdateOrthographicSize());
        }

        IEnumerator UpdateOrthographicSize()
        {
            while (true) {
                float aspectRatio = (float)Screen.height / (float)Screen.width;
                float orthographicSize = referenceOrthographicRatio * aspectRatio;
                if (Screen.height > Screen.width) {
                    orthographicSize /= 2;
                    if (Camera.main.transform.localRotation.z == 0) {
                        Camera.main.transform.Rotate(0, 0, 90);
                    }
                } else if (Camera.main.transform.localRotation.z != 0) {
                    Camera.main.transform.Rotate(0, 0, -90);
                }
                Camera.main.orthographicSize = orthographicSize;
                yield return new WaitForSeconds(.1f);
            }
        }
    }
}