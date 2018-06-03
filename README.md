# OpenSimpleFramework201801
unityroomなど小作品向けのUnity簡易フレームワークです。機能は以下の通りです。

- シーン切り替えに伴う画面やBGMのフェードイン・フェードアウト
- スコアの管理(カンスト、画面更新、ランキング呼び出し)
- unityroom用のネットランキングとツイート機能
- TextMesh Pro
- ゲームシーンにPostProcessing Stack V2のBloom
- 効果音、BGMの再生(3Dモードは未設定)

# ライセンス
[MIT License](LICENSE)
Copyright (c) 2018 Yu Tanaka

# 前提
- Unity2018.1.0f2

# デモ
[demo](https://am1tanaka.github.io/OpenSimpleFramework201801/demo/)

# 準備手順
[こちら](https://am1tanaka.github.io/OpenSimpleFramework201801/)

# Ver0.0.3以前はBGMがループしない
BGMのループ設定忘れてました。BGMをループさせるには、Titleシーンで以下をご設定ください。

- Titleシーンを開きます
- GameSystem -> SoundControllerを開きます
- BGMを選択します
- InspectorビューからLoopにチェックを入れます
- Inspectorビューの右上のApplyをクリックします

![Set Loop To BGM](https://lh3.googleusercontent.com/hr7gDhSwRCuxTIu9OqmRmaBSIBfUYQp70EpjdfKo3AaTdYqKGr4bhmMAOKukXe04ylBI1icNzuahGd2BwUdtj2exwL67nEqs3exvRgG6nLd6nhf3FMyI3OKkulhMX0Ori96FxArmEwo0Bf6ny4ug7DnoKGC_lfvGbP7wdkbuEkdxYt-1QmBdJ3VlMy0luYl3jMQxyhQSQSKdFIFQlb-LnB43TcQGe0SaPT6tLEwqe8Ak97-iWoMb1LvM3x4Pv62HadlVb29AoD7xvU3fk9lXFV67GxV5UfxSr8F4xUOl39SzjlFa6nHW1COw5F9vn2802dBoDvpWI5mjogY51k60SlJOFWK88ErRme04ukMBYzNG4JQ4g_syyopj65m50wthVySTK2IkkNgC9rrJiuKlsGHMIMkJZz7o_IJUXR_L0-E7gTdGQgcgY_Qc8aCjO5Vk98PyCzN2cpU0LJaLeLiWd5ldEleN0iR_GYlI7iiEbWvR63sglFtxGflNRakQ569WbNBtJladH5NBZGWBVSVWPi3aWkRXSYapHQKgnMlam10y0oBohFQtumqMu8Kf6tw7F5O8dfpkGQ3NoEctgaDnqvfVOoA5arS3EhqyZMJ93fj9yqoKjG799AJtSXFE_EwE3qK1I58bRgsLLl1dtFscZSOMoaDf1FTS=w875-h418-no)

以上です。

