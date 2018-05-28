/*
 * MIT License
 * Copyright (c) 2018 Yu Tanaka
 * https://github.com/am1tanaka/OpenSimpleFramework201801/blob/master/LICENSE
 */

using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

namespace AM1
{
    /// <summary>
    /// スコアやハイスコアなどのゲーム管理を行うクラスです。
    /// タイトルシーンなど、実行を開始するシーンに配置してください。
    /// DontDestroyOnLoadに設定します。
    /// 重複起動はしないので、複数のシーンに配置して構いません。
    /// </summary>
    public class GameSystem : MonoBehaviour
    {
        /// <summary>
        /// シングルトンのインスタンスです。
        /// </summary>
        public static GameSystem Instance
        {
            get
            {
                if (_instance != null)
                {
                    return _instance;
                }

                _instance = FindObjectOfType<GameSystem>();

                if (_instance != null)
                {
                    return _instance;
                }

                Debug.Log("GameSystemを配置してください。");
                Application.Quit();
                return null;
            }
        }
        protected static GameSystem _instance;

        /// <summary>
        /// タイトルからゲームを開始する時のフラグ。
        /// このフラグが設定されていた場合、新規ゲームとして初期化します。
        /// </summary>
        public static bool IsGameStart = false;

        /// <summary>
        /// 操作可能かを返します。以下の時にフラグがfalseになります。
        /// - フェード中
        /// </summary>
        public static bool IsControllerable
        {
            get
            {
                return !LevelChanger.IsFading;
            }
        }

        /// <summary>
        /// 現在のハイスコアを返します。
        /// </summary>
        public static int HighScore
        {
            get;
            private set;
        }
        [TooltipAttribute("点数表示ラベル"), SerializeField]
        private TextMeshProUGUI scoreText;
        [TooltipAttribute("カンスト点数"), SerializeField]
        private int MAX_SCORE = 9999999;

        /// <summary>
        /// 現在のスコアを返します。
        /// スコアの加算にはAddScore()を利用してください。
        /// </summary>
        public static int Score
        {
            get;
            private set;
        }

        /// <summary>
        /// ハイスコアを更新した時にtrueを返します。
        /// </summary>
        public static bool IsHighScore
        {
            get;
            private set;
        }

        private void Awake()
        {
            if (Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            DontDestroyOnLoad(gameObject);
        }

        /// <summary>
        /// スコアを加算します。
        /// カンストチェックとハイスコアの更新チェック、表示の更新をします。
        /// </summary>
        /// <param name="add">加算する点。</param>
        public static void AddScore(int add)
        {
            Score = Mathf.Min(Score+add, Instance.MAX_SCORE);
            if (Score > HighScore)
            {
                HighScore = Score;
                IsHighScore = true;
            }

            Instance.scoreText.text = Score.ToString("D7");
        }

        /// <summary>
        /// ランキングが表示中か
        /// </summary>
        /// <returns></returns>
        public static bool IsRankingShowing
        {
            get
            {
                Scene sc = SceneManager.GetSceneByName("Ranking");
                return (sc.IsValid());
            }
        }

        /// <summary>
        /// ゲーム開始時に呼び出される関数。
        /// ここにゲーム開始時に初期化しておきたい処理を書きます。
        /// </summary>
        public static void InitGame()
        {
            if (IsGameStart)
            {
                Score = 0;
                AddScore(0);
                IsHighScore = false;
            }

            IsGameStart = false;
            Time.timeScale = 1f;
        }

    }
}
