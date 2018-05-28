using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AM1;

public class TitleManager : MonoBehaviour {
    private void Start()
    {
        Time.timeScale = 1f;

        SoundController.PlayBGM(SoundController.BGM.TITLE);

        // ゲーム開始時に、新規ゲームとして初期化するフラグを設定
        GameSystem.IsGameStart = true;
    }

    /// <summary>
    /// ランキングを呼び出します。
    /// </summary>
    public void ShowRanking()
    {
        SoundController.Play(SoundController.SE.CLICK);
        naichilab.RankingLoader.Instance.SendScoreAndShowRanking(GameSystem.Score);
    }

    /// <summary>
    /// ツイートウィンドウを表示する
    /// </summary>
    public void ShowTweet()
    {
        naichilab.UnityRoomTweet.Tweet(
            "YOUR-GAMEID",
            "【SimpleFramework】で" + GameSystem.HighScore.ToString("#,0")+"点を達成！",
            "unityroom",
            "unity1week");
    }

    private void OnMouseDown()
    {
        if (!GameSystem.IsRankingShowing && GameSystem.IsControllerable) 
        {
            SoundController.Play(SoundController.SE.CLICK);
            SoundController.StopBGM(true);
            LevelChanger.ChangeScene("Game");
        }
    }
}
