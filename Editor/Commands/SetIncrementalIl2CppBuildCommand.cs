using UnityEditor;

namespace Kogane
{
    /// <summary>
    /// IL2CPP ビルドでインクリメンタルビルドを有効化するかを設定するコマンドを管理するクラス
    /// </summary>
    public sealed class SetIncrementalIl2CppBuildCommand : BuildCommandBase
    {
        //==============================================================================
        // プロパティ
        //==============================================================================
        public override string Tag => nameof( SetIncrementalIl2CppBuildCommand );

        //==============================================================================
        // 変数(readonly)
        //==============================================================================
        private readonly BuildTargetGroup m_targetGroup;
        private readonly bool             m_enabled;

        //==============================================================================
        // 変数
        //==============================================================================
        private bool m_oldEnabled;

        //==============================================================================
        // 関数
        //==============================================================================
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SetIncrementalIl2CppBuildCommand
        (
            BuildTargetGroup targetGroup,
            bool             enabled
        )
        {
            m_targetGroup = targetGroup;
            m_enabled     = enabled;
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SetIncrementalIl2CppBuildCommand
        (
            BuildTarget target,
            bool        enabled
        ) : this( BuildPipeline.GetBuildTargetGroup( target ), enabled )
        {
        }

        /// <summary>
        /// 実行する時に呼び出されます
        /// </summary>
        protected override BuildCommandResult DoRun()
        {
            m_oldEnabled = PlayerSettings.GetIncrementalIl2CppBuild( m_targetGroup );
            PlayerSettings.SetIncrementalIl2CppBuild( m_targetGroup, m_enabled );
            return Success( m_enabled.ToString() );
        }

        /// <summary>
        /// 後始末します
        /// </summary>
        protected override void DoCleanUp()
        {
            PlayerSettings.SetIncrementalIl2CppBuild( m_targetGroup, m_oldEnabled );
        }
    }
}