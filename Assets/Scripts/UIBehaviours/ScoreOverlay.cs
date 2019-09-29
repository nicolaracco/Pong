using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pong.UIBehaviours
{
    public class ScoreOverlay : MonoBehaviour
    {
        public TMPro.TMP_Text LeftScoreText;
        public TMPro.TMP_Text RightScoreText;
        public TMPro.TMP_Text LeftMatchPointText;
        public TMPro.TMP_Text RightMatchPointText;

        private float originalAlpha;

        public void OnMatchStateChanged(MatchStateTransition transition)
        {
            LeftScoreText.SetText(transition.leftScore.ToString());
            RightScoreText.SetText(transition.rightScore.ToString());
            ShowMatchPoint(transition);
            switch (transition.newState) {
                case MatchState.GoalMade:
                    HighlightLastGoalScore(transition);
                    break;
                case MatchState.Ended:
                    HighlightScores();
                    HideMatchPoint();
                    break;
                default:
                    ResetHighlight();
                    break;
            }
        }

        void Awake()
        {
            originalAlpha = LeftScoreText.alpha;
        }

        void HighlightLastGoalScore(MatchStateTransition transition)
        {
            LeftMatchPointText.alpha = RightMatchPointText.alpha = 1;
            if (transition.lastGoalPlayerID == PlayerID.Left) {
                LeftScoreText.alpha = 1;
            } else {
                RightScoreText.alpha = 1;
            }
        }

        void HighlightScores()
        {
            LeftScoreText.alpha = RightScoreText.alpha = 1;
            LeftMatchPointText.alpha = RightMatchPointText.alpha = 1;
        }

        void ResetHighlight()
        {
            LeftScoreText.alpha = RightScoreText.alpha = originalAlpha;
            LeftMatchPointText.alpha = RightMatchPointText.alpha = originalAlpha;
        }

        void ShowMatchPoint(MatchStateTransition transition)
        {
            if (!transition.isMatchPoint) {
                HideMatchPoint();
            } else if (transition.leftScore > transition.rightScore) {
                LeftMatchPointText.gameObject.SetActive(true);
                RightMatchPointText.gameObject.SetActive(false);
            } else {
                LeftMatchPointText.gameObject.SetActive(false);
                RightMatchPointText.gameObject.SetActive(true);
            }
        }

        void HideMatchPoint()
        {
            LeftMatchPointText.gameObject.SetActive(false);
            RightMatchPointText.gameObject.SetActive(false);
        }
    }
}
