using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AM1
{
    [System.Serializable]
    class POINTS
    {
        public int point;
        public SoundController.SE se;
    }
    public class ClickAction : MonoBehaviour
    {
        [TooltipAttribute("得点情報"), SerializeField]
        private POINTS[] pointData;

        public void AddScore(int point)
        {
            SoundController.Play(pointData[point].se);
            GameSystem.AddScore(pointData[point].point);
        }

        public void ChangeScene(string sc)
        {
            LevelChanger.ChangeScene(sc);
        }
    }
}
