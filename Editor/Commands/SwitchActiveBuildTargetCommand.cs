using UnityEditor;

namespace Kogane
{
    /// <summary>
    /// Switch Platform を実行するコマンドを管理するクラス
    /// </summary>
    public sealed class SwitchActiveBuildTargetCommand : BuildCommandBase
    {
        //==============================================================================
        // プロパティ
        //==============================================================================
        public override string Tag => nameof( SwitchActiveBuildTargetCommand );

        //==============================================================================
        // 変数
        //==============================================================================
        private readonly BuildTargetGroup m_targetGroup;
        private readonly BuildTarget      m_target;

        //==============================================================================
        // 関数
        //==============================================================================
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SwitchActiveBuildTargetCommand
        (
            BuildTargetGroup targetGroup,
            BuildTarget      target
        )
        {
            m_targetGroup = targetGroup;
            m_target      = target;
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SwitchActiveBuildTargetCommand
        (
            BuildTarget target
        ) : this( BuildPipeline.GetBuildTargetGroup( target ), target )
        {
        }

        /// <summary>
        /// 実行する時に呼び出されます
        /// </summary>
        protected override BuildCommandResult DoRun()
        {
            var currentTarget = EditorUserBuildSettings.activeBuildTarget;

            if ( currentTarget == m_target ) return Success( m_target.ToString() );

            if ( EditorUserBuildSettings.SwitchActiveBuildTarget( m_targetGroup, m_target ) )
            {
                return Success( m_target.ToString() );
            }

            return Error( m_target.ToString() );
        }
    }
}