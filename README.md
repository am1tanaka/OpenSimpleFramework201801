# OpenSimpleFramework201801
unityroomなど小作品向けのUnity簡易フレームワークです。機能は以下の通りです。

- シーン切り替えに伴う画面やBGMのフェードイン・フェードアウト
- スコアの管理(カンスト、画面更新、ランキング呼び出し)
- unityroom用のネットランキングとツイート機能
- TextMesh Pro
- ゲームシーンにPostProcessing Stack V2のBloom
- 効果音、BGMの再生(3Dモードは未設定)

# ライセンス
[MITライセンス]()

# 前提
- Unity2018.1.0f2

# ライブデモ
[demo](https://)

# 最初の設定
このリポジトリーは、プロジェクトが利用しているTextMesh ProやPostProcessing Stackなどのアセットを含みません。クローンしたままではエラーが出ますので、以下の手順で設定をしてください。

## UI Builderをお持ちの場合
ランキングとツイートの仮アイコン画像が、`Assets/Images/UI Builder`フォルダーに入っています。

![Icon Files](images\2018-05-28_12h51_35.png)

これらは、<a href="https://www.assetstore.unity3d.com/#!/content/29757?aid=1100l3q4P&pubref=github">UI - Builder</a>の同名のファイルの利用を想定しています。UI Builderをお持ちの場合は、同ファイルをこのフォルダーに上書きコピーしておくと、本来のイメージ通りに表示されます。

![Original Images](images\2018-05-27_23h54_32.png)

UI Builderのものを使う必要はないので、自由に書き換えたり差し替えてください。

## 公式パッケージを追加
Unityを起動して、サンプルプロジェクトを読み込んでください。まずは、**TextMesh Pro**と**Post Processing Stack v2**をインポートします。

- UnityのWindowメニューから、Package Managerを起動します

![Open Package Manager](images\2018-05-27_22h25_08.png)

- Allをクリックして、Postprocessingを選択したら、Installボタンをクリックします

![Install Post Processing](images\2018-05-27_22h27_03.png)

- 同様の手順で、TextMesh Proもインストールします

![Install TextMesh Pro](images\2018-05-27_22h29_47.png)

以上で、必要な標準パッケージのインストールは完了です。[x]ボタンでPackage Managerを閉じてください。

![Close Package Manager](images\2018-05-27_22h31_00.png)

## TextMesh Proの初期設定
TextMesh Proの基本アセットをインポートします。

- Windowメニューから、TextMesh Pro -> Import TMP Essential Resourcesを選択します

![Import TMP Essential Resources](images\2018-05-28_12h58_43.png)

※WindowメニューにTextMesh Proが見当たらない場合は、一度Unityを閉じて、再起動してください。

- Importボタンを押します

![Import](images\2018-05-28_13h00_29.png)

以上で、Projectビューに`TextMesh Pro`フォルダーが作成されます。


## オンラインランキングの設定
オンラインランキングに、naichiさん( @naichilab )とすずきかつーきさん( @divideby_zero )が公開して下さっているものを利用しています。以下に手順が掲載れているので、ニフクラの設定、および、関連データのインポートをしてください。

- [naichilab. 【Unity、WebGL】なるべく簡単にオンラインランキング機能をつけるサンプル](http://blog.naichilab.com/entry/webgl-simple-ranking)

ニフクラSDKのバージョンは3.2.2で動作確認しています。

### ニフクラライブラリの修正
Ver3.2.2のニフクラライブラリでは、通信完了を待つのに`WaitForSeconds()`を利用しているのですが、これだと`Time.timeScale`の影響を受けてしまいます。今回のフレームワークでは、ゲームオーバーやクリア時の処理を停止させるために`Time.timeScale`を`0`にしているので、通信が進まなくなってしまいます。そこで、コードを一部変更します。

- ProjectビューのNCMB -> Scriptフォルダーを開いて、NCMBConnectionスクリプトをダブルクリックして開きます

![Open Script](images\2018-05-28_23h26_12.png)

- `Seconds`で単語検索すると、`408`行目付近に`yield return new WaitForSeconds (waitTime);`という行が見つかるので、以下のように修正します

```cs
yield return new WaitForSecondsRealtime (waitTime);
```

上書き保存します。これで、リアルタイムでの時間待ちになるので`Time.timeScale`の影響を受けなくなります。

### 注意！
このフレームワークを利用する場合は、ゲームオーバーやクリア時に同様の原因でアニメーションなどが停止します。停止させたくないUIなどでは、Animatorの**Update Mode**を`Unscaled Time`にして、`Time.timeScale`の影響を受けないように設定してください。

![Set Unscaled Time](images\2018-05-28_23h29_57.png)

## ツイート用ライブラリの設定
ツイート機能を追加します。これもnaichiさん( @naichilab )ご提供のものを利用しています。以下のリポジトリーを開いて、手順に従ってパッケージをダウンロードして、プロジェクトにインポートしてください。

- [naichilab. WebGLからツイートするサンプル](https://github.com/naichilab/unityroom-tweet)

## TextMesh Proのフォントを作成する
TextMesh Pro用のフォントを作成します。このフレームワークでは、[fontna.com](http://www.fontna.com/)さんのフリーフォント、**ロゴたいぷゴシック**を利用しています。ライセンスをご確認の上、ダウンロードしてください。

- [fontna.com ロゴたいぷゴシック](http://www.fontna.com/blog/1226/) を開いて、フォントをダウンロードします
- ダウンロードしたファイルを展開したら、`ロゴたいぷゴシック.otf`ファイルを、ProjectビューのFontフォルダーにドラッグ＆ドロップします

![Drag&Drop Font File](images\2018-05-27_23h14_38.png)

- ファイル名が日本語だとTextMesh Proで利用できないので、`logotype-gothic`に名前を変更します

![Rename File Name](images\2018-05-27_23h17_01.png)

TextMesh Pro用のフォントの作成手順は、@thorikawaさんによる記事を参考にしました。記事の手順通りに進めてフォントを作成してください。

- [@thorikawa. UnityのText Mesh Proアセットで日本語を使うときの手順](https://qiita.com/thorikawa/items/03b65b75fa9461b53efd)

### 補足
作業手順の補足です。

- Atlas Resolutionは、2048x4096でも大丈夫です
- Character Set & Custom Rangeの設定方法は、[ここの値](https://gist.github.com/thorikawa/2856a7cf912349c0b6b7)を開いて、Rawボタンをクリックします

![Open Raw](images\2018-05-27_23h28_02.png)

- テキストを全て選択してコピーします

![Select All And Copy](images\2018-05-27_23h29_35.png)

- Character Set欄を<i>Custom Range</i>にします
- Character Sequence欄に貼り付けます

![Past](images\2018-05-27_23h32_46.png)

記事通りに作成が完了したら、SDFファイルをファイル名や場所を変更せずにそのまま保存してください。

保存が完了したら、TextMesh Proのウィンドウは閉じてください。

### 作成したフォントの適用
タイトルシーンにフォントを設定していきます。

- Projectビューから、Scenesフォルダーを開いて、Titleシーンをダブルクリックして起動します
- Hierarchyビューから、TitleCanvas -> Canvasを開きます
- TitleからVersionまで選択したら、作成したフォントを、InspectorビューのFont Asset欄にドラッグ＆ドロップして設定します。日本語が表示されるようになります

![Set Font](images\2018-05-28_22h55_04.png)

タイトルやコピーライトなど、自由に書き換えてください。

## オーディオの実装
タイトル画面とゲーム画面のBGM2曲と、効果音が4種類鳴るように設定されています。フレームワークでは、以下のものを利用しました。

他の音でも構わないので、何か設定すれば指定のタイミングで音が鳴ります。

### 効果音
[効果音ラボ](https://soundeffect-lab.info/)さんの以下の音源を利用しました。

- 決定音
  - [効果音ラボ. ボタン・システム音 / 決定、ボタン押下9]( https://soundeffect-lab.info/sound/button/mp3/decision9.mp3)
- 100万点 or ボールクリック音
  - [効果音ラボ. 戦闘1 / 刀で斬る5]( https://soundeffect-lab.info/sound/battle/mp3/katana-slash5.mp3)
- 10点
  - [効果音ラボ. 戦闘2 / 手裏剣を投げる](https://soundeffect-lab.info/sound/battle/mp3/dart1.mp3)
- 5点
  - [効果音ラボ. 戦闘1 / 剣で打ち合う4]( https://soundeffect-lab.info/sound/battle/mp3/sword-clash4.mp3)

## BGM
BGMは[H/MIX GALLERY](http://www.hmix.net/)さんの以下の音源を使いました。

- タイトル画面用BGM [夢いっぱいの箱](http://www.hmix.net/music/o/o13.mp3)
- ゲーム画面用BGM [華志の舞](http://www.hmix.net/music/n/n82.mp3)

以下のように、オーディオファイルは`Assets/Audio`フォルダーにまとめて入れておくとよいでしょう。

![Audio Folder](images\2018-05-28_12h49_45.png)

### オーディオの割り当て
以下の手順で割り当てます。

- HierarchyビューからGameSystemを開いて、SoundControllerを選択します

![Select SoundController](images\2018-05-28_13h27_32.png)

- Projectビューから、Audioフォルダーを開いて、ダウンロードしたオーディオクリップを以下の順に設定します

- SE List
  - Element0 decision9
  - Element1 katana-slash5
  - Element2 dart1
  - Element3 sword-clash4
- BGM List
  - Element0 o13
  - Element1 n82

![Set Audios](images\2018-05-28_13h31_36.png)

設定したら、クリック時や、操作時に音が再生されるようになります。

# 動作確認
以上で設定完了です。ランキングやツイートが呼び出せることなど、ご確認ください。

## タイトル画面
- ランキングボタンをクリックして、ランキングが表示
- ツイートボタンをクリックして、ツイートダイアログ表示
- 画面をクリックするとゲーム開始

## ゲーム画面
- フェードインが完全に完了したら操作可能になる
- ボールをクリックすると、効果音が鳴って100点加算
- +5, +10, +1Mボタンのクリックで、それぞれの効果音と加点
- Game Overボタンをクリックするとゲームオーバーへ
- Clearボタンをクリックするとクリア表示

## ゲームオーバー画面
- ハイスコアを記録した状態でクリックするとランキング表示
- スコア更新していない場合はそのままタイトルへ戻る

## クリア画面
- クリックするとゲーム画面に戻る

## ランキング画面
- 閉じるとタイトルへ

# まとめ
このようなフレームワークを用意しておくことで、ミニゲームを素早く完成させることができます。ランキングやツイートなどを最初から実装しておけば、常に機能が利用できるので便利です。

ご参考になれば幸いです。

# 利用アセット/参考URL
<iframe src="https://api.assetstore.unity3d.com/affiliate/embed/package/29757/widget-wide?aid=1100l3q4P&pubref=github" style="width:600px; height:130px; border:0px;"></iframe>

- TextMesh Pro
- PostProcessing Stack V2
- [naichilab. 【Unity、WebGL】なるべく簡単にオンラインランキング機能をつけるサンプル](http://blog.naichilab.com/entry/webgl-simple-ranking)
- [ニフクラ Unity用SDK ncmb_unity](https://github.com/NIFCloud-mbaas/ncmb_unity/releases)
- [naichilab. WebGLからツイートするサンプル](https://github.com/naichilab/unityroom-tweet)
- [fontna.com ロゴたいぷゴシック](http://www.fontna.com/blog/1226/)
- [@thorikawa. UnityのText Mesh Proアセットで日本語を使うときの手順](https://qiita.com/thorikawa/items/03b65b75fa9461b53efd)
- [効果音ラボ](https://soundeffect-lab.info/)
- [H/MIX GALLERY](http://www.hmix.net/)
