using UnityEditor;

namespace Kogane
{
    /// <summary>
    /// IL2CPP ビルドのコンパイラ構成を設定するコマンドを管理するクラス
    /// </summary>
    public sealed class SetIl2CppCompilerConfigurationCommand : BuildCommandBase
    {
        //==============================================================================
        // プロパティ
        //==============================================================================
        public override string Tag => nameof( SetIl2CppCompilerConfigurationCommand );

        //==============================================================================
        // 変数(readonly)
        //==============================================================================
        private readonly BuildTargetGroup            m_targetGroup;
        private readonly Il2CppCompilerConfiguration m_configuration;

        //==============================================================================
        // 変数
        //==============================================================================
        private Il2CppCompilerConfiguration m_oldConfiguration;

        //==============================================================================
        // 関数
        //==============================================================================
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SetIl2CppCompilerConfigurationCommand
        (
            BuildTargetGroup            targetGroup,
            Il2CppCompilerConfiguration configuration
        )
        {
            m_targetGroup   = targetGroup;
            m_configuration = configuration;
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SetIl2CppCompilerConfigurationCommand
        (
            BuildTarget                 target,
            Il2CppCompilerConfiguration configuration
        ) : this( BuildPipeline.GetBuildTargetGroup( target ), configuration )
        {
        }

        /// <summary>
        /// 実行する時に呼び出されます
        /// </summary>
        protected override BuildCommandResult DoRun()
        {
            m_oldConfiguration = PlayerSettings.GetIl2CppCompilerConfiguration( m_targetGroup );
            PlayerSettings.SetIl2CppCompilerConfiguration( m_targetGroup, m_configuration );
            return Success( m_configuration.ToString() );
        }

        /// <summary>
        /// 後始末します
        /// </summary>
        protected override void DoCleanUp()
        {
            PlayerSettings.SetIl2CppCompilerConfiguration( m_targetGroup, m_oldConfiguration );
        }
    }
}