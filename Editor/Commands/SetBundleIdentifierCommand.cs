using UnityEditor;

namespace Kogane
{
    /// <summary>
    /// Application Identifier を設定するコマンドを管理するクラス
    /// </summary>
    public sealed class SetApplicationIdentifierCommand : BuildCommandBase
    {
        //==============================================================================
        // プロパティ
        //==============================================================================
        public override string Tag => nameof( SetApplicationIdentifierCommand );

        //==============================================================================
        // 変数(readonly)
        //==============================================================================
        private readonly string m_applicationIdentifier;

        //==============================================================================
        // 変数
        //==============================================================================
        private string m_oldApplicationIdentifier;

        //==============================================================================
        // 関数
        //==============================================================================
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SetApplicationIdentifierCommand( string applicationIdentifier )
        {
            m_applicationIdentifier = applicationIdentifier;
        }

        /// <summary>
        /// 実行する時に呼び出されます
        /// </summary>
        protected override BuildCommandResult DoRun()
        {
            m_oldApplicationIdentifier           = PlayerSettings.applicationIdentifier;
            PlayerSettings.applicationIdentifier = m_applicationIdentifier;
            return Success( m_applicationIdentifier );
        }

        /// <summary>
        /// 後始末します
        /// </summary>
        protected override void DoCleanUp()
        {
            PlayerSettings.applicationIdentifier = m_oldApplicationIdentifier;
        }
    }
}