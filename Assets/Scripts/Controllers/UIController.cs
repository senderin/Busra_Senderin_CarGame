using System;
using CarGame.Managers;
using TMPro;
using UnityEngine;

namespace CarGame.Controllers
{
    public class UIController : MonoBehaviour
    {
        public TextMeshProUGUI levelCompletedText;
        public TextMeshProUGUI levelNumberText;
        public TextMeshProUGUI progressText;

        public void OnClickResetLevelButton()
        {
            GameManager.Instance.ResetLevel();
        }

        internal void UpdateLevelNumber(int level)
        {
            levelNumberText.SetText("Level " + level);
        }

        internal void NotifyLevelCompleted()
        {
            levelCompletedText.gameObject.SetActive(true);
        }

        internal void UpdateProgress(int drivenCars)
        {
            progressText.SetText(drivenCars + " / 8");
        }

        internal void NotifyAllLevelsCompleted()
        {
            levelCompletedText.SetText("Congratulations! All levels are completed!");
        }
    }
}


