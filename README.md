# UniBuildProcess

アプリをビルドするエディタ拡張を簡単に記述できる機能  

## 使用例

```cs
var target = BuildTarget.Android;
var levels = EditorBuildSettings.scenes.Select( x => x.path ).ToArray();

// ビルドの流れを定義
var process = new BuildProcess
{
    // ビルド終了後に AssetDatabase.SaveAssets を実行
    // （ビルド時に変更した設定を元に戻すため）
    new SaveAssetsOnCleanUpCommand(),

    // Console のログをクリア
    new ClearConsoleCommand(),

    // Switch Platform 実行
    new SwitchActiveBuildTargetCommand( target ),

    // Scripting Backend 指定
    new SetScriptingBackendCommand( target, ScriptingImplementation.IL2CPP ),

    // Android の Target Architectures 指定
    new SetAndroidTargetArchitecturesCommand( AndroidArchitecture.ARM64 ),

    // Stack Trace の Log Type 指定
    new SetStackTraceLogTypeCommand( StackTraceLogType.None ),

    // アプリをビルド
    new BuildPlayerCommand
    (
        locationPathName: "result.apk",
        target: target,
        options: BuildOptions.CompressWithLz4HC,
        levels: levels
    ),
};

// ビルド開始
var result = process.Run();

// ビルド結果をログ出力
Debug.Log( result.Message );

// バッチモードでビルドを実行しており、かつエラーが発生した場合は
// ビルドに失敗したことを通知
if ( !Application.isBatchMode ) return;
if ( result.Type != BuildCommandResultType.ERROR ) return;

EditorApplication.Exit( 1 );
```

## 特徴

* アプリをビルドするエディタ拡張を作成する時に、ビルドの流れをわかりやすく記述できます  
* ビルド時に変更した設定をビルド終了後に元に戻すことができます  
    * ビルド後に Git などのバージョン管理でファイルの変更が発生することを防げます  
* BuildProcess に指定するコマンドは自作できます  
