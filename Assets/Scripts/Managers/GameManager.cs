using System;
using CarGame.Enums;
using CarGame.Extensions;
using CarGame.StateMachine;
using UnityEngine.SceneManagement;

namespace CarGame.Managers
{
    public class GameManager : Singleton<GameManager>
    {
        public int currentLevel;

        private IState currentState;
        private InputManager inputManager;

        private void Start()
        {
            inputManager = InputManager.Instance;
            currentLevel = 1;
        }

        #region STATE MACHINE
        public void SetState(IState nextState)
        {
            if (currentState == nextState) return;
            if (currentState != null) currentState.Exit();

            currentState = nextState;
            nextState.Enter();
        }

        internal IState GetCurrentState()
        {
            return currentState;
        }

        #endregion

        #region LEVEL MANAGEMENT

        internal void LoadScene(Scenes sceneToLoad)
        {
            SceneManager.LoadScene((int)sceneToLoad);
        }

        internal bool HasNextLevel()
        {
            int nextLevel = currentLevel + 1;
            int totalSceneNum = SceneManager.sceneCountInBuildSettings;
            if (nextLevel < totalSceneNum)
                return true;
            return false;
        }

        internal void LoadNextLevel()
        {
            currentLevel++;
            SceneManager.LoadScene(currentLevel - 1);
        }

        internal void ResetLevel()
        {
            SceneManager.LoadScene(currentLevel - 1);
        }

        #endregion
    }
}
