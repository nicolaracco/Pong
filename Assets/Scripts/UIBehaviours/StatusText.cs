using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pong.UIBehaviours
{
    public class StatusText : MonoBehaviour
    {
        private TMPro.TMP_Text textObject;
        private float originalAlpha;
        private float secondsBeforeHiding;

        public void OnMatchStateChanged(MatchStateTransition transition)
        {
            switch (transition.newState) {
                case MatchState.WaitingToStart:
                case MatchState.GoalMade:
                    ShowStatusText("Ready");
                    break;
                case MatchState.Running:
                    ShowStatusText("Start!", false, 0.5f);
                    break;
                case MatchState.Ended:
                    ShowStatusText(
                        System.Enum.GetName(typeof(PlayerID), transition.lastGoalPlayerID) + " WON",
                        true
                    );
                    break;
                default:
                    HideStatusText();
                    break;
            }
        }

        void Awake()
        {
            textObject = GetComponent<TMPro.TMP_Text>();
            originalAlpha = textObject.alpha;
        }

        void Update()
        {
            if (secondsBeforeHiding > 0) {
                secondsBeforeHiding -= Time.deltaTime;
                if (secondsBeforeHiding <= 0) {
                    HideStatusText();
                }
            }
        }

        void ShowStatusText(string text, bool highlighted = false, float durationInSeconds = 0f) 
        {
            textObject.SetText(text);
            textObject.alpha = highlighted ? 1 : originalAlpha;
            secondsBeforeHiding = durationInSeconds;
            gameObject.SetActive(true);
        }

        void HideStatusText()
        {
            gameObject.SetActive(false);
        }
    }
}