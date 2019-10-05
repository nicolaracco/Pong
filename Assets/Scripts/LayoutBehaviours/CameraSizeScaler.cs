using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pong.LayoutBehaviours
{
    public class CameraSizeScaler : MonoBehaviour
    {
        float targetAspectRatio = 4f / 3f;
        float portraitRotationAngle = 90f;
        GameSettings gameSettings;

        void Awake()
        {
            gameSettings = GameSettings.Current;
        }

        void Start()
        {
            // when left player is human and right one is ai, rotate of 270 deg in portrait so the pad will be placed on bottom
            if (gameSettings != null && gameSettings.LeftPlayerType == PlayerType.Human 
                && gameSettings.RightPlayerType == PlayerType.AI
            ) {
                portraitRotationAngle = 270f;
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
                yield return new WaitForSeconds(.25f);
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