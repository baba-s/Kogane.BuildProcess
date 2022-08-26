using UnityEditor;

namespace Kogane
{
    /// <summary>
    /// apk ではなく aab をビルドするかどうかを設定するコマンドを管理するクラス
    /// </summary>
    public sealed class SetBuildAppBundleCommand : BuildCommandBase
    {
        //==============================================================================
        // プロパティ
        //==============================================================================
        public override string Tag => nameof( SetBuildAppBundleCommand );

        //==============================================================================
        // 変数(readonly)
        //==============================================================================
        private readonly bool m_buildAppBundle;

        //==============================================================================
        // 変数
        //==============================================================================
        private bool m_oldBuildAppBundle;

        //==============================================================================
        // 関数
        //==============================================================================
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SetBuildAppBundleCommand( bool buildAppBundle )
        {
            m_buildAppBundle = buildAppBundle;
        }

        /// <summary>
        /// 実行する時に呼び出されます
        /// </summary>
        protected override BuildCommandResult DoRun()
        {
            m_oldBuildAppBundle                    = EditorUserBuildSettings.buildAppBundle;
            EditorUserBuildSettings.buildAppBundle = m_buildAppBundle;
            return Success( m_buildAppBundle.ToString() );
        }

        /// <summary>
        /// 後始末します
        /// </summary>
        protected override void DoCleanUp()
        {
            EditorUserBuildSettings.buildAppBundle = m_oldBuildAppBundle;
        }
    }
}