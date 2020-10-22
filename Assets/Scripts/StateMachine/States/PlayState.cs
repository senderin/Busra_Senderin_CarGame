using System.Collections;
using System.Collections.Generic;
using CarGame.Controllers;
using CarGame.Enums;
using CarGame.Extensions.Level;
using CarGame.Managers;
using UnityEngine;

namespace CarGame.StateMachine.States
{
    public class PlayState : MonoBehaviour, IState
    {
        public GameObject carPrefab;
        public EnterExitController enterExitPairs;

        private bool isGameStart = false;
        private PlayerController player;
        private List<ReplayController> preDrivenCars;
        private UIController uiController;
        private GameManager gameManager;

        private void OnEnable()
        {
            preDrivenCars = new List<ReplayController>();
            uiController = FindObjectOfType<UIController>();

            gameManager = GameManager.Instance;
            gameManager.SetState(this);
            uiController.UpdateLevelNumber(GameManager.Instance.currentLevel);
        }

        public void Enter()
        {
            InputManager.Instance.OnPress += OnPress;

            ResetDrivenCars();
            SpawnPlayerCar();
        }

        public void Exit()
        {
            InputManager.Instance.OnPress += OnPress;
        }

        public void AddToDrivenCars(PlayerController carController)
        {
            isGameStart = false;

            ReplayController replayController = carController.gameObject.AddComponent<ReplayController>();
            replayController.turnSpeed = carController.turnSpeed;
            replayController.forwardSpeed = carController.forwardSpeed;
            replayController.SetReplayData(carController.GetReplayData(), this);
            replayController.SetEnterExitPair(carController.GetEnterExitPair());
            preDrivenCars.Add(replayController);

            Destroy(carController);

            if (IsLevelCompleted())
            {
                uiController.NotifyLevelCompleted();
                StartCoroutine("LoadNextLevel");
            }

            else
            {
                SwitchToNewCar();
            }
        }

        public bool IsGameStart()
        {
            return isGameStart;
        }

        private void OnPress(Direction direction)
        {
            if (!isGameStart)
            {
                isGameStart = true;
            }
            else if (player != null)
            {
                player.Rotate(direction);
            }
        }

        private void SwitchToNewCar()
        {
            enterExitPairs.GetNextEnterExitPair();
            ResetDrivenCars();
            SpawnPlayerCar();
        }

        private void ResetDrivenCars()
        {
            if (preDrivenCars.Count > 0)
            {
                foreach (ReplayController drivenCar in preDrivenCars)
                {
                    drivenCar.ReturnEnterPoint();
                }
            }
        }

        private void SpawnPlayerCar()
        {
            EnterExitPair pair = enterExitPairs.GetCurrentPair();
            GameObject car = Instantiate(carPrefab, pair.enterPoint.position, Quaternion.identity);
            car.name = "Car" + (preDrivenCars.Count + 1);
            player = car.GetComponent<PlayerController>();
            player.SetEnterExitPair(pair);

            uiController.UpdateProgress(preDrivenCars.Count);
        }

        private bool IsLevelCompleted()
        {
            return (preDrivenCars.Count == 8);
        }

        internal void Restart()
        {
            isGameStart = false;
            player.Reset();
            ResetDrivenCars();
        }

        private IEnumerator LoadNextLevel()
        {
            yield return new WaitForSeconds(2f);
            if (!gameManager.HasNextLevel())
            {
                uiController.NotifyAllLevelsCompleted();
            }
            else
                gameManager.LoadNextLevel();
        }
    }
}
