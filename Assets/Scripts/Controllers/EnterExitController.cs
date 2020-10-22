using System.Collections.Generic;
using CarGame.Extensions.Level;
using UnityEngine;

namespace CarGame.Controllers
{
    public class EnterExitController : MonoBehaviour
    {
        public List<EnterExitPair> pairList;

        private EnterExitPair currentPair;
        private int currentPairIndex = 0;

        private void Awake()
        {
            if (pairList != null && pairList.Count > 0)
            {
                foreach (EnterExitPair pair in pairList)
                {
                    pair.Disable();
                }

                SetFirstPair();
            }
        }

        private void SetFirstPair()
        {
            currentPair = pairList[currentPairIndex];
            currentPair.Enable();
        }

        public EnterExitPair GetCurrentPair()
        {
            if (currentPair == null)
                SetFirstPair();
            return currentPair;
        }

        public void GetNextEnterExitPair()
        {
            currentPairIndex++;
            currentPair.DisableTexts();
            currentPair = pairList[currentPairIndex];
            currentPair.Enable();
        }
    }
}

