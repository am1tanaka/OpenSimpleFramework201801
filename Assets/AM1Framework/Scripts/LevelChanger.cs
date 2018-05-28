/*
 * MIT License
 * Copyright (c) 2018 Yu Tanaka
 * https://github.com/am1tanaka/OpenSimpleFramework201801/blob/master/LICENSE
 */

using UnityEngine;
using UnityEngine.SceneManagement;

namespace AM1
{
    /// <summary>
    /// シーンの切り替え関連のクラス。フェードと連携します。
    /// </summary>
    [RequireComponent(typeof(Animator))]
    public class LevelChanger : MonoBehaviour
    {
        /// <summary>
        /// フェード中の場合、trueを返します
        /// </summary>
        public static bool IsFading
        {
            get;
            private set;
        }
        /// <summary>
        /// 何かシーンが追加で重なっている場合、trueを返します。
        /// ゲームオーバー表示時にプレイヤーの操作を停止したい時などに利用します。
        /// </summary>
        public static bool IsSceneOverrided
        {
            get;
            private set;
        }

        private static LevelChanger m_Instance;

        private Animator anim;
        private string nextScene = "";

        public delegate void EventAction();
        private event EventAction onPostFadeIn;

        /// <summary>
        /// フェードインが完了した時に実行したい処理を設定します。
        /// 設定できるのは引数、戻り値なしのメソッドです。
        /// </summary>
        /// <param name="postFadeInMethod">引数、戻り値なしのメソッド</param>
        public static void SetPostFadeIn(EventAction postFadeInMethod)
        {
            m_Instance.onPostFadeIn += postFadeInMethod;
        }

        private void Awake()
        {
            m_Instance = this;
            anim = GetComponent<Animator>();

            anim.SetTrigger("FadeIn");
            IsFading = true;
            IsSceneOverrided = false;
        }

        /// <summary>
        /// 指定のシーンに切り替えます。フェードイン・アウトをします。
        /// </summary>
        /// <param name="sc">シーン名</param>
        public static void ChangeScene(string sc)
        {
            IsFading = true;
            m_Instance.nextScene = sc;
            m_Instance.anim.SetTrigger("FadeOut");
        }

        /// <summary>
        /// 指定のシーンに切り替えます。フェードイン・アウトをします。
        /// インスタンスからの呼び出し用メソッドです。
        /// </summary>
        /// <param name="sc">シーン名</param>
        public void ChangeSceneInstance(string sc)
        {
            ChangeScene(sc);
        }

        /// <summary>
        /// 指定のシーンを重ねて表示します。フェードはしません。
        /// </summary>
        /// <param name="sc">シーン名</param>
        public static void AddScene(string sc)
        {
            IsSceneOverrided = true;
            SceneManager.LoadScene(sc, LoadSceneMode.Additive);
        }

        /// <summary>
        /// 指定のシーンを重ねて表示します。フェードはしません。
        /// インスタンスからの呼び出し用メソッドです。
        /// </summary>
        /// <param name="sc">シーン名</param>
        public void AddSceneInstance(string sc)
        {
            AddScene(sc);
        }

        /// <summary>
        /// フェードアウトが完了した時に呼び出すためのアニメーション用メソッドです。
        /// </summary>
        public void OnFadeOut()
        {
            SceneManager.LoadScene(nextScene);
        }

        /// <summary>
        /// フェードインが完了した時に呼び出すためのアニメーション用メソッドです。
        /// </summary>
        public void OnFadeIn()
        {
            IsFading = false;
            if (m_Instance.onPostFadeIn != null)
            {
                m_Instance.onPostFadeIn();
            }
        }
    }
}
