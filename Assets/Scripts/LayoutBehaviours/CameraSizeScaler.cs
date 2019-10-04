using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pong.LayoutBehaviours
{
    public class CameraSizeScaler : MonoBehaviour
    {
        float targetAspectRatio = 4f / 3f;
        float portraitRotationAngle;

        void Start()
        {
            if (GameSettings.LeftPlayerType == PlayerType.Human
                && GameSettings.RightPlayerType == PlayerType.AI) {
                    portraitRotationAngle = 270f;
            } else {
                portraitRotationAngle = 90f;
            }
            StartCoroutine(UpdateViewArea());
        }

        IEnumerator UpdateViewArea()
        {
            while (true) {
                if (Screen.height > Screen.width) {
                    SetPortraitViewArea();
                } else {
                    SetLandscapeViewArea();
                }
                yield return new WaitForSeconds(10.25f);
            }
        }

        void SetPortraitViewArea()
        {
            float windowAspectRatio = (float)Screen.width / (float)Screen.height;
            // rotate camera by 90 degrees
            Camera.main.transform.rotation = Quaternion.Euler(0, 0, portraitRotationAngle);
            // viewport height
            float scaledHeight = (1f / windowAspectRatio) / targetAspectRatio;
            if (scaledHeight < 1f) {
                Camera.main.orthographicSize = 10f * (scaledHeight / windowAspectRatio);
                Camera.main.rect = new Rect(0, (1f - scaledHeight) / 2f, 1f, scaledHeight);
            } else {
                Camera.main.orthographicSize = 10f / windowAspectRatio;
                Camera.main.rect = new Rect(0, 0, 1, 1);
            }
        }

        void SetLandscapeViewArea()
        {
            float windowAspectRatio = (float)Screen.width / (float)Screen.height;
            Camera.main.transform.rotation = Quaternion.Euler(0, 0, 0);
            Camera.main.orthographicSize = 10f;
            float scaledHeight = windowAspectRatio / targetAspectRatio;

            if (scaledHeight < 1f) {
                Camera.main.rect = new Rect(0, (1f - scaledHeight) / 2f, 1f, scaledHeight);
            } else {
                float scaledWidth = 1f / scaledHeight;
                Camera.main.rect = new Rect((1f - scaledWidth) / 2f, 0, scaledWidth, 1f);
            }
        }
    }
}