using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AM1
{
    public class GameOverManager : MonoBehaviour
    {
        private void Start()
        {
            Time.timeScale = 0f;
            SoundController.StopBGM(true);
            StartCoroutine(GameOverLoop());
        }

        IEnumerator GameOverLoop()
        {
            // 終了のクリック待ち
            while (true)
            {
                if (GameSystem.IsControllerable)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (GameSystem.IsHighScore)
                        {
                            naichilab.RankingLoader.Instance.SendScoreAndShowRanking(GameSystem.HighScore);
                            break;
                        }
                        else
                        {
                            LevelChanger.ChangeScene("Title");
                            yield break;
                        }
                    }
                }
                yield return null;
            }

            // ランキング終了待ち
            Scene rank = SceneManager.GetSceneByName("Ranking");
            while (rank.IsValid())
            {
                yield return null;
            }

            // タイトルへ
            LevelChanger.ChangeScene("Title");
        }
    }
}