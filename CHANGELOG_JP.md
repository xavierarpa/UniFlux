# チャンジェログ
本パッケージの注目すべき変更点はすべてこのファイルに記載される予定です。
## [1.3.1] - 2023-04-30
### 固定式
- Internal.Stateが先にディスパッチされた場合、ストアで起動するように修正しました。
- 状態変化時にInternal.StateがDispatchでInvokeされるように修正しました。
## [1.3.0] - 2023-04-23
### 追加
- Stateを持つStoreを扱うためのFluxState、State、StateFluxの追加
- Core.FluxにStoreStateとDispatchStateを追加し、State Managementメソッドを扱えるようにしました。
- StoreStateとDispatchStateをエクステンションで実装しました。
### 実験
- StoreとDispatchのキーとしてScriptableオブジェクトを使用できるようにすることで、モジュールをよりモジュール化することができますが、ボイラープレートも増えます。
### 削除しました
- ActionFluxParamのdictionary_readを削除しました。
## [1.2.2] - 2023-04-20
### 固定式
- FuncFluxとFuncFluxParamは、1つのキーに1つだけ追加するように設計されていますが、複数のキーを追加することができます。そのため、Func< TResult >をTStorageとして使用するだけで、最適化の複雑さを軽減しています。
### 削除しました
- ActionFlux, ActionFluxParam, FuncFlux, FuncFluxParam の dictionary_read を削除しました。
## [1.2.1] - 2023-04-17
### 固定式
- Storeメソッドは、購読を解除するとストレージから削除されるように修正されました。以前は、最初のものを追加することはできましたが、次のものを購読または解除することはできませんでした。
## [1.2.0] - 2023-04-09
UniFluxが以前よりも最適化されました。
### 追加
- 最適化のため、ActionFluxのdictionary_readに追加されました。
- 最適化のため、ActionParamFluxのdictionary_readに追加されました。
- 最適化のためにFuncFLux辞書_readに追加されました。
- 最適化のため、FuncParamFLux辞書_readに追加されました。
### 削除しました
- IStoreのDictionaryコントラクトを削除しました。
- テストがまだ含まれていないため、Test PlayModeを削除しました。
### 最適化された
- 最適化されたディスパッチ（～100.000反復の文字列キー⇒25msから～15msへ
- 最適化された保存の追加 ~10.000反復文字列キー⇒[300ms GC.Alloc 380MB] から [~15ms GC.Alloc 2.9MB] へ。
- 最適化されたストーリングリムーブは、〜10.000回の繰り返し文字列キー⇒[300ms GC.Alloc 380MB]から[〜15ms GC.Alloc 2.9MB] へ。
## [1.1.1] - 2023-04-09
### 追加
- パースをちっちゃく見ることができる「Architecture.io」追加
- サービステンプレート追加
- パッケージの使用方法のサンプル追加
- Unity EditModeのユニットテスト追加（WIP PlayMode)
- 小さなドキュメントを追加
### を変更しました。
- SubscribeメソッドのFluxAttribute.csを変更（同時に旧構造も削除）。
- 複数のクラスを持つスクリプトを1つのスクリプトにまとめ、異なるスクリプトを使用するように変更した
## [1.1.0] - 2023-04-06
UniFlux for Unityがリリースされました ！MonoFluxと[Flux("Hello World")]でActionを作り、"Hello World".Dispatch() で魔法を見ることができます！
まとめです：
- UniFluxを使用しています。
- YourMonoBehaviour ： MonoFlux
- Flux("Key")] void MethodExamples() => Debug.Log("Hello World")；
- "Key".Dispatch()を実行します；
### 固定
- UniFlux.Core.Internal.Flux<T,T2>でActionFluxParamとFuncFluxを不必要に生成するバグを修正し、特定のインスタンスのみを生成するようになりました。
### を変更しました。
- ISubscribeを削除しました。
- 削除されたIDictionary
- インターナルクラスを「インターナル」アクセスに変更
- 辞書を「readonly」プロパティとして追加。
- ITトリガーを削除し、各IFluxインターフェースに実装
- 標準的な設計規約を維持するためにMethodsの名前を変更（拡張クラスでは互換性のために@IEnumerator、@ITaskなどを残しています。）
### 追加
- ISubscribeやIDictionaryが行っていたことを行うIStoreを追加し、簡略化した。
- パイプラインのように内部のFluxクラスにアクセスするために、UniFlux.Core.Fluxをpublic staticクラスとして追加した
- 文字列型とint型に対応したUniFlux Extensionの追加
- 独自のUniFlux Extensionキータイプを作成するためのScriptTemplateを追加しました。
## [1.0.0] - 2023-03-24
本製品は、*UniFlux*の最初のリリースです。