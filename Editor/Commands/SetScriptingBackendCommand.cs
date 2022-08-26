using UnityEditor;

namespace Kogane
{
    /// <summary>
    /// Mono2x ビルドか IL2CPP ビルドかを設定するコマンドを管理するクラス
    /// </summary>
    public sealed class SetScriptingBackendCommand : BuildCommandBase
    {
        //==============================================================================
        // プロパティ
        //==============================================================================
        public override string Tag => nameof( SetScriptingBackendCommand );

        //==============================================================================
        // 変数(readonly)
        //==============================================================================
        private readonly BuildTargetGroup        m_targetGroup;
        private readonly ScriptingImplementation m_backend;

        //==============================================================================
        // 変数
        //==============================================================================
        private ScriptingImplementation m_oldBackend;

        //==============================================================================
        // 関数
        //==============================================================================
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SetScriptingBackendCommand
        (
            BuildTargetGroup        targetGroup,
            ScriptingImplementation backend
        )
        {
            m_targetGroup = targetGroup;
            m_backend     = backend;
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SetScriptingBackendCommand
        (
            BuildTarget             target,
            ScriptingImplementation backend
        ) : this( BuildPipeline.GetBuildTargetGroup( target ), backend )
        {
        }

        /// <summary>
        /// 実行する時に呼び出されます
        /// </summary>
        protected override BuildCommandResult DoRun()
        {
            m_oldBackend = PlayerSettings.GetScriptingBackend( m_targetGroup );
            PlayerSettings.SetScriptingBackend( m_targetGroup, m_backend );
            return Success( m_backend.ToString() );
        }

        /// <summary>
        /// 後始末します
        /// </summary>
        protected override void DoCleanUp()
        {
            PlayerSettings.SetScriptingBackend( m_targetGroup, m_oldBackend );
        }
    }
}