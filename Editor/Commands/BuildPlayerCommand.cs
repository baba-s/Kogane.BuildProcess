using System;
using System.Linq;
using System.Text;
using JetBrains.Annotations;
using UnityEditor;
using UnityEditor.Build.Reporting;

namespace Kogane
{
    /// <summary>
    /// プレイヤーをビルドするコマンドを管理するクラス
    /// </summary>
    public sealed class BuildPlayerCommand : BuildCommandBase
    {
        //==============================================================================
        // プロパティ
        //==============================================================================
        public override string Tag => nameof( BuildPlayerCommand );

        //==============================================================================
        // 変数(readonly)
        //==============================================================================
        private readonly BuildPlayerOptions m_buildPlayerOptions;

        //==============================================================================
        // 関数
        //==============================================================================
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public BuildPlayerCommand( BuildPlayerOptions buildPlayerOptions )
        {
            m_buildPlayerOptions = buildPlayerOptions;
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public BuildPlayerCommand
        (
            string[]     levels,
            string       locationPathName,
            BuildTarget  target,
            BuildOptions options
        ) : this
        (
            new BuildPlayerOptions
            {
                scenes           = levels,
                locationPathName = locationPathName,
                target           = target,
                options          = options
            }
        )
        {
        }

        /// <summary>
        /// 実行する時に呼び出されます
        /// </summary>
        protected override BuildCommandResult DoRun()
        {
            var report    = BuildPipeline.BuildPlayer( m_buildPlayerOptions );
            var isSuccess = report.summary.result == BuildResult.Succeeded;
            var message   = ToReportMessage( report, m_buildPlayerOptions );

            return isSuccess ? Success( message ) : Error( message );
        }

        /// <summary>
        /// ビルドレポートをメッセージに整形して返します
        /// </summary>
        private static string ToReportMessage( BuildReport report, BuildPlayerOptions options )
        {
            var builder = new StringBuilder();
            builder.AppendLine();
            builder.AppendLine( EditorJsonUtility.ToJson( new JsonBuildPlayerOptions( options ), true ) );
            builder.AppendLine( EditorJsonUtility.ToJson( new JsonBuildReport( report ), true ) );
            var message = builder.ToString();
            return message;
        }

        //==============================================================================
        // 構造体
        //==============================================================================
        /// <summary>
        /// BuildPlayerOptions 型を JSON に変換できるようにする構造体
        /// </summary>
        [Serializable]
        private struct JsonBuildPlayerOptions
        {
            [UsedImplicitly] public string[]         scenes;
            [UsedImplicitly] public string           locationPathName;
            [UsedImplicitly] public string           assetBundleManifestPath;
            [UsedImplicitly] public BuildTargetGroup targetGroup;
            [UsedImplicitly] public BuildTarget      target;
            [UsedImplicitly] public BuildOptions     options;
            [UsedImplicitly] public string[]         extraScriptingDefines;

            public JsonBuildPlayerOptions( BuildPlayerOptions other )
            {
                scenes                  = other.scenes;
                locationPathName        = other.locationPathName;
                assetBundleManifestPath = other.assetBundleManifestPath;
                targetGroup             = other.targetGroup;
                target                  = other.target;
                options                 = other.options;
                extraScriptingDefines   = other.extraScriptingDefines;
            }
        }

        /// <summary>
        /// BuildReport 型を JSON に変換できるようにする構造体
        /// </summary>
        [Serializable]
        private struct JsonBuildReport
        {
            [UsedImplicitly] public JsonBuildSummary summary;
            [UsedImplicitly] public JsonBuildStep[]  steps;

            public JsonBuildReport( BuildReport other )
            {
                summary = new JsonBuildSummary( other.summary );
                steps   = other.steps.Select( x => new JsonBuildStep( x ) ).ToArray();
            }
        }

        /// <summary>
        /// BuildSummary 型を JSON に変換できるようにする構造体
        /// </summary>
        [Serializable]
        private struct JsonBuildSummary
        {
            [UsedImplicitly] public string buildStartedAt;
            [UsedImplicitly] public string buildEndedAt;
            [UsedImplicitly] public string guid;
            [UsedImplicitly] public string platform;
            [UsedImplicitly] public string platformGroup;
            [UsedImplicitly] public string options;
            [UsedImplicitly] public string outputPath;
            [UsedImplicitly] public string totalSize;
            [UsedImplicitly] public string totalTime;
            [UsedImplicitly] public string totalWarnings;
            [UsedImplicitly] public string totalErrors;
            [UsedImplicitly] public string result;

            public JsonBuildSummary( BuildSummary other )
            {
                buildStartedAt = other.buildStartedAt.ToString( "yyyy/MM/dd HH:mm:ss" );
                guid           = other.guid.ToString();
                platform       = other.platform.ToString();
                platformGroup  = other.platformGroup.ToString();
                options        = other.options.ToString();
                outputPath     = other.outputPath;
                totalSize      = other.totalSize / 1024 / 1024 + " MB";
                totalTime      = other.totalTime.TotalSeconds.ToString( "0.0" ) + " 秒";
                buildEndedAt   = other.buildEndedAt.ToString( "yyyy/MM/dd HH:mm:ss" );
                totalErrors    = other.totalErrors + " 個";
                totalWarnings  = other.totalWarnings + " 個";
                result         = other.result.ToString();
            }
        }

        /// <summary>
        /// BuildStep 型を JSON に変換できるようにする構造体
        /// </summary>
        [Serializable]
        private struct JsonBuildStep
        {
            [UsedImplicitly] public string                 name;
            [UsedImplicitly] public string                 duration;
            [UsedImplicitly] public JsonBuildStepMessage[] messages;
            [UsedImplicitly] public int                    depth;

            public JsonBuildStep( BuildStep other )
            {
                name     = other.name;
                duration = other.duration.TotalSeconds.ToString( "0.0" ) + " 秒";
                messages = other.messages.Select( x => new JsonBuildStepMessage( x ) ).ToArray();
                depth    = other.depth;
            }
        }

        /// <summary>
        /// BuildStepMessage 型を JSON に変換できるようにする構造体
        /// </summary>
        [Serializable]
        private struct JsonBuildStepMessage
        {
            [UsedImplicitly] public string type;
            [UsedImplicitly] public string content;

            public JsonBuildStepMessage( BuildStepMessage other )
            {
                type    = other.type.ToString();
                content = other.content;
            }
        }
    }
}