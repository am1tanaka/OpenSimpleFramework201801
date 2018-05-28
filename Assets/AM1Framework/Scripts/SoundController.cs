/*
 * MIT License
 * Copyright (c) 2018 Yu Tanaka
 * https://github.com/am1tanaka/OpenSimpleFramework201801/blob/master/LICENSE
 */

using UnityEngine;

namespace AM1
{
    /// <summary>
    /// BGMやSEを制御するクラスです。
    /// </summary>
    [RequireComponent(typeof(Animator), typeof(AudioSource))]
    public class SoundController : MonoBehaviour
    {
        /// <summary>
        /// 自分のインスタンス
        /// </summary>
        public static SoundController me
        {
            get;
            private set;
        }

        [TooltipAttribute("SE用のオーディオソース"), SerializeField]
        private AudioSource audioSE;
        [TooltipAttribute("BGM用のオーディオソース"), SerializeField]
        private AudioSource audioBGM;

        /// <summary>
        /// 効果音リスト。この並びと<c>SEList</c>にセットするAudioClipの並びを合わせます。
        /// </summary>
        [SerializeField]
        public enum SE
        {
            CLICK,
            SWING,
            THROW,
            HIT,
            COUNT
        };
        [TooltipAttribute("効果音リスト"), SerializeField]
        private AudioClip[] SEList;

        /// <summary>
        /// BGMの列挙子。この並びと<c>BGMList</c>にセットするAudioClipの並びを合わせます。
        /// </summary>
        public enum BGM
        {
            TITLE,
            BGM,
            NONE
        }
        [TooltipAttribute("BGMリスト"), SerializeField]
        private AudioClip[] BGMList;

        // Use this for initialization
        void Awake()
        {
            if (me == null)
            {
                me = this;
                anim = GetComponent<Animator>();
            }
            IsFading = false;
        }

        private static Animator anim;
        /// <summary>
        /// フェードイン・アウト中を表すフラグ。trueの時、フェード中。
        /// </summary>
        public static bool IsFading
        {
            get;
            private set;
        }

        // フェード中に再生を指定されたBGM。フェードアウトが完了したら再生処理を開始します。
        // すでに設定済みのBGMがあったら、設定済みのBGMは破棄して新しいBGMに予約を変えます。
        private BGM nextBGM = BGM.NONE;
        // 予約したBGMをフェードインさせるかのフラグです。
        private bool nextIsFadin = false;

        /// <summary>
        /// 指定の効果音を鳴らします。
        /// </summary>
        /// <param name="snd">再生したい効果音</param>
        public static void Play(SE snd)
        {
            me.audioSE.PlayOneShot(me.SEList[(int)snd]);
        }

        /// <summary>
        /// 指定のBGMを再生します。
        /// </summary>
        /// <param name="bgm">再生したいBGM</param>
        /// <param name="isFadeIn">フェードインさせたい時trueを設定。デフォルトはfalse。</param>
        public static void PlayBGM(BGM bgm, bool isFadeIn=false)
        {
            // フェードアウト中だった時、
            if (IsFading)
            {
                me.nextBGM = bgm;
                me.nextIsFadin = isFadeIn;
                return;
            }

            // 同じ曲が設定されていて再生中ならなにもしない
            if (me.audioBGM.clip == me.BGMList[(int)bgm])
            {
                if (me.audioBGM.isPlaying)
                {
                    me.audioBGM.volume = 1f;
                    return;
                }
            }
            else
            {
                // 違う曲の場合
                // 曲が設定されていたら、曲を停止
                if (me.audioBGM.clip != null)
                {
                    me.audioBGM.Stop();
                }

                // 曲を設定
                me.audioBGM.clip = me.BGMList[(int)bgm];
            }

            // 再生開始
            me.audioBGM.Play();
            if (isFadeIn)
            {
                anim.SetTrigger("FadeIn");
            }
            else
            {
                me.audioBGM.volume = 1f;
            }
        }

        /// <summary>
        /// BGMを停止します。
        /// </summary>
        /// <param name="isFadeOut">trueを設定すると、フェードアウトしたのち停止。falseだとすぐ停止。</param>
        public static void StopBGM(bool isFadeOut=false)
        {
            if (isFadeOut)
            {
                anim.SetTrigger("FadeOut");
            }
            else
            {
                me.OnFadeOutDone();
            }
        }

        /// <summary>
        /// アニメーションからフェードアウトが完了した時に呼び出します。
        /// </summary>
        public void OnFadeOutDone()
        {
            IsFading = false;
            if ((audioBGM.clip != null) && (audioBGM.isPlaying))
            {
                audioBGM.Stop();
            }

            // 予約があったら次のBGMを再生する
            if (nextBGM != BGM.NONE)
            {
                PlayBGM(nextBGM, nextIsFadin);
                nextBGM = BGM.NONE;
            }
        }

        /// <summary>
        /// アニメーションからフェードインが完了した時に呼び出します。
        /// </summary>
        public void OnFadeInDone()
        {
            IsFading = false;
        }

        /// <summary>
        /// BGMのボリュームを設定します。
        /// </summary>
        /// <param name="vol">0=消音 / 1=最大ボリューム</param>
        public static void SetBGMVolume(float vol)
        {
            me.audioBGM.volume = vol;
        }

        /// <summary>
        /// 効果音を停止します。
        /// </summary>
        public static void Stop()
        {
            me.audioSE.Stop();
        }
    }
}

